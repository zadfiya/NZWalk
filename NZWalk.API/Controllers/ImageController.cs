using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalk.API.Model.Domain;
using NZWalk.API.Model.DTO;
using NZWalk.API.Repository;
using static System.Net.Mime.MediaTypeNames;
using System;
using NZWalk.API.Data;

namespace NZWalk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository imageRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly AppDbContext appContext;

        public ImageController(IImageRepository imageRepository, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, AppDbContext appContext)
        {
            this.imageRepository = imageRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.appContext = appContext;
        }
        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadDTO request)
        {
            ValidateFileUpload(request);
            if(ModelState.IsValid)
            {
                var image = new Model.Domain.Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = Path.GetFileName(request.File.FileName),
                    FileDescription = request.FileDescription
                };
                var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");

                //upload on local path
                using var stream = new FileStream(localFilePath, FileMode.Create);
                await image.File.CopyToAsync(stream);

                //https://localhoist:port/images/image.png
                var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request
                    .PathBase}/Images/{image.FileName}";
                image.FilePath = urlFilePath;

                //add images
                
                    var abc = await appContext.Image.AddAsync(image);
                    await appContext.SaveChangesAsync();
                return Ok(image);
                    //imageRepository.Upload(imageDomainModel);

            }
            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadDTO request)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if(!allowedExtensions.Contains(Path.GetExtension(request.File.FileName))) {
                ModelState.AddModelError("file", "Unsupported file extension");
            }

            if (request.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size morethan 10 MB is not allowed!!");
            }
        }
    }
}

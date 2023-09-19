using Microsoft.EntityFrameworkCore;
using NZWalk.API.Data;
using NZWalk.API.Model.Domain;

namespace NZWalk.API.Repository
{
    public class ImageRepository:IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly AppDbContext appContext;

        public ImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, AppDbContext appContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.appContext = appContext;
        }

        async Task<Image> IImageRepository.Upload(Image image)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");

            //upload on local path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            //https://localhoist:port/images/image.png
            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request
                .PathBase}/Images/{image.FileName}";
            image.FilePath = urlFilePath;

            //add images
            try
            {
                var abc = await appContext.Image.AddAsync(image);
                await appContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
               

                // If available, access the inner exception for more details
                if (ex.InnerException != null)
                {
                    string v = $"Inner Exception: {ex.InnerException.Message}";
                    
                    // You can also log the stack trace and other relevant information here
                }

                // Handle the exception as needed (e.g., roll back a transaction)
            }
            catch (Exception ex)
            {
                string v =  $"An error occurred: {ex.Message}";
            }



            return image;

        }
    }
}

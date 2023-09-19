using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalk.API.Data;
using NZWalk.API.Model;
using NZWalk.API.Model.DTO;
using NZWalk.API.Repository;
namespace NZWalk.API.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using NZWalk.API.Mappings;

    
    
    [Route("api/[controller]")]
    [ApiController]
    
    public class RegionController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper mapper;
    private readonly ILogger logger;

    public RegionController(IRegionRepository regionRepository, IMapper _mapper, ILogger logger)
        {
            
            _regionRepository = regionRepository;
               mapper = _mapper;
        this.logger = logger;
    }

        [HttpGet]
        [Authorize(Roles ="Reader")]
    public async Task<IActionResult> GetAll(string? filter, string? filterOn)
        {
            var regions = await _regionRepository.GetAllAsync();
            //var regionDTOs = new List<RegionDTO>();
            //foreach (var region in regions)
            //{
            //    regionDTOs.Add(new RegionDTO
            //    {
            //        Id = region.Id,
            //        Name = region.Name,
            //        Code = region.Code,
            //        RegionImageUrl = region.RegionImageUrl,
            //    });
            //}
            
            return Ok(mapper.Map<List<RegionDTO>>(regions));
        }

        [HttpGet]
        [Route("{id:Guid}")]
    [Authorize]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var region = await _regionRepository.GetByIdAsync(id);
            if (region == null)
                return NotFound();
            
            //RegionDTO regionDTO = new RegionDTO
            //{
            //    Id = region.Id,
            //    Name = region.Name,
            //    Code = region.Code,
            //    RegionImageUrl = region.RegionImageUrl,
            //};


        return Ok(mapper.Map<RegionDTO>(region));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionDTO addRegionDTO)
        {
            
            //var regionModel = new Region
            //{
            //    Id = new Guid(),
            //    Code = addRegionDTO.Code,
            //    Name = addRegionDTO.Name,
            //    RegionImageUrl = addRegionDTO.RegionImageUrl,
            //};
            
        var regionModel = await _regionRepository.CreateAsync(mapper.Map<Region>(addRegionDTO));


        //var regionDto = new RegionDTO
        //{
        //    Id = regionModel.Id,
        //    Code = regionModel.Code,    
        //    Name = regionModel.Name,
        //    RegionImageUrl = regionModel.RegionImageUrl
        //};
        var regionDTO = mapper.Map<RegionDTO>(regionModel);
            return CreatedAtAction(nameof(GetById),new {id=regionDTO.Id}, regionDTO );
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody]  UpdateRegionDTO updateRegionDTO)
        {
            //var selectedRegion = new Region { Id = id};
            //selectedRegion.Name = updateRegionDTO.Name;
            //selectedRegion.Code = updateRegionDTO.Code;
            //selectedRegion.RegionImageUrl = updateRegionDTO.RegionImageUrl;
            
            var region = await _regionRepository.UpdateAsync(id, mapper.Map<Region>(updateRegionDTO));
            if (region == null) return NotFound();





        //RegionDTO regionDTO = new RegionDTO
        //{
        //    Id = region.Id,
        //    Code = region.Code,
        //    Name = region.Name,
        //    RegionImageUrl = region.RegionImageUrl
        //};

        return Ok(mapper.Map<RegionDTO>(region));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            var region = await _regionRepository.DeleteAsync(id);
            if (region == null) return NotFound();

            return Ok(mapper.Map<RegionDTO>(region));
        }
    }


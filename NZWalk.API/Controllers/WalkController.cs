
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalk.API.Data;
using NZWalk.API.Model;
using NZWalk.API.Model.DTO.Walk;
using NZWalk.API.Repository;

namespace NZWalk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;
        //private readonly AppDbContext _appDbContext;
        public WalkController(IMapper _mapper, IWalkRepository _walkRepository)
        {
            _mapper = mapper;
            _walkRepository = walkRepository;
            //_appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalks([FromQuery] string? filterOn=null, [FromQuery] string? filterQuery=null, [FromQuery] string? sorting = null, [FromQuery] bool? isAscending=null, [FromQuery] int pageNumber=1, [FromQuery] int pageSize=50)
        {

            //var walks = _appDbContext.Walk.Include("difficulty").Include("region").AsQueryable();

            //if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            //{
            //    if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
            //    {
            //        walks = walks.Where(x => x.Name.Contains(filterQuery));
            //    }

            //    if (filterOn.Equals("Description", StringComparison.OrdinalIgnoreCase))
            //    {
            //        walks = walks.Where(y => y.Description.Contains(filterQuery));
            //    }
            //}

            //if (string.IsNullOrWhiteSpace(sorting) == false && isAscending != null)
            //{
            //    if (sorting.Equals("Name", StringComparison.OrdinalIgnoreCase))
            //    {
            //        if (isAscending == true) walks = walks.OrderBy(x => x.Name);
            //        else walks = walks.OrderByDescending(x => x.Name);
            //    }

            //    if (sorting.Equals("Description", StringComparison.OrdinalIgnoreCase))
            //    {
            //        if (isAscending == true) walks = walks.OrderBy(x => x.Description);
            //        else walks = walks.OrderByDescending(x => x.Description);
            //    }
            //}

            //walks.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            //return Ok(await walks.ToListAsync());
            var walks = await walkRepository.GetAllAsync(filterOn, filterQuery, sorting, isAscending ?? true, pageNumber, pageSize);
            return Ok(mapper.Map<WalkDTO>(walks));

        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetWalkByID(Guid id)
        {
            var walk = await walkRepository.GetByIdAsync(id);
            return Ok(mapper.Map<WalkDTO>(walk));
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkDTO walkDTO)
        {
            var walk = await walkRepository.CreateAsync(mapper.Map<Walk>(walkDTO));
            return CreatedAtAction(nameof(GetWalkByID), new { id = walk.Id }, mapper.Map<WalkDTO>(walk)) ;
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkDTO updateDTO)
        {
            var walk = await walkRepository.UpdateAsync(id,mapper.Map<Walk>(updateDTO));
            return Ok(mapper.Map<WalkDTO>(walk));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var walk = await walkRepository.DeleteAsync(id);
            return Ok(mapper.Map<WalkDTO>(walk));
        }
    }
}

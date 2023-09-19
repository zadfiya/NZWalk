using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalk.API.Model;
using NZWalk.API.Model.DTO;
using NZWalk.API.Model.DTO.Difficulty;
using NZWalk.API.Repository;

namespace NZWalk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DifficultyController : ControllerBase
    {
        private readonly IDifficultyRepository _difficultyRepository;
        private readonly IMapper mapper;
        public DifficultyController(IDifficultyRepository difficultyRepository, IMapper _mapper)
        {
            _difficultyRepository = difficultyRepository;
            mapper = _mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var difficultyList = await _difficultyRepository.GetAllAsync();
            return Ok(mapper.Map<List<DifficultyDTO>>(difficultyList));
        
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetByID([FromRoute] Guid id)
        {
            var difficulty = await _difficultyRepository.GetByIdAsync(id);
            return Ok(mapper.Map<DifficultyDTO>(difficulty));
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create([FromBody] AddDifficultyDTO _difficulty)
        {
            var difficulty = await _difficultyRepository.CreateAsync(mapper.Map<Difficulty>(_difficulty));
            return CreatedAtAction(nameof(GetByID), new {id=difficulty.Id}, mapper.Map<DifficultyDTO>(difficulty));
        }

        [HttpPut]
        [AutoValidateAntiforgeryToken]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateDifficultyDTO _difficulty)
        {
            var difficulty = await _difficultyRepository.UpdateAsync(id, mapper.Map<Difficulty>(_difficulty));
            return Ok(mapper.Map<DifficultyDTO>(difficulty));
        }

        [HttpDelete]
        [AutoValidateAntiforgeryToken]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var difficulty = await _difficultyRepository.DeleteAsync(id);
            return Ok(mapper.Map<DifficultyDTO>(difficulty));
        }
    }
}

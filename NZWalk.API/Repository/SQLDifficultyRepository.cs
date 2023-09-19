using Microsoft.EntityFrameworkCore;
using NZWalk.API.Data;
using NZWalk.API.Model;

namespace NZWalk.API.Repository
{
    public class SQLDifficultyRepository : IDifficultyRepository
    {
        private readonly AppDbContext _appDbContext;
        public SQLDifficultyRepository(AppDbContext appDbContext) { _appDbContext = appDbContext; }
        public async Task<Difficulty> CreateAsync(Difficulty addDifficultyDTO)
        {
            await _appDbContext.Difficulty.AddAsync(addDifficultyDTO);
            await _appDbContext.SaveChangesAsync();
            return addDifficultyDTO;
        }

        public async Task<Difficulty> DeleteAsync(Guid id)
        {
            var difficultyModel = await _appDbContext.Difficulty.FindAsync(id);
            if (difficultyModel == null) { return null; }
            _appDbContext.Difficulty.Remove(difficultyModel);
            return difficultyModel;
        }

        public async Task<List<Difficulty>> GetAllAsync()
        {
            return await _appDbContext.Difficulty.ToListAsync();
        }

        public async Task<Difficulty> GetByIdAsync(Guid id)
        {
            return await _appDbContext.Difficulty.FindAsync(id);
        }

        public async Task<Difficulty> UpdateAsync(Guid id, Difficulty updateDifficultyDTO)
        {
            var difficultyModel = await _appDbContext.Difficulty.FindAsync(id);
            if (difficultyModel != null) { return null; }
            difficultyModel.Name = updateDifficultyDTO.Name;
            await _appDbContext.SaveChangesAsync();
            return difficultyModel;
        }
    }
}

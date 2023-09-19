using NZWalk.API.Model;

namespace NZWalk.API.Repository
{
    public interface IDifficultyRepository
    {
        public Task<List<Difficulty>> GetAllAsync();
        public Task<Difficulty> GetByIdAsync(Guid id);
        public Task<Difficulty> CreateAsync(Difficulty addDifficultyDTO);
        public Task<Difficulty> UpdateAsync(Guid id, Difficulty updateDifficultyDTO);
        public Task<Difficulty> DeleteAsync(Guid id);

    }
}

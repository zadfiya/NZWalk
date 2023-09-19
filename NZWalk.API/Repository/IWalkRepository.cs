using NZWalk.API.Model;

namespace NZWalk.API.Repository
{
    public interface IWalkRepository
    {
        public Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sorting = null,bool? isAscending=null, int pageNumber = 1, int pageSize = 100);
        public Task<Walk> GetByIdAsync(Guid id);
        public Task<Walk> CreateAsync(Walk addWalkDTO);
        public Task<Walk> UpdateAsync(Guid id, Walk updateWalkDTO);
        public Task<Walk> DeleteAsync(Guid id);
    }
}

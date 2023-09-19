using Microsoft.AspNetCore.Mvc;
using NZWalk.API.Model;
using NZWalk.API.Model.DTO;

namespace NZWalk.API.Repository
{
    public interface IRegionRepository
    {
       public Task<List<Region>> GetAllAsync();
        public Task<Region> GetByIdAsync( Guid id);
        public  Task<Region> CreateAsync( Region addRegionDTO);
        public Task<Region> UpdateAsync( Guid id,  Region updateRegionDTO);
        public Task<Region> DeleteAsync( Guid id);

    }
}

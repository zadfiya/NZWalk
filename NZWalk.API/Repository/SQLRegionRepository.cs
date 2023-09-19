using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NZWalk.API.Data;
using NZWalk.API.Model;
using NZWalk.API.Model.DTO;
using NZWalk.API.Repository;

namespace NZWalk.API.Repository
{
    public class SQLRegionRepository: IRegionRepository
    {
        private readonly AppDbContext _appDbContext;
        public SQLRegionRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await _appDbContext.Region.ToListAsync();
        }

        public async Task<Region> CreateAsync(Region region)
        {
           await  _appDbContext.Region.AddAsync(region);
            await _appDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region = await _appDbContext.Region.FindAsync(id);
            _appDbContext.Region.Remove(region);
            await _appDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> GetByIdAsync(Guid id)
        {
            return await _appDbContext.Region.FindAsync(id);
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            Region selectedRegion = _appDbContext.Region.Find(id);
            selectedRegion.Name = region.Name;
            selectedRegion.Code = region.Code;
            selectedRegion.RegionImageUrl = region.RegionImageUrl;
            await _appDbContext.SaveChangesAsync();
            return selectedRegion;
        }

    }
}

using Microsoft.EntityFrameworkCore;
using NZWalk.API.Data;
using NZWalk.API.Model;
using System.Linq;

namespace NZWalk.API.Repository;
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly AppDbContext _appDbContext;
        public SQLWalkRepository(AppDbContext appDbContext) {
            _appDbContext = appDbContext;

        }
        public async Task<Walk> CreateAsync(Walk addWalkDTO)
        {
             await _appDbContext.AddAsync(addWalkDTO);
            await _appDbContext.SaveChangesAsync();
            return addWalkDTO;
        }

        public async Task<Walk> DeleteAsync(Guid id)
        {
            var walkModel = await _appDbContext.Walk.FindAsync(id);
            if (walkModel == null) return null;
            _appDbContext.Walk.Remove(walkModel);
            await _appDbContext.SaveChangesAsync();
            return walkModel;
        }
        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sorting = null, bool? isAscending = null, int pageNumber = 1, int pageSize = 50)
        {
            var walks =  _appDbContext.Walk.Include("difficulty").Include("region").AsQueryable();

            if(string.IsNullOrWhiteSpace(filterOn) ==false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if(filterOn.Equals("Name",StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }

                if(filterOn.Equals("Description", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(y => y.Description.Contains(filterQuery));
                }
            }

            if (string.IsNullOrWhiteSpace(sorting) == false && isAscending != null)
            {
                if (sorting.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    if (isAscending == true) walks = walks.OrderBy(x => x.Name);
                    else walks = walks.OrderByDescending(x => x.Name);
                }

                if (sorting.Equals("Description", StringComparison.OrdinalIgnoreCase))
                {
                    if (isAscending == true) walks = walks.OrderBy(x => x.Description);
                    else walks = walks.OrderByDescending(x => x.Description);
                }
            }

            walks.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return await walks.ToListAsync();
            
        }

        public async Task<Walk> GetByIdAsync(Guid id)
        {
            return await _appDbContext.Walk.Include("Difficulty")
                .Include("Region")
                .FirstOrDefaultAsync(x=> x.Id==id);
        }

        public async Task<Walk> UpdateAsync(Guid id, Walk updateWalkDTO)
        {
            var walkModel = await _appDbContext.Walk.FindAsync(id);
            if (walkModel == null) return null;
            //walkModel.region = updateWalkDTO.region;
            walkModel.ImageURL = updateWalkDTO.ImageURL;
            walkModel.Description = updateWalkDTO.Description;
            walkModel.LengthInKm = updateWalkDTO.LengthInKm;
            walkModel.Name = updateWalkDTO.Name;
            walkModel.DifficultyId = updateWalkDTO.DifficultyId;
            walkModel.RegionId = updateWalkDTO.RegionId;

            await _appDbContext.SaveChangesAsync();
            return walkModel;
        }
    }


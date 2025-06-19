using AutoMapper.Configuration.Annotations;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public class SQLResgionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLResgionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region; // Return the created region
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.id == id);
            if (existingRegion == null)
            {
                return null; 
            }
            dbContext.Regions.Remove(existingRegion);
            await dbContext.SaveChangesAsync();
            return existingRegion;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetByIdAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x => x.id == id);
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
           var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.id == id);
            if (existingRegion == null)
            {
                return null; // or throw an exception
            }
            existingRegion.code = region.code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;
            dbContext.Regions.Update(existingRegion);
            await dbContext.SaveChangesAsync();
            return existingRegion;
        }
    }
}

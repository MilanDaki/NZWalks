using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLWalkRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Walks> CreateAsync(Walks walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walks?> DeleteAsync(Guid id)
        {
            var existingWalk =  await dbContext.Walks.FirstOrDefaultAsync(w => w.id == id);
      
            if (existingWalk == null)
            {
                return null;
            }
            dbContext.Walks.Remove(existingWalk);
            await dbContext.SaveChangesAsync();
            return existingWalk; 
        }

        public async Task<List<Walks>> GetAllAsync()
        {
           return await dbContext.Walks.ToListAsync(); 
        }

        public async Task<Walks?> GetByIdAsync(Guid id)
        {
           return await dbContext.Walks.FirstOrDefaultAsync(w => w.id == id);
        }

        public async Task<Walks?> UpdateAsync(Guid id, Walks walk)
        {
           var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(w => w.id == id);
            if (existingWalk == null)
            {
                return null; // Walk not found
            }
            // Update properties
            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.RegionId = walk.RegionId;
            existingWalk.DifficultyId = walk.DifficultyId;

            await dbContext.SaveChangesAsync();
            return existingWalk;
        }
    }
}

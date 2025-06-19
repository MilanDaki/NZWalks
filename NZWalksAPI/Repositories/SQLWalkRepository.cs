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

        public async Task<List<Walks>> GetAllAsync(string? filteron = null, string? filterquery = null,
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pagesize = 1000)
        {
            var walks = dbContext.Walks.AsQueryable();
            // filtering
            if (string.IsNullOrWhiteSpace(filteron) == false && string.IsNullOrWhiteSpace(filterquery) == false)
            {
                if (filteron.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterquery));
                } 
            }

            //sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name); 
                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            // Pagination 
            var SkipResults = (pageNumber - 1) * pagesize;

            return await walks.Skip(SkipResults).Take(pagesize).ToListAsync();
            // return await dbContext.Walks.ToListAsync(); 
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

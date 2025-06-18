using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public interface IRegionRepository
    {
        Task<Region> CreateAsync(Region region);
 
        Task<Region> DeleteAsync(Guid id);
        Task<Region> GetByIdAsync(Guid id);
        Task<Region> UpdateAsync(Guid id, Region region);
        Task<List<Region>> GetAllAsync();
    }
}

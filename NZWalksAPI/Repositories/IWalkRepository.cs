using NZWalksAPI.Models.Domain;
using System.Runtime.InteropServices;

namespace NZWalksAPI.Repositories
{
    public interface IWalkRepository
    {
        Task<Walks> CreateAsync(Walks walk);

        Task<List<Walks>> GetAllAsync(string? filteron = null, string? filterquery = null, 
             string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pagesize = 1000);

        Task<Walks?> GetByIdAsync(Guid id);

        Task<Walks?> UpdateAsync(Guid id, Walks walk);
       
        Task<Walks?> DeleteAsync(Guid id);
    }
}

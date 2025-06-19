using NZWalksAPI.Models.Domain;
using System.Runtime.InteropServices;

namespace NZWalksAPI.Repositories
{
    public interface IWalkRepository
    {
        Task<Walks> CreateAsync(Walks walk);

        Task<List<Walks>> GetAllAsync();

        Task<Walks?> GetByIdAsync(Guid id);

        Task<Walks?> UpdateAsync(Guid id, Walks walk);
       
        Task<Walks?> DeleteAsync(Guid id);
    }
}

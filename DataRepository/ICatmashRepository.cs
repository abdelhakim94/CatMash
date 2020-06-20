using System.Threading.Tasks;
using System.Collections.Generic;
using Catmash.EntityModel;

namespace Catmash.DataRepository
{
    public interface ICatmashRepository
    {
        Task<Image> CreateAsync(Image image);
        Task<IEnumerable<Image>> RetrieveAllAsync();
        Task<Image> RetrieveAsync(string id);
        Task<Image> RetrieveAsync(int index);
        Task<Image> UpdateAsync(string id, Image image);
        Task<bool?> DeleteAsync(string id);
        Task<int> CountAsync();
        Task<bool?> SaveChangesAsync();
    }
}
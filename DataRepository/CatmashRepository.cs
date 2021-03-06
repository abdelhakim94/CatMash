using System.Linq;
using Catmash.EntityModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Catmash.DataRepository
{
    public class CatmashRepository : ICatmashRepository
    {
        private readonly CatmashEntities context;

        public CatmashRepository(CatmashEntities context) => this.context = context;

        public async Task<Image> CreateAsync(Image image)
        {
            EntityEntry<Image> added = await context.Images.AddAsync(image);
            int affected = await context.SaveChangesAsync();
            if (affected == 1) return image;
            else return null;
        }

        public Task<IEnumerable<Image>> RetrieveAllAsync()
        {
            return Task.Run<IEnumerable<Image>>(() => context.Images);
        }

        public Task<Image> RetrieveAsync(string id)
        {
            return Task.Run<Image>(() => context.Images
                                                .Where(img => img.Id == id)
                                                .SingleOrDefault());
        }

        public Task<Image> RetrieveAsync(int index)
        {
            return Task.Run<Image>(() => context.Images
                                                .OrderBy(img => img.Id)
                                                .Skip(index)
                                                .Take(1)
                                                .SingleOrDefault());
        }

        public async Task<Image> UpdateAsync(string id, Image image)
        {
            context.Images.Update(image);
            int affected = await context.SaveChangesAsync();
            if (affected == 1) return image;
            else return null;
        }

        public async Task<bool?> DeleteAsync(string id)
        {
            Image image = context.Images.Find(id);
            context.Images.Remove(image);
            int affected = await context.SaveChangesAsync();
            if (affected == 1) return true;
            else return null;
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() => context.Images.Count());
        }

        public async Task<bool?> SaveChangesAsync()
        {
            int affected = await context.SaveChangesAsync();
            if (affected != 0) return true;
            else return null;
        }
    }
}
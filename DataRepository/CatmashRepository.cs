using System.Linq;
using Catmash.EntityModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Catmash.DataRepository
{
    public class CatmashRepository : ICatmashRepository
    {
        private CatmashEntities context;

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
                                                .Select((img, idx) => new { image = img, index = idx })
                                                .Where(x => x.index == index)
                                                .Select(x => x.image)
                                                .SingleOrDefault());
        }
    }
}
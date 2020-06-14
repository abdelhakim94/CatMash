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
    }
}
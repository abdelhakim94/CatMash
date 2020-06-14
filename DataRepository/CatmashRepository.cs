using Catmash.EntityModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataRepository
{
    public class CatmashRepository
    {
        private CatmashEntities context;

        public CatmashRepository(CatmashEntities context) => this.context = context;
    }
}
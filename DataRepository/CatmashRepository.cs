using Catmash.EntityModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catmash.DataRepository
{
    public class CatmashRepository : ICatmashRepository
    {
        private CatmashEntities context;

        public CatmashRepository(CatmashEntities context) => this.context = context;
    }
}
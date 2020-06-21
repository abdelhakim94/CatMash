using System.Linq;
using System.Collections.Generic;

namespace Catmash.EntityModel
{
    public class ImagePropertiesComparer : IEqualityComparer<Image>
    {
        public bool Equals(Image x, Image y)
        {
            if (x == null && y == null) return true;
            else if (x == null || y == null) return false;
            else return ((x.Id.Equals(y.Id))
                && (x.Url.Equals(y.Url))
                && (x.Score == y.Score)
                && (x.Votes == y.Votes));
        }
        public int GetHashCode(Image obj)
        {
            if (obj is null) return 0;
            return obj.Id.Aggregate<char, int>(0, (i, c) => i + c)
                + obj.Url.Aggregate<char, int>(0, (i, c) => i + c)
                + (int)obj.Score
                + (int)obj.Votes;
        }
    }
}
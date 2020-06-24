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
        public int GetHashCode(Image img)
        {
            if (img is null) return 0;
            return img.Id.Aggregate<char, int>(0, (i, c) => i + c)
                + img.Url.Aggregate<char, int>(0, (i, c) => i + c)
                + (int)img.Score
                + (int)img.Votes;
        }
    }
}
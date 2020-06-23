using System.Collections.Generic;
using Catmash.Web.Models;
using Catmash.EntityModel;

namespace Catmash.Tests.Web
{
    public class ImagePairComparer : IEqualityComparer<ImagePair>
    {
        private IEqualityComparer<Image> comparer;

        public ImagePairComparer(IEqualityComparer<Image> comparer) => this.comparer = comparer;

        public bool Equals(ImagePair x, ImagePair y)
        {
            return (comparer.Equals(x.first, y.first) && comparer.Equals(x.second, y.second));
        }

        public int GetHashCode(ImagePair x)
        {
            return comparer.GetHashCode(x.first) + comparer.GetHashCode(x.second);
        }
    }
}
using System.Diagnostics.CodeAnalysis;

namespace Day5
{
    internal class RangeLongComparer : IEqualityComparer<RangeLong>
    {
        public bool Equals(RangeLong? x, RangeLong? y)
        {
            return x.Start == y.Start && x.End == y.End;
        }

        public int GetHashCode([DisallowNull] RangeLong obj)
        {
            return obj.Start.GetHashCode() ^ obj.End.GetHashCode();
        }
    }
}

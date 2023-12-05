using System.Diagnostics.CodeAnalysis;

namespace Day5
{
    internal class RangeLong : IEquatable<RangeLong>
    {
        public readonly long Start;
        public readonly long End;

        public RangeLong(long start, long end)
        {
            Start = start;
            End = end;
        }

        public bool Equals(RangeLong? other)
        {
            return other != null && Start == other.Start && End == other.End;
        }
    }
}

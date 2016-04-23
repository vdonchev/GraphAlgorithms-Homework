namespace _05.BreakCycles
{
    using System;

    public class Edge : IComparable<Edge>
    {
        public string Start { get; set; }

        public string End { get; set; }

        public int CompareTo(Edge other)
        {
            int comparer = string.Compare(this.Start, other.Start, StringComparison.Ordinal);
            if (comparer == 0)
            {
                return string.Compare(this.End, other.End, StringComparison.Ordinal);
            }

            return comparer;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}", this.Start, this.End);
        }
    }
}
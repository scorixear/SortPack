using SortPack.Domain.Abstractions;
using SortPack.Domain.Interfaces;

namespace SortPack.Infrastructure.NonComparison
{
    public class RadixLSDSort : SortAlgorithm
    {
        public RadixLSDSort(IStatisticCounter statisticCounter) : base(statisticCounter)
        {
        }

        public override IList<T> Sort<T>(IList<T> collection)
        {
            List<T> result = [.. collection];
            return SortInPlace(result);
        }

        public override IList<T> SortInPlace<T>(IList<T> collection) where T : byte, short, ushort, int, uint, long, ulong
        {
            if (collection.Count < 2) return collection;
            int n = collection.Count;
            int[] count = new int[256];
            int[] temp = new int[n];
            int[] indexes = new int[256];
            int shift = 0;
            int mask = 0xFF;
            int bytes = 4;
            for (int i = 0; i < bytes; i++)
            {
                for (int j = 0; j < count.Length; j++)
                {
                    count[j] = 0;
                }

                for (int j = 0; j < n; j++)
                {
                    int c = (collection[j].GetHashCode() >> shift) & mask;
                    count[c]++;
                }

                indexes[0] = 0;
                for (int j = 1; j < count.Length; j++)
                {
                    indexes[j] = indexes[j - 1] + count[j - 1];
                }

                for (int j = 0; j < n; j++)
                {
                    int c = (collection[j].GetHashCode() >> shift) & mask;
                    temp[indexes[c]++] = j;
                }

                for (int j = 0; j < n; j++)
                {
                    collection[j] = collection[temp[j]];
                }

                shift += 8;
            }

            return collection;
        }
    }
}

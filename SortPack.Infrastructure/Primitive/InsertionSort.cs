using SortPack.Domain.Abstractions;
using SortPack.Domain.Interfaces;

namespace SortPack.Infrastructure.Primitive
{
    public class InsertionSort : SortAlgorithm
    {
        public InsertionSort() { }
        public InsertionSort(IStatisticCounter statisticCounter) : base(statisticCounter) { }

        public override IList<T> Sort<T>(IList<T> collection)
        {
            List<T> result = [.. collection];

            SortInPlace(result);
            return result;
        }

        public override IList<T> SortInPlace<T>(IList<T> collection)
        {
            for (int i = 1; i < collection.Count; i++)
            {
                T key = collection[i];
                StatisticCounter?.IncrementReadOperations();
                int j = i - 1;

                while (j >= 0 && GreaterThan(collection[j], key))
                {
                    StatisticCounter?.IncrementReadOperations();
                    j--;
                }

                if (j + 1 == i)
                {
                    continue;
                }

                collection.RemoveAt(i);
                collection.Insert(j + 1, key);
                StatisticCounter?.IncrementWriteOperations((ulong)(i - j));
            }
            return collection;
        }
    }
}

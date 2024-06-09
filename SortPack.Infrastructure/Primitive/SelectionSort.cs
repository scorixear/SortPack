using SortPack.Domain;
using SortPack.Domain.Interfaces;

namespace SortPack.Infrastructure.Primitive
{
    public class SelectionSort : SortInPlaceAlgorithm
    {
        public SelectionSort()
        {
        }

        public SelectionSort(IStatisticCounter statisticCounter) : base(statisticCounter)
        {
        }



        public override IList<T> Sort<T>(IList<T> collection)
        {
            List<T> result = [.. collection];

            SortInPlace(result);
            return result;
        }



        public override IList<T> SortInPlace<T>(IList<T> collection)
        {
            for (int i = 0; i < collection.Count - 1; i++)
            {
                int minIndex = i;
                T key = collection[minIndex];
                StatisticCounter?.IncrementReadOperations();

                for (int j = i + 1; j < collection.Count; j++)
                {
                    T other = collection[j];
                    StatisticCounter?.IncrementReadOperations();
                    if (LessThan(other, key))
                    {
                        minIndex = j;
                        key = other;
                    }
                }

                if (minIndex != i)
                {
                    StatisticCounter?.IncrementReadOperations();
                    Swap(collection, i, collection[i], minIndex, key);
                }
            }
            return collection;
        }
    }
}

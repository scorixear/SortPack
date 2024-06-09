using SortPack.Domain.Abstractions;
using SortPack.Domain.Interfaces;

namespace SortPack.Infrastructure.Primitive
{
    public class BubbleSort : SortAlgorithm
    {
        public BubbleSort() : base()
        {
        }

        public BubbleSort(IStatisticCounter statisticCounter) : base(statisticCounter)
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
            if (collection.Count < 2)
            {
                return collection;
            }
            bool swapped = false;
            int rounds = 0;
            do
            {
                swapped = false;
                T prev = collection[0];
                StatisticCounter?.IncrementReadOperations();
                for (int j = 1; j < collection.Count - rounds; j++)
                {
                    T next = collection[j];
                    StatisticCounter?.IncrementReadOperations();
                    if (LessThan(prev, next))
                    {
                        prev = next;
                        continue;
                    }
                    Swap(collection, j - 1, prev, j, next);
                    swapped = true;
                }
                rounds++;
            } while (swapped);
            return collection;
        }
    }
}

using SortPack.Domain.Abstractions;
using SortPack.Domain.Interfaces;

namespace SortPack.Infrastructure.Primitive;

public class ShakerSort : SortAlgorithm
{
    public ShakerSort()
    {

    }

    public ShakerSort(IStatisticCounter statisticCounter) : base(statisticCounter)
    {

    }

    public override IList<T> SortInPlace<T>(IList<T> collection)
    {
        if (collection.Count < 2)
        {
            return collection;
        }
        bool swapped = true;
        int start = 0;
        int end = collection.Count;
        while (swapped)
        {
            swapped = false;
            T prev = collection[start];
            StatisticCounter?.IncrementReadOperations();
            for (int i = start; i < end - 1; i++)
            {
                T next = collection[i + 1];
                StatisticCounter?.IncrementReadOperations();
                if (GreaterThan(prev, next))
                {
                    Swap(collection, i, prev, i + 1, next);
                    swapped = true;
                }
                else
                {
                    prev = next;
                }
            }

            if (!swapped)
            {
                break;
            }

            swapped = false;
            end--;
            T next2 = collection[end];
            StatisticCounter?.IncrementReadOperations();
            for (int i = end - 1; i >= start; i--)
            {
                T prev2 = collection[i];
                StatisticCounter?.IncrementReadOperations();
                if (GreaterThan(prev2, next2))
                {
                    Swap(collection, i, prev2, i + 1, next2);
                    swapped = true;
                }
                else
                {
                    next2 = prev2;
                }
            }

            start++;
        }
        return collection;
    }
}

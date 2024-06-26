﻿using SortPack.Domain.Abstractions;
using SortPack.Domain.Interfaces;

namespace SortPack.Infrastructure.Primitive;

public class BubbleSort : SortAlgorithm
{
    public BubbleSort() : base()
    {
    }

    public BubbleSort(IStatisticCounter statisticCounter) : base(statisticCounter)
    {
    }

    public override IList<T> SortInPlace<T>(IList<T> collection, CancellationToken? cancellationToken = null)
    {
        if (collection.Count < 2)
        {
            return collection;
        }

        int rounds = 0;
        bool swapped;
        do
        {
            cancellationToken?.ThrowIfCancellationRequested();
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

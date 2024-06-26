﻿using SortPack.Domain.Abstractions;
using SortPack.Domain.Interfaces;

namespace SortPack.Infrastructure.Primitive;

public class DoubleSelectionSort : SortAlgorithm
{
    public DoubleSelectionSort() { }
    public DoubleSelectionSort(IStatisticCounter statisticCounter) : base(statisticCounter) { }

    public override IList<T> SortInPlace<T>(IList<T> collection, CancellationToken? cancellationToken = null)
    {
        int length = collection.Count;

        for (int left = 0; left < length / 2; left++)
        {
            cancellationToken?.ThrowIfCancellationRequested();
            int minIndex = left;
            T minValue = collection[left];

            int right = length - (left + 1);
            int maxIndex = right;
            T maxValue = collection[right];

            StatisticCounter?.IncrementReadOperations(2);

            for (int i = left; i <= right; i++)
            {
                T value = collection[i];
                StatisticCounter?.IncrementReadOperations();
                if (GreaterThan(minValue, value))
                {
                    minValue = value;
                    minIndex = i;
                }
                if (LessThan(maxValue, value))
                {
                    maxValue = value;
                    maxIndex = i;
                }
            }

            if (left == maxIndex)
            {
                maxIndex = minIndex;
            }

            Swap(collection, left, minIndex);
            Swap(collection, right, maxIndex);
        }
        return collection;
    }
}

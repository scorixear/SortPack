using SortPack.Domain.Abstractions;
using SortPack.Domain.Interfaces;
using System.Runtime.CompilerServices;

namespace SortPack.Infrastructure.DivideAndConquer;

public class MergeSort : RecursiveSortAlgorithm
{
    public MergeSort()
    {
    }

    public MergeSort(IStatisticCounter statisticCounter) : base(statisticCounter)
    {
    }

    public override IList<T> SortInPlace<T>(IList<T> collection, CancellationToken? cancellationToken = null)
    {
        int length = collection.Count;

        List<T?> auxiliary = Enumerable.Repeat(default(T), length).ToList();
        for (int currSize = 1; currSize < length; currSize *= 2)
        {
            for (int leftStart = 0; leftStart < length - 1; leftStart += 2 * currSize)
            {
                cancellationToken?.ThrowIfCancellationRequested();
                int mid = Math.Min(leftStart + currSize - 1, length - 1);
                int rightEnd = Math.Min(leftStart + (2 * currSize) - 1, length - 1);

                int i = leftStart;
                int j = mid + 1;
                int k = leftStart;

                while (i <= mid && j <= rightEnd)
                {
                    if (LessThanOrEqual(collection, i, j))
                    {
                        auxiliary[k] = collection[i];
                        StatisticCounter?.IncrementReadOperations();
                        i++;
                    }
                    else
                    {
                        auxiliary[k] = collection[j];
                        StatisticCounter?.IncrementReadOperations();
                        j++;
                    }
                    k++;
                }

                while (i <= mid)
                {
                    auxiliary[k] = collection[i];
                    StatisticCounter?.IncrementReadOperations();
                    i++;
                    k++;
                }

                while (j <= rightEnd)
                {
                    auxiliary[k] = collection[j];
                    StatisticCounter?.IncrementReadOperations();
                    j++;
                    k++;
                }

                for (int l = leftStart; l <= rightEnd; l++)
                {
                    collection[l] = auxiliary[l]!;
                }
                StatisticCounter?.IncrementWriteOperations((ulong)(rightEnd - leftStart + 1));
            }
        }
        return collection;
    }

    public override IList<T> RecursiveSortInPlace<T>(IList<T> collection, CancellationToken? cancellationToken = null)
    {
        if (collection.Count < 2)
        {
            return collection;
        }
        List<T?> auxiliary = Enumerable.Repeat(default(T), collection.Count).ToList();
        RecursiveCall(collection, auxiliary, 0, collection.Count - 1, cancellationToken ?? CancellationToken.None);
        StatisticCounter?.IncrementWriteOperations((ulong)auxiliary.Count);
        return collection;
    }

    private void RecursiveCall<T>(IList<T> collection, IList<T?> auxiliary, int left, int right, CancellationToken cancellationToken) where T : IComparable<T>
    {
        cancellationToken.ThrowIfCancellationRequested();
        RuntimeHelpers.EnsureSufficientExecutionStack();

        if (left == right)
        {
            return;
        }

        int mid = (left + right) / 2;

        RecursiveCall(collection, auxiliary, left, mid, cancellationToken);
        RecursiveCall(collection, auxiliary, mid + 1, right, cancellationToken);

        int i = left;
        int j = mid + 1;
        int k = left;
        while (i <= mid && j <= right)
        {
            if (LessThanOrEqual(collection, i, j))
            {
                auxiliary[k] = collection[i];
                StatisticCounter?.IncrementReadOperations();
                i++;
            }
            else
            {
                auxiliary[k] = collection[j];
                StatisticCounter?.IncrementReadOperations();
                j++;
            }
            k++;
        }

        while (i <= mid)
        {
            auxiliary[k] = collection[i];
            StatisticCounter?.IncrementReadOperations();
            i++;
            k++;
        }

        while (j <= right)
        {
            auxiliary[k] = collection[j];
            StatisticCounter?.IncrementReadOperations();
            j++;
            k++;
        }

        for (int l = left; l <= right; l++)
        {
            collection[l] = auxiliary[l]!;
        }
        StatisticCounter?.IncrementWriteOperations((ulong)(right - left + 1));
    }

}

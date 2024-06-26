﻿using SortPack.Domain.Abstractions;
using SortPack.Domain.Interfaces;
using System.Runtime.CompilerServices;

namespace SortPack.Infrastructure.DivideAndConquer;

public class QuickSort : RecursiveSortAlgorithm
{
    public QuickSort()
    {
    }

    public QuickSort(IStatisticCounter statisticCounter) : base(statisticCounter)
    {
    }
    public IList<T> Sort<T>(IList<T> collection, Random random, CancellationToken? cancellationToken = null) where T : IComparable<T>
    {
        List<T> result = [.. collection];
        return SortInPlace(result, random, cancellationToken);
    }

    public override IList<T> SortInPlace<T>(IList<T> collection, CancellationToken? cancellationToken = null)
    {
        return SortInPlace(collection, new Random(), cancellationToken);
    }

    public IList<T> SortInPlace<T>(IList<T> collection, Random random, CancellationToken? cancellationToken = null) where T : IComparable<T>
    {
        if (collection.Count < 2)
        {
            return collection;
        }

        int left = 0;
        int right = collection.Count - 1;
        Stack<int> stack = new();
        while (true)
        {
            cancellationToken?.ThrowIfCancellationRequested();
            while (left < right)
            {
                T pivot = collection[random.Next(left, right)];
                stack.Push(right);

                int mid = left;
                while (true)
                {
                    cancellationToken?.ThrowIfCancellationRequested();
                    while (LessThan(collection[mid], pivot))
                    {
                        mid++;
                    }
                    while (GreaterThan(collection[right], pivot))
                    {
                        right--;
                    }
                    if (mid >= right)
                    {
                        break;
                    }
                    Swap(collection, mid, right);
                }
            }
            if (stack.Count == 0)
            {
                break;
            }
            left = right + 1;
            right = stack.Pop();
        }
        return collection;
    }

    public override IList<T> RecursiveSortInPlace<T>(IList<T> collection, CancellationToken? cancellationToken = null)
    {
        if (collection.Count < 2)
        {
            return collection;
        }

        int left = 0;
        int right = collection.Count - 1;

        RecursiveCall(collection, left, right, cancellationToken);

        return collection;
    }

    private void RecursiveCall<T>(IList<T> collection, int left, int right, CancellationToken? cancellationToken = null) where T : IComparable<T>
    {
        while (true)
        {
            //Console.Error.WriteLine($"left: {left}, right: {right}");
            cancellationToken?.ThrowIfCancellationRequested();
            RuntimeHelpers.EnsureSufficientExecutionStack();

            if (left >= right)
            {
                return;
            }

            int pi = Partition(collection, left, right);
            RecursiveCall(collection, left, pi - 1, cancellationToken);
            left = pi + 1;
        }
    }

    private int Partition<T>(IList<T> collection, int left, int right) where T : IComparable<T>
    {
        T pivot = collection[right];
        StatisticCounter?.IncrementReadOperations();
        int i = left - 1;

        for (int j = left; j < right; j++)
        {
            StatisticCounter?.IncrementReadOperations();
            if (LessThan(collection[j], pivot))
            {
                i++;
                Swap(collection, i, j);
            }
        }

        Swap(collection, i + 1, right);
        return i + 1;
    }

}

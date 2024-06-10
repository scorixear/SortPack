using SortPack.Domain.Abstractions;
using SortPack.Domain.Interfaces;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace SortPack.Infrastructure.NonComparison;

// ReSharper disable once InconsistentNaming
public sealed class RadixMSDSort : RecursiveStringAndNumberSortAlgorithm
{
    public RadixMSDSort()
    {
    }

    public RadixMSDSort(IStatisticCounter statisticCounter) : base(statisticCounter)
    {
    }

    public override IList<T> SortInPlace<T>(IList<T> collection)
    {
        if (collection.Count == 0)
        {
            return collection;
        }

        int byteWidth = GetByteWidth(typeof(T));
        Queue<(int start, int end, int byteIndex)> callQueue = new();

        callQueue.Enqueue((0, collection.Count, byteWidth - 1));

        while (callQueue.Count > 0)
        {
            (int start, int end, int byteIndex) = callQueue.Dequeue();
            if (start >= end || byteIndex < 0)
            {
                continue;
            }
            List<T>[] buckets = new List<T>[256];

            for (int i = 0; i < buckets.Length; i++)
            {
                buckets[i] = [];
            }

            for (int i = start; i < end; i++)
            {
                T value = collection[i];
                byte bucketIndex = GetByteValue(value, byteIndex);
                buckets[bucketIndex].Add(value);
            }
            StatisticCounter?.IncrementReadOperations((ulong)(end - start));

            int index = start;
            foreach (List<T> bucket in buckets)
            {
                if (bucket.Count <= 0)
                {
                    continue;
                }

                foreach (T value in bucket)
                {
                    collection[index++] = value;
                }
                StatisticCounter?.IncrementWriteOperations((ulong)bucket.Count);
                callQueue.Enqueue((index - bucket.Count, index, byteIndex - 1));
            }
        }

        return collection;
    }

    public override IList<string> SortInPlace(IList<string> collection)
    {
        if (collection.Count == 0)
        {
            return collection;
        }
        Queue<(int start, int end, int charIndex)> callQueue = new();

        callQueue.Enqueue((0, collection.Count, 0));

        while (callQueue.Count > 0)
        {
            (int start, int end, int charIndex) = callQueue.Dequeue();
            if (start >= end || charIndex < 0)
            {
                continue;
            }

            List<string>[] buckets = new List<string>[257];
            for (int i = 0; i < buckets.Length; i++)
            {
                buckets[i] = [];
            }

            for (int i = start; i < end; i++)
            {
                string value = collection[i];
                int bucketIndex = GetCharIndex(value, charIndex);
                buckets[bucketIndex].Add(value);
            }
            StatisticCounter?.IncrementReadOperations((ulong)(end - start));

            int index = start;
            for (int i = 0; i < buckets.Length; i++)
            {
                if (buckets[i].Count <= 0)
                {
                    continue;
                }

                for (int j = 0; j < buckets[i].Count; j++)
                {
                    collection[index++] = buckets[i][j];
                }
                StatisticCounter?.IncrementWriteOperations((ulong)buckets[i].Count);

                if (i > 0)
                {
                    callQueue.Enqueue((index - buckets[i].Count, index, charIndex + 1));
                }
            }
        }

        return collection;
    }

    public override IList<string> RecursiveSortInPlace(IList<string> collection, CancellationToken? cancellationToken = null)
    {
        if (collection.Count == 0)
        {
            return collection;
        }
        RecursiveStringCall(collection, 0, collection.Count, 0, cancellationToken ?? CancellationToken.None);
        return collection;
    }

    public override IList<T> RecursiveSortInPlace<T>(IList<T> collection, CancellationToken? cancellationToken = null)
    {
        if (collection.Count == 0)
        {
            return collection;
        }

        int byteWidth = GetByteWidth(typeof(T));
        RecursiveCall(collection, 0, collection.Count, byteWidth - 1, cancellationToken ?? CancellationToken.None);
        return collection;
    }

    private void RecursiveStringCall(IList<string> collection, int start, int end, int charIndex, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        RuntimeHelpers.EnsureSufficientExecutionStack();
        if (start >= end || charIndex < 0)
        {
            return;
        }

        List<string>[] buckets = new List<string>[257];
        for (int i = 0; i < buckets.Length; i++)
        {
            buckets[i] = [];
        }

        for (int i = start; i < end; i++)
        {
            string value = collection[i];
            int bucketIndex = GetCharIndex(value, charIndex);
            buckets[bucketIndex].Add(value);
        }
        StatisticCounter?.IncrementReadOperations((ulong)(end - start));

        int index = start;
        for (int i = 0; i < buckets.Length; i++)
        {
            if (buckets[i].Count <= 0)
            {
                continue;
            }

            for (int j = 0; j < buckets[i].Count; j++)
            {
                collection[index++] = buckets[i][j];
            }
            StatisticCounter?.IncrementWriteOperations((ulong)buckets[i].Count);

            if (i > 0)
            {
                RecursiveStringCall(collection, index - buckets[i].Count, index, charIndex + 1, cancellationToken);
            }
        }
    }

    private void RecursiveCall<T>(IList<T> collection, int start, int end, int byteIndex, CancellationToken cancellationToken) where T : IComparable<T>, IBitwiseOperators<T, T, T>, IShiftOperators<T, int, T>
    {
        cancellationToken.ThrowIfCancellationRequested();
        RuntimeHelpers.EnsureSufficientExecutionStack();
        if (start >= end || byteIndex < 0)
        {
            return;
        }
        List<T>[] buckets = new List<T>[256];

        for (int i = 0; i < buckets.Length; i++)
        {
            buckets[i] = [];
        }

        for (int i = start; i < end; i++)
        {
            T value = collection[i];
            StatisticCounter?.IncrementReadOperations();
            byte bucketIndex = GetByteValue(value, byteIndex);
            buckets[bucketIndex].Add(value);
        }

        int index = start;
        foreach (List<T> bucket in buckets)
        {
            if (bucket.Count <= 0)
            {
                continue;
            }

            foreach (T value in bucket)
            {
                collection[index++] = value;
                StatisticCounter?.IncrementWriteOperations();
            }
            RecursiveCall(collection, index - bucket.Count, index, byteIndex - 1, cancellationToken);
        }
    }

    private static int GetCharIndex(string value, int charIndex)
    {
        return charIndex >= value.Length ? 0 : value[charIndex] + 1;
    }

    private static int GetByteWidth(Type type)
    {
        return type == typeof(byte) || type == typeof(sbyte)
            ? 1
            : type == typeof(ushort) || type == typeof(short)
            ? 2
            : type == typeof(uint) || type == typeof(int)
            ? 4
            : type == typeof(ulong) || type == typeof(long) ? 8 : throw new ArgumentException("Type not supported");
    }
}

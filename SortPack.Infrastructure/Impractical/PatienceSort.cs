using SortPack.Domain.Abstractions;
using SortPack.Domain.Interfaces;

namespace SortPack.Infrastructure.Impractical;

public class PatienceSort : SortAlgorithm
{
    public PatienceSort() { }
    public PatienceSort(IStatisticCounter statisticCounter) : base(statisticCounter) { }
    public override IList<T> SortInPlace<T>(IList<T> collection, CancellationToken? cancellationToken = null)
    {
        List<Stack<T>> stacks = [];
        foreach (T value in collection)
        {
            cancellationToken?.ThrowIfCancellationRequested();
            int stackIndex = BinarySearch(stacks, value);

            if (stackIndex >= 0)
            {
                stacks[stackIndex].Push(value);
                continue;
            }
            Stack<T> newStack = new();
            newStack.Push(value);
            stacks.Add(newStack);
        }
        StatisticCounter?.IncrementReadOperations((ulong)collection.Count);

        MinHeapPriorityQueue(collection, stacks);

        return collection;
    }

    private int BinarySearch<T>(List<Stack<T>> stacks, T value) where T : IComparable<T>
    {
        int left = 0;
        int right = stacks.Count - 1;
        int lastFound = -1;
        while (left <= right)
        {
            int middle = left + ((right - left) / 2);
            if (LessThanOrEqual(stacks[middle].Peek(), value))
            {
                left = middle + 1;
            }
            else
            {
                lastFound = middle;
                right = middle - 1;
            }
        }
        return lastFound;
    }

    private void MinHeapPriorityQueue<T>(IList<T> collection, List<Stack<T>> minHeap) where T : IComparable<T>
    {
        int index = 0;
        while (minHeap.Count > 0)
        {
            collection[index++] = minHeap[0].Pop();
            if (minHeap[0].Count == 0)
            {
                // get last element and remove it
                if (minHeap.Count <= 1)
                {
                    break;
                }
                minHeap[0] = minHeap[^1];
                minHeap.RemoveAt(minHeap.Count - 1);
            }
            // heapify
            int parentIndex = 0;
            while (true)
            {
                int leftChildIndex = (2 * parentIndex) + 1;
                int rightChildIndex = (2 * parentIndex) + 2;
                int smallest = parentIndex;
                if (leftChildIndex < minHeap.Count && LessThan(minHeap[leftChildIndex].Peek(), minHeap[smallest].Peek()))
                {
                    smallest = leftChildIndex;
                }
                if (rightChildIndex < minHeap.Count && LessThan(minHeap[rightChildIndex].Peek(), minHeap[smallest].Peek()))
                {
                    smallest = rightChildIndex;
                }
                if (smallest == parentIndex)
                {
                    break;
                }
                (minHeap[parentIndex], minHeap[smallest]) = (minHeap[smallest], minHeap[parentIndex]);
                parentIndex = smallest;
            }
        }
        StatisticCounter?.IncrementWriteOperations((ulong)collection.Count);
    }
}

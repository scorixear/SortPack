using SortPack.Domain.Abstractions;
using SortPack.Domain.Interfaces;
using System.Runtime.CompilerServices;

namespace SortPack.Infrastructure.Advanced
{
    public class HeapSort : RecursiveSortAlgorithm
    {
        public HeapSort()
        {
        }

        public HeapSort(IStatisticCounter statisticCounter) : base(statisticCounter)
        {
        }

        public override IList<T> SortInPlace<T>(IList<T> collection)
        {
            int length = collection.Count;

            for (int i = length / 2 - 1; i >= 0; i--)
            {
                IncrementalHeapify(collection, length, i);
            }

            for (int i = length - 1; i > 0; i--)
            {
                Swap(collection, 0, i);
                IncrementalHeapify(collection, i, 0);
            }

            return collection;
        }

        public override IList<T> RecursiveSortInPlace<T>(IList<T> collection, CancellationToken? cancellationToken = null)
        {
            int length = collection.Count;

            for (int i = length / 2 - 1; i >= 0; i--)
            {
                Heapify(collection, length, i, cancellationToken ?? CancellationToken.None);
            }

            for (int i = length - 1; i > 0; i--)
            {
                Swap(collection, 0, i);
                Heapify(collection, i, 0, cancellationToken ?? CancellationToken.None);
            }

            return collection;
        }

        private void Heapify<T>(IList<T> collection, int length, int i, CancellationToken cancellationToken) where T : IComparable<T>
        {
            cancellationToken.ThrowIfCancellationRequested();
            RuntimeHelpers.EnsureSufficientExecutionStack();

            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            if (left < length && GreaterThan(collection, left, largest))
            {
                largest = left;
            }

            if (right < length && GreaterThan(collection, right, largest))
            {
                largest = right;
            }

            if (largest != i)
            {
                Swap(collection, i, largest);
                Heapify(collection, length, largest, cancellationToken);
            }
        }

        private void IncrementalHeapify<T>(IList<T> collection, int length, int i) where T : IComparable<T>
        {
            // Heapify with no recursion
            while (true)
            {
                int largest = i;
                int left = 2 * i + 1;
                int right = 2 * i + 2;

                if (left < length && GreaterThan(collection, left, largest))
                {
                    largest = left;
                }

                if (right < length && GreaterThan(collection, right, largest))
                {
                    largest = right;
                }

                if (largest != i)
                {
                    Swap(collection, i, largest);
                    i = largest;
                }
                else
                {
                    break;
                }
            }
        }


    }
}

using SortPack.Domain.Abstractions;
using SortPack.Domain.Interfaces;

namespace SortPack.Infrastructure.Primitive
{
    public class BinaryInsertionSort : SortAlgorithm
    {
        public BinaryInsertionSort() { }
        public BinaryInsertionSort(IStatisticCounter statisticCounter) : base(statisticCounter) { }

        public override IList<T> Sort<T>(IList<T> collection)
        {
            List<T> result = [.. collection];

            SortInPlace(result);
            return result;
        }

        public override IList<T> SortInPlace<T>(IList<T> collection)
        {
            for (int i = 1; i < collection.Count; i++)
            {
                T key = collection[i];
                StatisticCounter?.IncrementReadOperations();

                int insertionIndex = BinarySearchLessThan(collection, 0, i - 1, key);
                if (insertionIndex == i)
                {
                    continue;
                }

                collection.RemoveAt(i);
                collection.Insert(insertionIndex, key);
                StatisticCounter?.IncrementWriteOperations((ulong)(i - insertionIndex + 1));
            }
            return collection;
        }

        private int BinarySearchLessThan<T>(IList<T> collection, int left, int right, T key) where T : IComparable<T>
        {
            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                int comparisonResult = key.CompareTo(collection[mid]);
                StatisticCounter?.IncrementReadOperations();
                StatisticCounter?.IncrementCompareOperations();
                switch (comparisonResult)
                {
                    case 0:
                        return mid;
                    case < 0:
                        right = mid - 1;
                        break;
                    default:
                        left = mid + 1;
                        break;
                }
            }
            return left;
        }
    }
}

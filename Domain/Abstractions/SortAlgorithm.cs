using SortPack.Domain.Interfaces;

namespace SortPack.Domain.Abstractions
{
    public abstract class SortAlgorithm : ISortAlgorithm
    {
        protected IStatisticCounter? StatisticCounter;
        protected SortAlgorithm(IStatisticCounter statisticCounter)
        {
            StatisticCounter = statisticCounter ?? throw new ArgumentNullException(nameof(statisticCounter));
        }
        protected SortAlgorithm()
        {
            StatisticCounter = null;
        }


        public abstract IList<T> SortInPlace<T>(IList<T> collection) where T : IComparable<T>;

        public Task<IList<T>> SortInPlaceAsync<T>(IList<T> collection) where T : IComparable<T>
        {
            return Task.Run(() => SortInPlace(collection));
        }



        public IList<T> Sort<T>(IList<T> collection) where T : IComparable<T>
        {
            IList<T> result = [.. collection];
            return SortInPlace(result);
        }

        public Task<IList<T>> SortAsync<T>(IList<T> collection) where T : IComparable<T>
        {
            return Task.Run(() => Sort(collection));
        }

        protected void Swap<T>(IList<T> collection, int i, int j) where T : IComparable<T>
        {
            StatisticCounter?.IncrementWriteOperations(2);
            StatisticCounter?.IncrementReadOperations(2);
            (collection[i], collection[j]) = (collection[j], collection[i]);
        }

        protected void Swap<T>(IList<T> collection, int i, T a, int j, T b) where T : IComparable<T>
        {
            StatisticCounter?.IncrementWriteOperations(2);
            (collection[i], collection[j]) = (b, a);
        }

        protected bool LessThan<T>(IList<T> collection, int aIndex, int bIndex) where T : IComparable<T>
        {
            StatisticCounter?.IncrementCompareOperations();
            StatisticCounter?.IncrementReadOperations(2);
            return collection[aIndex].CompareTo(collection[bIndex]) < 0;
        }
        protected bool LessThan<T>(T a, T b) where T : IComparable<T>
        {
            StatisticCounter?.IncrementCompareOperations();
            return a.CompareTo(b) < 0;
        }

        protected bool LessThanOrEqual<T>(IList<T> collection, int aIndex, int bIndex) where T : IComparable<T>
        {
            StatisticCounter?.IncrementCompareOperations();
            StatisticCounter?.IncrementReadOperations(2);
            return collection[aIndex].CompareTo(collection[bIndex]) <= 0;
        }

        protected bool LessThanOrEqual<T>(T a, T b) where T : IComparable<T>
        {
            StatisticCounter?.IncrementCompareOperations();
            return a.CompareTo(b) <= 0;
        }

        protected bool GreaterThan<T>(IList<T> collection, int aIndex, int bIndex) where T : IComparable<T>
        {
            StatisticCounter?.IncrementCompareOperations();
            StatisticCounter?.IncrementReadOperations(2);
            return collection[aIndex].CompareTo(collection[bIndex]) > 0;
        }

        protected bool GreaterThan<T>(T a, T b) where T : IComparable<T>
        {
            StatisticCounter?.IncrementCompareOperations();
            return a.CompareTo(b) > 0;
        }

        protected bool GreaterThanOrEqual<T>(IList<T> collection, int aIndex, int bIndex) where T : IComparable<T>
        {
            StatisticCounter?.IncrementCompareOperations();
            StatisticCounter?.IncrementReadOperations(2);
            return collection[aIndex].CompareTo(collection[bIndex]) >= 0;
        }

        protected bool GreaterThanOrEqual<T>(T a, T b) where T : IComparable<T>
        {
            StatisticCounter?.IncrementCompareOperations();
            return a.CompareTo(b) >= 0;
        }

        protected bool Equal<T>(IList<T> collection, int aIndex, int bIndex) where T : IComparable<T>
        {
            StatisticCounter?.IncrementCompareOperations();
            StatisticCounter?.IncrementReadOperations(2);
            return collection[aIndex].CompareTo(collection[bIndex]) == 0;
        }

        protected bool Equal<T>(T a, T b) where T : IComparable<T>
        {
            StatisticCounter?.IncrementCompareOperations();
            return a.CompareTo(b) == 0;
        }
    }
}

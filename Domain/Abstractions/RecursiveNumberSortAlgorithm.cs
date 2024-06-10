using SortPack.Domain.Interfaces;
using System.Numerics;

namespace SortPack.Domain.Abstractions
{
    public abstract class RecursiveNumberSortAlgorithm : NumberSortAlgorithm, IRecursiveNumberSortAlgorithm
    {
        protected RecursiveNumberSortAlgorithm() { }
        protected RecursiveNumberSortAlgorithm(IStatisticCounter statisticCounter) : base(statisticCounter) { }

        public IList<T> RecursiveSort<T>(IList<T> collection, CancellationToken? cancellationToken = null) where T : IComparable<T>,
            IBitwiseOperators<T, T, T>,
            IShiftOperators<T, int, T>,
            IDivisionOperators<T, T, T>,
            IAdditionOperators<T, T, T>
        {
            List<T> result = [.. collection];
            return RecursiveSortInPlace(result, cancellationToken);
        }
        public Task<IList<T>> RecursiveSortAsync<T>(IList<T> collection, CancellationToken? cancellationToken = null) where T : IComparable<T>,
            IBitwiseOperators<T, T, T>,
            IShiftOperators<T, int, T>,
            IDivisionOperators<T, T, T>,
            IAdditionOperators<T, T, T>
        {
            return Task.Run(() => RecursiveSort(collection, cancellationToken));
        }

        public Task<IList<T>> RecursiveSortInPlaceAsync<T>(IList<T> collection, CancellationToken? cancellationToken = null) where T : IComparable<T>,
            IBitwiseOperators<T, T, T>,
            IShiftOperators<T, int, T>,
            IDivisionOperators<T, T, T>,
            IAdditionOperators<T, T, T>
        {
            return Task.Run(() => RecursiveSortInPlace(collection, cancellationToken));
        }

        public abstract IList<T> RecursiveSortInPlace<T>(IList<T> collection, CancellationToken? cancellationToken = null) where T : IComparable<T>,
            IBitwiseOperators<T, T, T>,
            IShiftOperators<T, int, T>,
            IDivisionOperators<T, T, T>,
            IAdditionOperators<T, T, T>;
    }
}

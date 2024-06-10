using System.Numerics;

namespace SortPack.Domain.Interfaces;

public interface IRecursiveNumberSortAlgorithm : INumberSortAlgorithm
{
    IList<T> RecursiveSort<T>(IList<T> collection, CancellationToken? cancellationToken) where T : IComparable<T>,
        IBitwiseOperators<T, T, T>,
        IShiftOperators<T, int, T>,
        IDivisionOperators<T, T, T>,
        IAdditionOperators<T, T, T>;
    Task<IList<T>> RecursiveSortAsync<T>(IList<T> collection, CancellationToken? cancellationToken = null) where T : IComparable<T>,
        IBitwiseOperators<T, T, T>,
        IShiftOperators<T, int, T>,
        IDivisionOperators<T, T, T>,
        IAdditionOperators<T, T, T>;
    IList<T> RecursiveSortInPlace<T>(IList<T> collection, CancellationToken? cancellationToken) where T : IComparable<T>,
        IBitwiseOperators<T, T, T>,
        IShiftOperators<T, int, T>,
        IDivisionOperators<T, T, T>,
        IAdditionOperators<T, T, T>;
    Task<IList<T>> RecursiveSortInPlaceAsync<T>(IList<T> collection, CancellationToken? cancellationToken = null) where T : IComparable<T>,
        IBitwiseOperators<T, T, T>,
        IShiftOperators<T, int, T>,
        IDivisionOperators<T, T, T>,
        IAdditionOperators<T, T, T>;
}

using System.Numerics;

namespace SortPack.Domain.Interfaces;

public interface INumberSortAlgorithm
{
    IList<T> Sort<T>(IList<T> collection) where T : IComparable<T>,
        IBitwiseOperators<T, T, T>,
        IShiftOperators<T, int, T>,
        IDivisionOperators<T, T, T>,
        IAdditionOperators<T, T, T>,
        ISubtractionOperators<T, T, T>;
    Task<IList<T>> SortAsync<T>(IList<T> collection) where T : IComparable<T>,
        IBitwiseOperators<T, T, T>,
        IShiftOperators<T, int, T>,
        IDivisionOperators<T, T, T>,
        IAdditionOperators<T, T, T>,
        ISubtractionOperators<T, T, T>;
    IList<T> SortInPlace<T>(IList<T> collection) where T : IComparable<T>,
        IBitwiseOperators<T, T, T>,
        IShiftOperators<T, int, T>,
        IDivisionOperators<T, T, T>,
        IAdditionOperators<T, T, T>,
        ISubtractionOperators<T, T, T>;
}

using SortPack.Domain.Interfaces;
using System.Numerics;

namespace SortPack.Domain.Abstractions
{
    public abstract class NumberSortAlgorithm : INumberSortAlgorithm
    {
        protected IStatisticCounter? StatisticCounter;
        protected NumberSortAlgorithm(IStatisticCounter statisticCounter)
        {
            StatisticCounter = statisticCounter ?? throw new ArgumentNullException(nameof(statisticCounter));
        }
        protected NumberSortAlgorithm()
        {
            StatisticCounter = null;
        }

        public IList<T> Sort<T>(IList<T> collection) where T : IComparable<T>,
            IBitwiseOperators<T, T, T>,
            IShiftOperators<T, int, T>,
            IDivisionOperators<T, T, T>,
            IAdditionOperators<T, T, T>
        {
            List<T> result = [.. collection];
            return SortInPlace(result);
        }

        public Task<IList<T>> SortAsync<T>(IList<T> collection) where T : IComparable<T>,
            IBitwiseOperators<T, T, T>,
            IShiftOperators<T, int, T>,
            IDivisionOperators<T, T, T>,
            IAdditionOperators<T, T, T>
        {
            return Task.Run(() => Sort(collection));
        }

        public abstract IList<T> SortInPlace<T>(IList<T> collection) where T : IComparable<T>,
            IBitwiseOperators<T, T, T>,
            IShiftOperators<T, int, T>,
            IDivisionOperators<T, T, T>,
            IAdditionOperators<T, T, T>;
        public Task<IList<T>> SortInPlaceAsync<T>(IList<T> collection) where T : IComparable<T>,
            IBitwiseOperators<T, T, T>,
            IShiftOperators<T, int, T>,
            IDivisionOperators<T, T, T>,
            IAdditionOperators<T, T, T>
        {
            return Task.Run(() => SortInPlace(collection));
        }

        protected static byte GetByteValue<T>(T value, int byteIndex) where T : IComparable<T>,
            IBitwiseOperators<T, T, T>,
            IShiftOperators<T, int, T>
        {
            return Convert.ToByte((value >> (byteIndex * 8)) & (T)Convert.ChangeType(0xFF, typeof(T)));
        }
    }
}

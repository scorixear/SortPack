using SortPack.Domain.Interfaces;

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

        public IList<ulong> Sort(IList<ulong> collection)
        {
            List<ulong> result = [.. collection];
            return SortInPlace(result);
        }

        public IList<uint> Sort(IList<uint> collection)
        {
            List<uint> result = [.. collection];
            return SortInPlace(result);
        }

        public IList<ushort> Sort(IList<ushort> collection)
        {
            List<ushort> result = [.. collection];
            return SortInPlace(result);
        }

        public IList<byte> Sort(IList<byte> collection)
        {
            List<byte> result = [.. collection];
            return SortInPlace(result);
        }

        public IList<long> Sort(IList<long> collection)
        {
            List<long> result = [.. collection];
            return SortInPlace(result);
        }

        public IList<int> Sort(IList<int> collection)
        {
            List<int> result = [.. collection];
            return SortInPlace(result);
        }

        public IList<short> Sort(IList<short> collection)
        {
            List<short> result = [.. collection];
            return SortInPlace(result);
        }

        public IList<sbyte> Sort(IList<sbyte> collection)
        {
            List<sbyte> result = [.. collection];
            return SortInPlace(result);
        }

        public Task<IList<ulong>> SortAsync(IList<ulong> collection)
        {
            return Task.Run(() => Sort(collection));
        }

        public Task<IList<uint>> SortAsync(IList<uint> collection)
        {
            return Task.Run(() => Sort(collection));
        }

        public Task<IList<ushort>> SortAsync(IList<ushort> collection)
        {
            return Task.Run(() => Sort(collection));
        }

        public Task<IList<byte>> SortAsync(IList<byte> collection)
        {
            return Task.Run(() => Sort(collection));
        }

        public Task<IList<long>> SortAsync(IList<long> collection)
        {
            return Task.Run(() => Sort(collection));
        }

        public Task<IList<int>> SortAsync(IList<int> collection)
        {
            return Task.Run(() => Sort(collection));
        }

        public Task<IList<short>> SortAsync(IList<short> collection)
        {
            return Task.Run(() => Sort(collection));
        }

        public Task<IList<sbyte>> SortAsync(IList<sbyte> collection)
        {
            return Task.Run(() => Sort(collection));
        }

        public IList<ulong> SortInPlace(IList<ulong> collection) => SortInPlace<ulong>(collection);

        public IList<uint> SortInPlace(IList<uint> collection) => SortInPlace<uint>(collection);

        public IList<ushort> SortInPlace(IList<ushort> collection) => SortInPlace<ushort>(collection);
        public IList<byte> SortInPlace(IList<byte> collection) => SortInPlace<byte>(collection);
        public IList<long> SortInPlace(IList<long> collection) => SortInPlace<long>(collection);
        public IList<int> SortInPlace(IList<int> collection) => SortInPlace<int>(collection);
        public IList<short> SortInPlace(IList<short> collection) => SortInPlace<short>(collection);
        public IList<sbyte> SortInPlace(IList<sbyte> collection) => SortInPlace<sbyte>(collection);

        protected abstract IList<T> SortInPlace<T>(IList<T> collection) where T : unmanaged;
    }
}

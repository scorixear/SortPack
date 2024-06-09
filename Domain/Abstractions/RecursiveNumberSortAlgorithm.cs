using SortPack.Domain.Interfaces;

namespace SortPack.Domain.Abstractions
{
    public abstract class RecursiveNumberSortAlgorithm : NumberSortAlgorithm, IRecursiveNumberSortAlgorithm
    {
        protected RecursiveNumberSortAlgorithm() { }
        protected RecursiveNumberSortAlgorithm(IStatisticCounter statisticCounter) : base(statisticCounter) { }

        public IList<ulong> RecursiveSort(IList<ulong> collection, CancellationToken? cancellationToken = null)
        {
            List<ulong> result = [.. collection];
            return RecursiveSortInPlace(result, cancellationToken);
        }
        public IList<uint> RecursiveSort(IList<uint> collection, CancellationToken? cancellationToken = null)
        {
            List<uint> result = [.. collection];
            return RecursiveSortInPlace(result, cancellationToken);
        }
        public IList<ushort> RecursiveSort(IList<ushort> collection, CancellationToken? cancellationToken = null)
        {
            List<ushort> result = [.. collection];
            return RecursiveSortInPlace(result, cancellationToken);
        }
        public IList<byte> RecursiveSort(IList<byte> collection, CancellationToken? cancellationToken = null)
        {
            List<byte> result = [.. collection];
            return RecursiveSortInPlace(result, cancellationToken);
        }
        public IList<long> RecursiveSort(IList<long> collection, CancellationToken? cancellationToken = null)
        {
            List<long> result = [.. collection];
            return RecursiveSortInPlace(result, cancellationToken);
        }
        public IList<int> RecursiveSort(IList<int> collection, CancellationToken? cancellationToken = null)
        {
            List<int> result = [.. collection];
            return RecursiveSortInPlace(result, cancellationToken);
        }
        public IList<short> RecursiveSort(IList<short> collection, CancellationToken? cancellationToken = null)
        {
            List<short> result = [.. collection];
            return RecursiveSortInPlace(result, cancellationToken);
        }
        public IList<sbyte> RecursiveSort(IList<sbyte> collection, CancellationToken? cancellationToken = null)
        {
            List<sbyte> result = [.. collection];
            return RecursiveSortInPlace(result, cancellationToken);
        }
        public Task<IList<ulong>> RecursiveSortAsync(IList<ulong> collection, CancellationToken? cancellationToken = null)
        {
            return Task.Run(() => RecursiveSort(collection, cancellationToken));
        }
        public Task<IList<uint>> RecursiveSortAsync(IList<uint> collection, CancellationToken? cancellationToken = null)
        {
            return Task.Run(() => RecursiveSort(collection, cancellationToken));
        }
        public Task<IList<ushort>> RecursiveSortAsync(IList<ushort> collection, CancellationToken? cancellationToken = null)
        {
            return Task.Run(() => RecursiveSort(collection, cancellationToken));
        }
        public Task<IList<byte>> RecursiveSortAsync(IList<byte> collection, CancellationToken? cancellationToken = null)
        {
            return Task.Run(() => RecursiveSort(collection, cancellationToken));
        }
        public Task<IList<long>> RecursiveSortAsync(IList<long> collection, CancellationToken? cancellationToken = null)
        {
            return Task.Run(() => RecursiveSort(collection, cancellationToken));
        }
        public Task<IList<int>> RecursiveSortAsync(IList<int> collection, CancellationToken? cancellationToken = null)
        {
            return Task.Run(() => RecursiveSort(collection, cancellationToken));
        }
        public Task<IList<short>> RecursiveSortAsync(IList<short> collection, CancellationToken? cancellationToken = null)
        {
            return Task.Run(() => RecursiveSort(collection, cancellationToken));
        }
        public Task<IList<sbyte>> RecursiveSortAsync(IList<sbyte> collection, CancellationToken? cancellationToken = null)
        {
            return Task.Run(() => RecursiveSort(collection, cancellationToken));
        }

        public IList<ulong> RecursiveSortInPlace(IList<ulong> collection, CancellationToken? cancellationToken = null)
        {
            return RecursiveSortInPlace<ulong>(collection, cancellationToken);
        }
        public IList<uint> RecursiveSortInPlace(IList<uint> collection, CancellationToken? cancellationToken = null)
        {
            return RecursiveSortInPlace<uint>(collection, cancellationToken);
        }
        public IList<ushort> RecursiveSortInPlace(IList<ushort> collection, CancellationToken? cancellationToken = null)
        {
            return RecursiveSortInPlace<ushort>(collection, cancellationToken);
        }
        public IList<byte> RecursiveSortInPlace(IList<byte> collection, CancellationToken? cancellationToken = null)
        {
            return RecursiveSortInPlace<byte>(collection, cancellationToken);
        }
        public IList<long> RecursiveSortInPlace(IList<long> collection, CancellationToken? cancellationToken = null)
        {
            return RecursiveSortInPlace<long>(collection, cancellationToken);
        }
        public IList<int> RecursiveSortInPlace(IList<int> collection, CancellationToken? cancellationToken = null)
        {
            return RecursiveSortInPlace<int>(collection, cancellationToken);
        }
        public IList<short> RecursiveSortInPlace(IList<short> collection, CancellationToken? cancellationToken = null)
        {
            return RecursiveSortInPlace<short>(collection, cancellationToken);
        }
        public IList<sbyte> RecursiveSortInPlace(IList<sbyte> collection, CancellationToken? cancellationToken = null)
        {
            return RecursiveSortInPlace<sbyte>(collection, cancellationToken);
        }
        public Task<IList<ulong>> RecursiveSortInPlaceAsync(IList<ulong> collection, CancellationToken? cancellationToken = null)
        {
            return Task.Run(() => RecursiveSortInPlace(collection, cancellationToken));
        }
        public Task<IList<uint>> RecursiveSortInPlaceAsync(IList<uint> collection, CancellationToken? cancellationToken = null)
        {
            return Task.Run(() => RecursiveSortInPlace(collection, cancellationToken));
        }
        public Task<IList<ushort>> RecursiveSortInPlaceAsync(IList<ushort> collection, CancellationToken? cancellationToken = null)
        {
            return Task.Run(() => RecursiveSortInPlace(collection, cancellationToken));
        }
        public Task<IList<byte>> RecursiveSortInPlaceAsync(IList<byte> collection, CancellationToken? cancellationToken = null)
        {
            return Task.Run(() => RecursiveSortInPlace(collection, cancellationToken));
        }
        public Task<IList<long>> RecursiveSortInPlaceAsync(IList<long> collection, CancellationToken? cancellationToken = null)
        {
            return Task.Run(() => RecursiveSortInPlace(collection, cancellationToken));
        }
        public Task<IList<int>> RecursiveSortInPlaceAsync(IList<int> collection, CancellationToken? cancellationToken = null)
        {
            return Task.Run(() => RecursiveSortInPlace(collection, cancellationToken));
        }
        public Task<IList<short>> RecursiveSortInPlaceAsync(IList<short> collection, CancellationToken? cancellationToken = null)
        {
            return Task.Run(() => RecursiveSortInPlace(collection, cancellationToken));
        }
        public Task<IList<sbyte>> RecursiveSortInPlaceAsync(IList<sbyte> collection, CancellationToken? cancellationToken = null)
        {
            return Task.Run(() => RecursiveSortInPlace(collection, cancellationToken));
        }

        protected abstract IList<T> RecursiveSortInPlace<T>(IList<T> collection, CancellationToken? cancellationToken = null) where T : IComparable<T>;
    }
}

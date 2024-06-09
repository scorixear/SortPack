namespace SortPack.Domain.Interfaces
{
    public interface IRecursiveNumberSortAlgorithm : INumberSortAlgorithm
    {
        IList<ulong> RecursiveSort(IList<ulong> collection, CancellationToken? cancellationToken = null);
        IList<uint> RecursiveSort(IList<uint> collection, CancellationToken? cancellationToken = null);
        IList<ushort> RecursiveSort(IList<ushort> collection, CancellationToken? cancellationToken = null);
        IList<byte> RecursiveSort(IList<byte> collection, CancellationToken? cancellationToken = null);
        IList<long> RecursiveSort(IList<long> collection, CancellationToken? cancellationToken = null);
        IList<int> RecursiveSort(IList<int> collection, CancellationToken? cancellationToken = null);
        IList<short> RecursiveSort(IList<short> collection, CancellationToken? cancellationToken = null);
        IList<sbyte> RecursiveSort(IList<sbyte> collection, CancellationToken? cancellationToken = null);
        Task<IList<ulong>> RecursiveSortAsync(IList<ulong> collection, CancellationToken? cancellationToken = null);
        Task<IList<uint>> RecursiveSortAsync(IList<uint> collection, CancellationToken? cancellationToken = null);
        Task<IList<ushort>> RecursiveSortAsync(IList<ushort> collection, CancellationToken? cancellationToken = null);
        Task<IList<byte>> RecursiveSortAsync(IList<byte> collection, CancellationToken? cancellationToken = null);
        Task<IList<long>> RecursiveSortAsync(IList<long> collection, CancellationToken? cancellationToken = null);
        Task<IList<int>> RecursiveSortAsync(IList<int> collection, CancellationToken? cancellationToken = null);
        Task<IList<short>> RecursiveSortAsync(IList<short> collection, CancellationToken? cancellationToken = null);
        Task<IList<sbyte>> RecursiveSortAsync(IList<sbyte> collection, CancellationToken? cancellationToken = null);
        IList<ulong> RecursiveSortInPlace(IList<ulong> collection, CancellationToken? cancellationToken = null);
        IList<uint> RecursiveSortInPlace(IList<uint> collection, CancellationToken? cancellationToken = null);
        IList<ushort> RecursiveSortInPlace(IList<ushort> collection, CancellationToken? cancellationToken = null);
        IList<byte> RecursiveSortInPlace(IList<byte> collection, CancellationToken? cancellationToken = null);
        IList<long> RecursiveSortInPlace(IList<long> collection, CancellationToken? cancellationToken = null);
        IList<int> RecursiveSortInPlace(IList<int> collection, CancellationToken? cancellationToken = null);
        IList<short> RecursiveSortInPlace(IList<short> collection, CancellationToken? cancellationToken = null);
        IList<sbyte> RecursiveSortInPlace(IList<sbyte> collection, CancellationToken? cancellationToken = null);
        Task<IList<ulong>> RecursiveSortInPlaceAsync(IList<ulong> collection, CancellationToken? cancellationToken = null);
        Task<IList<uint>> RecursiveSortInPlaceAsync(IList<uint> collection, CancellationToken? cancellationToken = null);
        Task<IList<ushort>> RecursiveSortInPlaceAsync(IList<ushort> collection, CancellationToken? cancellationToken = null);
        Task<IList<byte>> RecursiveSortInPlaceAsync(IList<byte> collection, CancellationToken? cancellationToken = null);
        Task<IList<long>> RecursiveSortInPlaceAsync(IList<long> collection, CancellationToken? cancellationToken = null);
        Task<IList<int>> RecursiveSortInPlaceAsync(IList<int> collection, CancellationToken? cancellationToken = null);
        Task<IList<short>> RecursiveSortInPlaceAsync(IList<short> collection, CancellationToken? cancellationToken = null);
        Task<IList<sbyte>> RecursiveSortInPlaceAsync(IList<sbyte> collection, CancellationToken? cancellationToken = null);
    }
}

namespace SortPack.Domain.Interfaces
{
    public interface INumberSortAlgorithm
    {
        IList<ulong> Sort(IList<ulong> collection);

        IList<uint> Sort(IList<uint> collection);

        IList<ushort> Sort(IList<ushort> collection);

        IList<byte> Sort(IList<byte> collection);

        IList<long> Sort(IList<long> collection);

        IList<int> Sort(IList<int> collection);

        IList<short> Sort(IList<short> collection);

        IList<sbyte> Sort(IList<sbyte> collection);

        Task<IList<ulong>> SortAsync(IList<ulong> collection);

        Task<IList<uint>> SortAsync(IList<uint> collection);

        Task<IList<ushort>> SortAsync(IList<ushort> collection);

        Task<IList<byte>> SortAsync(IList<byte> collection);

        Task<IList<long>> SortAsync(IList<long> collection);

        Task<IList<int>> SortAsync(IList<int> collection);

        Task<IList<short>> SortAsync(IList<short> collection);

        Task<IList<sbyte>> SortAsync(IList<sbyte> collection);

        IList<ulong> SortInPlace(IList<ulong> collection);

        IList<uint> SortInPlace(IList<uint> collection);

        IList<ushort> SortInPlace(IList<ushort> collection);
        IList<byte> SortInPlace(IList<byte> collection);
        IList<long> SortInPlace(IList<long> collection);
        IList<int> SortInPlace(IList<int> collection);
        IList<short> SortInPlace(IList<short> collection);
        IList<sbyte> SortInPlace(IList<sbyte> collection);
    }
}

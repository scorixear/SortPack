namespace SortPack.Domain.Interfaces;

public interface IStringSortAlgorithm
{
    IList<string> Sort(IList<string> collection, CancellationToken? cancellationToken = null);
    Task<IList<string>> SortAsync(IList<string> collection, CancellationToken? cancellationToken = null);
    IList<string> SortInPlace(IList<string> collection, CancellationToken? cancellationToken = null);
    Task<IList<string>> SortInPlaceAsync(IList<string> collection, CancellationToken? cancellationToken = null);
}

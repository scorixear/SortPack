namespace SortPack.Domain.Interfaces;

public interface IStringSortAlgorithm
{
    IList<string> Sort(IList<string> collection);
    Task<IList<string>> SortAsync(IList<string> collection);
    IList<string> SortInPlace(IList<string> collection);
    Task<IList<string>> SortInPlaceAsync(IList<string> collection);
}

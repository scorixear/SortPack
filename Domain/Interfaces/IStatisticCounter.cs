namespace SortPack.Domain.Interfaces;

public interface IStatisticCounter
{
    public ulong ReadOperations { get; }
    public ulong WriteOperations { get; }

    public ulong CompareOperations { get; }

    public void Reset();
    public void IncrementReadOperations();
    public void IncrementReadOperations(ulong count);
    public void IncrementWriteOperations();
    public void IncrementWriteOperations(ulong count);
    public void IncrementCompareOperations();
    public void IncrementCompareOperations(ulong count);
}

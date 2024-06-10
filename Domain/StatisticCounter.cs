using SortPack.Domain.Interfaces;

namespace SortPack.Domain;

public class StatisticCounter : IStatisticCounter
{
    public ulong ReadOperations { get; private set; }
    public ulong WriteOperations { get; private set; }
    public ulong CompareOperations { get; private set; }

    public StatisticCounter() { }

    public void Reset()
    {
        ReadOperations = 0;
        WriteOperations = 0;
    }

    public void IncrementReadOperations()
    {
        ReadOperations++;
    }

    public void IncrementReadOperations(ulong count)
    {
        ReadOperations += count;
    }

    public void IncrementWriteOperations()
    {
        WriteOperations++;
    }
    public void IncrementWriteOperations(ulong count)
    {
        WriteOperations += count;
    }

    public void IncrementCompareOperations()
    {
        CompareOperations++;
    }

    public void IncrementCompareOperations(ulong count)
    {
        CompareOperations += count;
    }

}

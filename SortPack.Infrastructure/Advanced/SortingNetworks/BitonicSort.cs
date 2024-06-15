using SortPack.Domain.Abstractions;
using SortPack.Domain.Interfaces;

namespace SortPack.Infrastructure.Advanced.SortingNetworks;

public class BitonicSort : SortAlgorithm
{
    public BitonicSort() { }

    public BitonicSort(IStatisticCounter statisticCounter) : base(statisticCounter) { }

    public override IList<T> SortInPlace<T>(IList<T> collection, CancellationToken? cancellationToken = null)
    {
        Stack<(int low, int high, bool direction, bool doMerge)> stack = new();
        stack.Push((0, collection.Count, true, false));
        while (stack.Count > 0)
        {

            (int low, int high, bool direction, bool doMerge) = stack.Pop();
            if (high <= 1)
            {
                continue;
            }

            cancellationToken?.ThrowIfCancellationRequested();
            if (doMerge)
            {
                int m = GreatestPowerOfTwoLessThan(high);
                for (int i = low; i < low + high - m; i++)
                {
                    if (direction == GreaterThan(collection, i, i + m))
                    {
                        Swap(collection, i, i + m);
                    }
                }
                stack.Push((low + m, high - m, direction, true));
                stack.Push((low, m, direction, true));
            }
            else
            {
                int m = high / 2;
                stack.Push((low, high, direction, true));
                stack.Push((low + m, high - m, direction, false));
                stack.Push((low, m, !direction, false));
            }
        }
        return collection;
    }

    private static int GreatestPowerOfTwoLessThan(int n)
    {
        int k = 1;
        while (k > 0 && k < n)
        {
            k <<= 1;
        }
        return k >> 1;
    }
}

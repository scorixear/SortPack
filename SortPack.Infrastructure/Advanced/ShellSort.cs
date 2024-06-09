using SortPack.Domain.Abstractions;
using SortPack.Domain.Interfaces;

namespace SortPack.Infrastructure.Advanced
{
    public class ShellSort : SortAlgorithm
    {
        private readonly double _shrinkFactor;
        public ShellSort(double shrinkFactor = 2.3)
        {
            _shrinkFactor = shrinkFactor;
        }

        public ShellSort(IStatisticCounter statisticCounter, double shrinkFactor = 2.3) : base(statisticCounter)
        {
            _shrinkFactor = shrinkFactor;
        }

        public override IList<T> Sort<T>(IList<T> collection)
        {
            List<T> result = [.. collection];
            return SortInPlace(result);
        }

        public override IList<T> SortInPlace<T>(IList<T> collection)
        {
            if (collection.Count < 2) return collection;
            int n = collection.Count;
            int gap = (int)(n / _shrinkFactor);
            if (gap <= 0)
            {
                gap = 1;
            }
            while (gap > 0)
            {
                for (int i = gap; i < n; i++)
                {
                    T temp = collection[i];
                    int j = i;
                    T other = collection[j - gap];
                    StatisticCounter?.IncrementReadOperations(2);

                    while (j >= gap && GreaterThan(other, temp))
                    {
                        collection[j] = other;
                        StatisticCounter?.IncrementWriteOperations();
                        j -= gap;
                        if (j < gap)
                        {
                            break;
                        }
                        other = collection[j - gap];
                        StatisticCounter?.IncrementReadOperations();
                    }

                    collection[j] = temp;
                }

                if (gap == 1) { break; }
                gap = (int)(gap / _shrinkFactor);

                if (gap <= 0)
                {
                    gap = 1;
                }
            }

            return collection;
        }
    }
}

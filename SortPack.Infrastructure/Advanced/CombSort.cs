﻿using SortPack.Domain;
using SortPack.Domain.Interfaces;

namespace SortPack.Infrastructure.Advanced
{
    public class CombSort : SortInPlaceAlgorithm
    {
        private readonly double _shrinkFactor;
        public CombSort(double shrinkFactor = 1.3)
        {
            _shrinkFactor = shrinkFactor;
        }

        public CombSort(IStatisticCounter statisticCounter, double shrinkFactor = 1.3) : base(statisticCounter)
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
            int length = collection.Count;
            int gap = length;
            bool swapped = true;

            while (gap != 1 || swapped)
            {
                gap = GetNextGap(gap);
                swapped = false;

                for (int i = 0; i < length - gap; i++)
                {
                    if (GreaterThan(collection, i, i + gap))
                    {
                        Swap(collection, i, i + gap);
                        swapped = true;
                    }
                }
            }

            return collection;
        }

        private int GetNextGap(int gap)
        {
            gap = (int)(gap / _shrinkFactor);

            if (gap < 1)
            {
                return 1;
            }

            return gap;
        }
    }
}

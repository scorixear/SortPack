# SortPack
[![.NET](https://github.com/scorixear/SortPack/actions/workflows/dotnet.yml/badge.svg)](https://github.com/scorixear/SortPack/actions/workflows/dotnet.yml)

SortPack is a NugetPackage Library for sorting algorithms.

Its goal is to provide an easy access to all commonly known sorting algorithms.<br>
As this will always be a work in progress, future versions will include more sorting algorithms.

## Installation
To install SortPack, run the following command in the Package Manager Console:

```bash
Install-Package SortPack -Version 1.0.0
```

## Usage
```csharp
using SortPack.Domain.Abstractions;
using SortPack.Domain.Extensions;
using SortPack.Domain.Interfaces;
using SortPack.Infrastructure.DivideAndConquer;
using SortPack.Infrastructure.Joke;

public class Program
{
  public static void Main()
  {
    int[] array = new int[] { 5, 3, 8, 4, 2, 1, 9, 7, 6 };

    IStatisticCounter statisticCounter = new StatisticCounter();
    ISortAlgorithm sortAlgorithm = new QuickSort(statisticCounter);
    int[] sortedArray = array.SortWith(sortAlgorithm);

    Console.WriteLine($"Used {statisticCounter.ReadOperations} reads");
    Console.WriteLine($"Used {statisticCounter.WriteOperations} writes");
    Console.WriteLine($"Used {statisticCounter.CompareOperations} compares");

    using(var cts = new CancellationTokenSource())
    {
      Task task = Task.Run(() => new BogoSort().SortInPlace(array, cts.Token));
      cts.CancelAfter(1000);
      task.Wait();
    }
  }
}
```

## Algorithms

### Primitive
| Implemented | Algorithm  | Best  | Average | Worst   | Memory avg. | Stable |
|-------------|------------|-------|---------|---------|-------------|--------|
| ✅          | BubbleSort | $$n$$ | $$n^2$$ | $$n^2$$ | $$1$$       | Yes    |
| ✅          | Binary InsertionSort | $$n \log n$$ | $$n^2$$ | $$n^2$$ | $$1$$ | Yes |
| ✅          | Double SelectionSort | $$n^2$$ | $$n^2$$ | $$n^2$$ | $$1$$ | No |
| ✅          | InsertionSort | $$n$$ | $$n^2$$ | $$n^2$$ | $$1$$ | Yes |
| ✅          | SelectionSort | $$n^2$$ | $$n^2$$ | $$n^2$$ | $$1$$ | No |
| ✅          | ShakerSort | $$n$$ | $$n^2$$ | $$n^2$$ | $$1$$ | Yes |

### Divide and Conquer
| Implemented | Algorithm  | Best  | Average | Worst   | Memory avg. | Stable |
|-------------|------------|-------|---------|---------|-------------|--------|
| ✅          | MergeSort  | $$n \log n$$ | $$n \log n$$ | $$n \log n$$ | $$n$$ | Yes |
| ✅          | QuickSort  | $$n \log n$$ | $$n \log n$$ | $$n^2$$ | $$\log n$$ | No |

### Advanced
| Implemented | Algorithm  | Best  | Average | Worst   | Memory avg. | Stable |
|-------------|------------|-------|---------|---------|-------------|--------|
| ✅          | CombSort   | $$n \log n$$ | $$n^{1.2}$$ | $$n^2$$ | $$1$$ | No |
| ✅          | HeapSort   | $$n \log n$$ | $$n \log n$$ | $$n \log n$$ | $$1$$ | No |
| ✅          | ShellSort  | $$n \log n$$ | $$n^{1.2}$$ | $$n^{1.5}$$ | $$1$$ | No |
| ✅          | Sorting Network | $$n$$ | $$n$$ | $$n$$ | $$n$$ | Yes |
| ✅          | BitonicSort | $$n \log^2 n$$ | $$n \log^2 n$$ | $$n \log^2 n$$ | $$n \log^2 n$$ | Yes |



### Non-comparison
| Implemented | Algorithm  | Best  | Average | Worst   | Memory avg. | Stable |
|-------------|------------|-------|---------|---------|-------------|--------|
| ✅          | BucketSort | |$$n+b+n^2/b$$ | | $$n+b$$ | Yes |
| ✅          | CountingSort | | $$n+r$$ |  | $$n+r$$ | Yes |
| ✅          | RadixLSDSort | | $$d(n+b)$$ | | $$n+b$$ | Yes |
| ✅          | RadixMSDSort | | $$nd+r$$ | | $$n+b+d$$ | Yes |

### Impractical
| Implemented | Algorithm  | Best  | Average | Worst   | Memory avg. | Stable |
|-------------|------------|-------|---------|---------|-------------|--------|
| ✅          | CycleSort  | $$n^2$$ | $$n^2$$ | $$n^2$$ | $$1$$ | No | 
| ✅          | GravitySort | | $$n \cdot r$$ | | $$n \cdot r$$ | No |
| ❌          | CircleSort  | $$n \log n$$ | $$n (\log n)^2$$ | $$n (\log n)^2$$ | $$\log n$$ | No |
| ✅          | ExchangeSort | $$n^2$$ | $$n^2$$ | $$n^2$$ | $$1$$ | No |
| ❌          | GnomeSort  | $$n$$ | $$n^2$$ | $$n^2$$ | $$1$$ | Yes |
| ❌          | LibrarySort | $$n \log n$$ | $$n \log n$$ | $$n^2$$ | $$n$$ | No |
| ❌          | Merge-InsertionSort | $$n$$ | $$n^2$$ | $$n^2$$ | $$\log n$$ | No |
| ❌          | Odd-EvenSort | $$n$$ | $$n^2$$ | $$n^2$$ | $$1$$ | Yes |
| ✅          | PatienceSort | $$n$$ | $$n \log n$$ | $$n \log n$$ | $$n$$ | No |
| ❌          | StrandSort | $$n$$ | $$n^1.5$$ | $$n^2$$ | $$n$$ | Yes |
| ❌          | TournamentSort | $$n \log n$$ | $$n \log n$$ | $$n \log n$$ | $$n$$ | No |
| ❌          | TreeSort   | $$n \log n$$ | $$n \log n$$ | $$n \log n$$ | $$n$$ | Yes |

### Joke
| Implemented | Algorithm  | Best  | Average | Worst   | Memory avg. | Stable |
|-------------|------------|-------|---------|---------|-------------|--------|
| ✅          | BogoSort   | $$n$$ | $$n \cdot n!$$ | $$\infty$$ | $$1$$ | No |
| ❌          | BogobogoSort | | $$n \cdot (n!)^n$$ | | $$n^2$$ | No |
| ❌          | MiracleSort | | $$\infty$$ | | $$1$$ | No |
| ❌          | PowerSort  | | $$n \cdot n!$$ | | $$n \cdot n!$$ | No |
| ❌          | Quantum Bogosort | $$n$$ | $$n$$ | $$n$$ | $$1$$ | No |
| ❌          | SleepSort  | | $$n + r$$ | | $$n$$ | No |
| ❌          | SlowSort   | $$n^{\Omega(\log n)}$$ | $$n^{\Omega(\log n)}$$ | $$n^{\Omega(\log n)}$$ | $$n$$ | No |
| ❌          | StalinSort | | $$n$$ | | 1 | ??? |
| ❌          | StoogeSort | $$n^{2.71}$$ | $$n^{2.71}$$ | $$n^{2.71}$$ | $$\log n$$ | Yes |

### Hybrid
| Implemented | Algorithm  | Best  | Average | Worst   | Memory avg. | Stable |
|-------------|------------|-------|---------|---------|-------------|--------|
| ❌          | American Flag Sort | | $$n \log r$$ | | $$\log r$$ | No |
| ❌          | Binary QuickSort | | $$n \log r$$ | | $$\log r$$ | No |
| ❌          | BurstSort  | | $$w \cdot n$$ | | $$w \cdot n$$ | No |
| ❌          | Cartesian TreeSort | | $$n \log n$$ || $$n$$ | No |
| ❌          | Dual-Pivot QuickSort | $$n \log n$$ | $$n \log n$$ | $$n^2$$ | $$\log n$$ | No |
| ❌          | In-Place Radix LSD Sort |  | $$d \cdot n^2$$ | | $$b$$ | Yes |
| ❌          | In-Place Radix MSD Sort |  | $$n \cdot d \cdot b$$ | | $$b + d$$ | No |
| ❌          | IntroSort  | $$n \log n$$ | $$n \log n$$ | $$n \log n$$ | $$\log n$$ | No |
| ❌          | Iterative MergeSort | $$n \log n$$ | $$n \log n$$ | $$n \log n$$ | $$n$$ | Yes |
| ❌          | Naive In-Place MergeSort | $$n^2$$ | $$n^2$$ | $$n^2$$ | $$1$$ | Yes |
| ❌          | Pattern-Defeating QuickSort | $$n$$ | $$n \log n$$ | $$n \log n$$ | $$\log n$$ | No |
| ❌          | PoplarSort | $$n \log n$$ | $$n \log n$$ | $$n \log n$$ | $$1$$ | No |
| ❌          | Proportion Extended QuickSort | $$n \log n$$ | $$n \log n$$ | $$n \log n$$ | $$\log n$$ | No |
| ❌          | ProxmapSort | | $$n + b + n^2/b$$ | | $$n + b$$ | Yes |
| ❌          | QuadSort   | $$n$$ | $$n \log n$$ | $$n \log n$$ | $$n$$ | Yes |
| ❌          | Quick LL Sort | $$n \log n$$ | $$n \log n$$ | $$n^2$$ | $$\log n$$ | No |
| ❌          | Rotate MergeSort | $$n (\log n)^2$$ | $$n (\log n)^2$$ | $$n (\log n)^2$$ | $$\log n$$ | Yes |
| ❌          | SampleSort | | $$s \cdot b \cdot \log(s\cdot b) + n + b + n^2/b$$ | | $$n + b$$ | Yes |
| ❌          | SmoothSort | $$n$$ | $$n \log n$$ | $$n \log n$$ | $$1$$ | No |
| ❌          | SpreadSort | | $$n \log n$$ | | $$n$$ | No |
| ❌          | TimSort    | $$n$$ | $$n \log n$$ | $$n \log n$$ | $$n$$ | Yes |
| ❌          | Weak HeapSort | $$n \log n$$ | $$n \log n$$ | $$n \log n$$ | $$n$$ | No |
| ❌          | WeaveSort  | $$n^2$$ | $$n^2$$ | $$n^2$$ | $$1$$ | Yes |

### BlockSort
| Implemented | Algorithm  | Best  | Average | Worst   | Memory avg. | Stable |
|-------------|------------|-------|---------|---------|-------------|--------|
| ❌          | BlockSort  | $$n$$ | $$n \log n$$ | $$n \log n$$ | $$1$$ | Yes |
| ❌          | GrailSort  | $$n \log n$$ | $$n \log n$$ | $$n \log n$$ | $$1$$ | Yes |
| ❌          | WikiSort   | $$n \log n$$ | $$n \log n$$ | $$n \log n$$ | $$1$$ | Yes |


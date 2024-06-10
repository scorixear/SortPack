using FluentAssertions;
using FluentAssertions.AssertMultiple;
using SortPack.Infrastructure.NonComparison;
using SortPack.Infrastructure.UnitTests.NUnit;

namespace SortPack.Infrastructure.UnitTests.Algorithms.NonComparison;

[TestFixture]
public class RadixMSDSortTest : RecursiveNumberSortAlgorithmTestBase<RadixMSDSort>
{
    [Test]
    public override void SortInPlace_Uneven_WhenCalled_SortsCollection()
    {
        // Arrange
        List<int> collection = [3, 2, 1];

        // Act
        Sut.SortInPlace(collection);

        // Assert
        AssertMultiple.Multiple(() =>
        {
            collection.Should().BeEquivalentTo(new List<int> { 1, 2, 3 });
            StatisticCounter.ReadOperations.Should().Be(12);
            StatisticCounter.WriteOperations.Should().Be(12);
            StatisticCounter.CompareOperations.Should().Be(0);
        });
    }

    [Test]
    public override void SortInPlace_Even_WhenCalled_SortsCollection()
    {
        // Arrange
        List<int> collection = [3, 2, 1, 0];

        // Act
        Sut.SortInPlace(collection);

        // Assert
        AssertMultiple.Multiple(() =>
        {
            collection.Should().BeEquivalentTo(new List<int> { 0, 1, 2, 3 });
            StatisticCounter.ReadOperations.Should().Be(16);
            StatisticCounter.WriteOperations.Should().Be(16);
            StatisticCounter.CompareOperations.Should().Be(0);
        });
    }

    [Test]
    public override void SortInPlace_EmptyList_DoNothing()
    {
        // Arrange
        List<int> collection = [];

        // Act
        Sut.SortInPlace(collection);

        // Assert
        AssertMultiple.Multiple(() =>
        {
            StatisticCounter.ReadOperations.Should().Be(0);
            StatisticCounter.WriteOperations.Should().Be(0);
            StatisticCounter.CompareOperations.Should().Be(0);
        });
    }

    [Test]
    public override void RecursiveSortInPlace_Uneven_WhenCalled_SortsCollection()
    {
        // Arrange
        List<int> collection = [3, 2, 1];

        // Act
        Sut.RecursiveSortInPlace(collection);

        // Assert
        AssertMultiple.Multiple(() =>
        {
            collection.Should().BeEquivalentTo(new List<int> { 1, 2, 3 });
            StatisticCounter.ReadOperations.Should().Be(12);
            StatisticCounter.WriteOperations.Should().Be(12);
            StatisticCounter.CompareOperations.Should().Be(0);
        });
    }

    [Test]
    public override void RecursiveSortInPlace_Even_WhenCalled_SortsCollection()
    {
        // Arrange
        List<int> collection = [3, 2, 1, 0];

        // Act
        Sut.RecursiveSortInPlace(collection);

        // Assert
        AssertMultiple.Multiple(() =>
        {
            collection.Should().BeEquivalentTo(new List<int> { 0, 1, 2, 3 });
            StatisticCounter.ReadOperations.Should().Be(16);
            StatisticCounter.WriteOperations.Should().Be(16);
            StatisticCounter.CompareOperations.Should().Be(0);
        });
    }

    [Test]
    public override void RecursiveSortInPlace_EmptyList_DoNothing()
    {
        // Arrange
        List<int> collection = [];

        // Act
        Sut.SortInPlace(collection);

        // Assert
        AssertMultiple.Multiple(() =>
        {
            StatisticCounter.ReadOperations.Should().Be(0);
            StatisticCounter.WriteOperations.Should().Be(0);
            StatisticCounter.CompareOperations.Should().Be(0);
        });
    }

    [Test]
    public void SortInPlace_String_Uneven_WhenCalled_SortsCollection()
    {
        // Arrange
        List<string> collection = ["C", "B", "A"];

        // Act
        Sut.SortInPlace(collection);

        // Assert
        AssertMultiple.Multiple(() =>
        {
            collection.Should().BeEquivalentTo(new List<string> { "A", "B", "C" });
            StatisticCounter.ReadOperations.Should().Be(6);
            StatisticCounter.WriteOperations.Should().Be(6);
            StatisticCounter.CompareOperations.Should().Be(0);
        });
    }

    [Test]
    public void SortInPlace_String_Even_WhenCalled_SortsCollection()
    {
        // Arrange
        List<string> collection = ["D", "C", "B", "A"];

        // Act
        Sut.SortInPlace(collection);

        // Assert
        AssertMultiple.Multiple(() =>
        {
            collection.Should().BeEquivalentTo(new List<string> { "A", "B", "C", "D" });
            StatisticCounter.ReadOperations.Should().Be(8);
            StatisticCounter.WriteOperations.Should().Be(8);
            StatisticCounter.CompareOperations.Should().Be(0);
        });
    }

    [Test]
    public void SortInPlace_String_EmptyList_DoNothing()
    {
        // Arrange
        List<string> collection = [];

        // Act
        Sut.SortInPlace(collection);

        // Assert
        AssertMultiple.Multiple(() =>
        {
            StatisticCounter.ReadOperations.Should().Be(0);
            StatisticCounter.WriteOperations.Should().Be(0);
            StatisticCounter.CompareOperations.Should().Be(0);
        });
    }

    [Test]
    public void RecursiveSortInPlace_String_Uneven_WhenCalled_SortsCollection()
    {
        // Arrange
        List<string> collection = ["C", "B", "A"];

        // Act
        Sut.RecursiveSortInPlace(collection);

        // Assert
        AssertMultiple.Multiple(() =>
        {
            collection.Should().BeEquivalentTo(new List<string> { "A", "B", "C" });
            StatisticCounter.ReadOperations.Should().Be(6);
            StatisticCounter.WriteOperations.Should().Be(6);
            StatisticCounter.CompareOperations.Should().Be(0);
        });
    }

    [Test]
    public void RecursiveSortInPlace_String_Even_WhenCalled_SortsCollection()
    {
        // Arrange
        List<string> collection = ["D", "C", "B", "A"];

        // Act
        Sut.RecursiveSortInPlace(collection);

        // Assert
        AssertMultiple.Multiple(() =>
        {
            collection.Should().BeEquivalentTo(new List<string> { "A", "B", "C", "D" });
            StatisticCounter.ReadOperations.Should().Be(8);
            StatisticCounter.WriteOperations.Should().Be(8);
            StatisticCounter.CompareOperations.Should().Be(0);
        });
    }

    [Test]
    public void RecursiveSortInPlace_String_EmptyList_DoNothing()
    {
        // Arrange
        List<string> collection = [];

        // Act
        Sut.SortInPlace(collection);

        // Assert
        AssertMultiple.Multiple(() =>
        {
            StatisticCounter.ReadOperations.Should().Be(0);
            StatisticCounter.WriteOperations.Should().Be(0);
            StatisticCounter.CompareOperations.Should().Be(0);
        });
    }

    [Test, CancelOnInconclusive]
    [NonParallelizable]
    [TestCase(1_000, TestName = "1-000")]
    [TestCase(10_000, TestName = "10-000")]
    [TestCase(100_000, TestName = "100-000")]
    [TestCase(1_000_000, TestName = "1000-000")]
    [TestCase(10_000_000, TestName = "10-000-000")]
    public async Task MassiveList_String_RecursiveTest(int numberOfValues)
    {
        try
        {
            List<string> expected = GenerateAlphabeticalStrings(numberOfValues);

            string[] shuffled = [.. expected];
            Random.Shared.Shuffle(shuffled);

            await TimeoutHandler.HandleActionWithCancellationToken(3000, (cancellationToken) =>
            {
                Sut.RecursiveSortInPlace(shuffled, cancellationToken);
            });

            foreach ((string actual, string expect) in shuffled.Zip(expected))
            {
                actual.Should().Be(expect);
            }
        }
        catch (ThreadAbortException)
        {
            Console.WriteLine($"Test with {numberOfValues} timed out");
        }
    }

    [Test, CancelOnInconclusive]
    [TestCase(1000, TestName = "1-000")]
    [TestCase(10_000, TestName = "10-000")]
    [TestCase(100_000, TestName = "100-000")]
    [TestCase(1_000_000, TestName = "1-000-000")]
    [TestCase(10_000_000, TestName = "10-000-000")]
    public async Task MassiveList_String_Test(int numberOfValues)
    {
        try
        {
            List<string> expected = GenerateAlphabeticalStrings(numberOfValues);
            string[] shuffled = [.. expected];
            Random.Shared.Shuffle(shuffled);

            await TimeoutHandler.HandleActionWithoutCancellationToken(3000, () =>
            {
                Sut.SortInPlace(shuffled);
            });

            foreach ((string actual, string expect) in shuffled.Zip(expected))
            {
                actual.Should().Be(expect);
            }
        }
        catch (ThreadAbortException)
        {
            Console.WriteLine($"Test with {numberOfValues} timed out");
        }
    }

    private List<string> GenerateAlphabeticalStrings(int numberOfValues)
    {
        List<string> resultList = [];
        int repetitions = numberOfValues / 26;
        for (int i = 0; i < 26; i++)
        {
            for (int j = 0; j < repetitions; j++)
            {
                resultList.Add(new string((char)('A' + i), j + 1));
            }
        }
        return resultList;
    }
}

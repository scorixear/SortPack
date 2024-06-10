﻿using FluentAssertions;
using FluentAssertions.AssertMultiple;
using SortPack.Infrastructure.Primitive;

namespace SortPack.Infrastructure.UnitTests.Algorithms.Primitive;

[TestFixture]
public class ShakerSortTest : SortAlgorithmTestBase<ShakerSort>
{
    [Test]
    public override void SortInPlace_Uneven_WhenCalled_SortsCollection()
    {
        // Arrange
        List<int> collection = [3, 2, 1];

        // Act
        Sut!.SortInPlace(collection);

        // Assert
        AssertMultiple.Multiple(() =>
        {
            collection.Should().BeEquivalentTo(new List<int> { 1, 2, 3 });
            StatisticCounter!.ReadOperations.Should().Be(7);
            StatisticCounter!.WriteOperations.Should().Be(6);
            StatisticCounter!.CompareOperations.Should().Be(4);
        });
    }

    [Test]
    public override void SortInPlace_Even_WhenCalled_SortsCollection()
    {
        // Arrange
        List<int> collection = [3, 2, 1, 0];

        // Act
        Sut!.SortInPlace(collection);

        // Assert
        AssertMultiple.Multiple(() =>
        {
            collection.Should().BeEquivalentTo(new List<int> { 0, 1, 2, 3 });
            StatisticCounter!.ReadOperations.Should().Be(12);
            StatisticCounter!.WriteOperations.Should().Be(12);
            StatisticCounter!.CompareOperations.Should().Be(8);
        });
    }

    [Test]
    public override void SortInPlace_EmptyList_DoNothing()
    {
        // Arrange
        List<int> collection = [];

        // Act
        Sut!.SortInPlace(collection);

        // Assert
        AssertMultiple.Multiple(() =>
        {
            StatisticCounter!.ReadOperations.Should().Be(0);
            StatisticCounter!.WriteOperations.Should().Be(0);
            StatisticCounter!.CompareOperations.Should().Be(0);
        });
    }
}

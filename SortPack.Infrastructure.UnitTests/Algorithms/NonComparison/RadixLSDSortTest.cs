﻿using FluentAssertions;
using FluentAssertions.AssertMultiple;
using SortPack.Infrastructure.NonComparison;

namespace SortPack.Infrastructure.UnitTests.Algorithms.NonComparison
{
    [TestFixture]
    public class RadixLSDSortTest : NumberSortAlgorithmTestBase<RadixLSDSort>
    {
        [Test]
        public override void SortInPlace_Uneven_WhenCalled_SortsCollection()
        {
            // Arrange
            var collection = new List<int> { 3, 2, 1 };

            // Act
            Sut.SortInPlace(collection);

            // Assert
            AssertMultiple.Multiple(() =>
            {
                collection.Should().BeEquivalentTo(new List<int> { 1, 2, 3 });
                StatisticCounter.ReadOperations.Should().Be(24);
                StatisticCounter.WriteOperations.Should().Be(12);
                StatisticCounter.CompareOperations.Should().Be(0);
            });
        }

        [Test]
        public override void SortInPlace_Even_WhenCalled_SortsCollection()
        {
            // Arrange
            var collection = new List<int> { 3, 2, 1, 0 };

            // Act
            Sut.SortInPlace(collection);

            // Assert
            AssertMultiple.Multiple(() =>
            {
                collection.Should().BeEquivalentTo(new List<int> { 0, 1, 2, 3 });
                StatisticCounter.ReadOperations.Should().Be(32);
                StatisticCounter.WriteOperations.Should().Be(16);
                StatisticCounter.CompareOperations.Should().Be(0);
            });
        }

        [Test]
        public override void SortInPlace_EmptyList_DoNothing()
        {
            // Arrange
            var collection = new List<int>();

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
    }
}

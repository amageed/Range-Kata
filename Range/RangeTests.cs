using System;
using System.Linq;
using NUnit.Framework;

namespace Range
{
    [TestFixture]
    public class RangeTests
    {
        [Test]
        public void CannotInstantiateInvalidRangeTest()
        {
            string input = "2,6";
            Assert.That(() => new Range(input), Throws.ArgumentException);
        }

        [Test]
        public void CanCheckInclusiveStartAndEndTest()
        {
            string input = "[2,6]";
            var range = new Range(input);

            Assert.That(range.IsInclusiveStart, Is.True);
            Assert.That(range.IsInclusiveEnd, Is.True);
        }

        [Test]
        public void CanCheckExclusiveStartAndEndTest()
        {
            var range = new Range("(2,6)");

            Assert.That(range.IsInclusiveStart, Is.False);
            Assert.That(range.IsInclusiveEnd, Is.False);
        }

        [Test]
        public void CanGetExpectedInclusiveStartAndEndValuesTest()
        {
            var range = new Range("[2,6]");

            Assert.That(range.Start, Is.EqualTo(2));
            Assert.That(range.End, Is.EqualTo(6));
        }

        [Test]
        public void CanGetExpectedExclusiveStartAndEndValuesTest()
        {
            var range = new Range("(2,6)");

            Assert.That(range.Start, Is.EqualTo(3));
            Assert.That(range.End, Is.EqualTo(5));
        }

        [Test]
        public void CanGetInclusiveTwoToSixValuesTest()
        {
            var range = new Range("[2,6]");

            int[] expectedResult = { 2, 3, 4, 5, 6 };
            bool result = expectedResult.SequenceEqual(range.Sequence);

            Assert.That(result, Is.True);
        }

        [Test]
        public void CanGetExclusiveTwoToSixValuesTest()
        {
            var range = new Range("(2,6)");

            int[] expectedResult = { 3, 4, 5 };
            bool result = expectedResult.SequenceEqual(range.Sequence);

            Assert.That(result, Is.True);
        }

        [Test]
        public void CanGetInclusiveTwoToExclusiveSixValuesTest()
        {
            var range = new Range("[2,6)");

            int[] expectedResult = { 2, 3, 4, 5 };
            bool result = expectedResult.SequenceEqual(range.Sequence);

            Assert.That(result, Is.True);
        }

        [Test]
        public void RangesAreEqualTest()
        {
            string input = "[2,6)";
            var firstRange = new Range(input);
            var secondRange = new Range(input);

            bool result = firstRange.Equals(secondRange);

            Assert.That(result, Is.True);
        }

        [Test]
        public void RangesAreNotEqualTest()
        {
            var firstRange = new Range("[2,6)");
            var secondRange = new Range("(2,6)");

            bool result = firstRange.Equals(secondRange);

            Assert.That(result, Is.False);
        }

        [Test]
        public void FirstRangeContainsSecondRangeTest()
        {
            var firstRange = new Range("[2,10)");
            var secondRange = new Range("[3,5]");

            bool result = firstRange.Contains(secondRange);

            Assert.That(result, Is.True);
        }

        [Test]
        public void SecondRangeDoesNotContainFirstRangeTest()
        {
            var firstRange = new Range("[3,5]");
            var secondRange = new Range("[2,10)");

            bool result = firstRange.Contains(secondRange);

            Assert.That(result, Is.False);
        }

        [Test]
        public void IncThreeIncFiveContainsIncThreeExcFiveIsTrueTest()
        {
            var firstRange = new Range("[3,5]");
            var secondRange = new Range("[3,5)");

            bool result = firstRange.Contains(secondRange);

            Assert.That(result, Is.True);
        }
    }
}

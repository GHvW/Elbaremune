using FluentAssertions;

using System;
using System.Collections.Generic;
using System.Linq;

using Xunit;

namespace Elbaremune.Test {

    public class IEnumerableExtensionsTests {

        public class GivenAnIEnumerableOfStrings {

            private readonly IEnumerable<string> strings;

            public GivenAnIEnumerableOfStrings() {
                this.strings = new List<string>() { "hello ", "world", "!!!" };
            }

            [Fact]
            public void WhenScanIsUsedWithAnAddOperation() {
                Func<string, string, string> add = (x, y) => x + y;

                var result = strings.Scan("", add);
                var expected = new List<string>() { "hello ", "hello world", "hello world!!!" };

                Assert.True(result.SequenceEqual(expected));
            }

            [Fact]
            public void WhenScanIsUsedWithAnOperationThatReturnsADifferentTypeFromTheIEnumerableType() {
                Func<int, string, int> addCounts = (totalChars, next) => totalChars + next.Length;

                var result = strings.Scan(0, addCounts);
                var expected = new List<int>() { 6, 11, 14 };

                Assert.True(result.SequenceEqual(expected));
            }
        }

        public class GivenAChunkSize {

            [Fact]
            public void WhenChunkedAndSizeCountIsEvenlyDivisibleBySize() {
                var data = new int[] { 1, 2, 3, 4, 5, 6 };
                var chunkSize = 3;

                var result = data.ChunkBySize(chunkSize).ToList();

                result.Count.Should().Be(2);

                result[0][0].Should().Be(1);
                result[0][1].Should().Be(2);
                result[0][2].Should().Be(3);
                result[1][0].Should().Be(4);
                result[1][1].Should().Be(5);
                result[1][2].Should().Be(6);
            }

            [Fact]
            public void WhenChunkedAndSizeCountIsNotEvenlyDivisibleBySize() {
                var data = new int[] { 1, 2, 3, 4, 5 };
                var chunkSize = 3;

                var result = data.ChunkBySize(chunkSize).ToList();

                result.Count.Should().Be(2);

                result[1].Length.Should().Be(2);

                result[0][0].Should().Be(1);

                result[1][0].Should().Be(4);
            }

            [Fact]
            public void WhenChunknedAndSequenceOnlyHasOneElement() {
                var data = new int[] { 1 };
                var chunkSize = 3;

                var result = data.ChunkBySize(chunkSize).ToList();

                result.Count.Should().Be(1);

                result[0].Length.Should().Be(1);
            }
        }
    }
}

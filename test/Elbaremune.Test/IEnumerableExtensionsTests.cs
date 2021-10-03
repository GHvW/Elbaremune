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
    }
}

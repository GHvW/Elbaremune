using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace Elbaremune.Test {

    public class SequenceTests {

        public class GivenAFactorialResult {

            private readonly int factorialResult;

            public GivenAFactorialResult() {
                this.factorialResult = 120; // 5!
            }

            [Fact]
            public void When() {
                Func<int, (int, int)?> generator = (it) => {
                    if (it == 0) {
                        return null;
                    }
                    return (it - 1, it - 1);
                };

                var result = Sequence.Unfold(10, generator);

                var expected = new List<int>() { 5, 4, 3, 2, 1 };

                Assert.True(result.SequenceEqual(expected));
            }

            [Fact]
            public void WhenUnfoldingit() {
                Func<(int, int), (int, (int, int))?> generator = (it) => {
                    var (n, currentState) = it;
                    if (n == 0) {
                        return null;
                    }
                    return (n, (n - 1, currentState / n));
                };

                var result = Sequence.Unfold((5, this.factorialResult), generator);

                var expected = new List<int>() { 5, 4, 3, 2, 1 };

                Assert.True(result.SequenceEqual(expected));
            }
        }
    }
}

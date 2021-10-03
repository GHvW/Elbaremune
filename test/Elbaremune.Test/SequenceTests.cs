using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace Elbaremune.Test {

    public class SequenceTests {

        public class GivenANumber {

            private readonly int number;

            public GivenANumber() {
                this.number = 5;
            }

            [Fact]
            public void WhenUnfoldedByOneToZero() {
                Func<int, (int, int)?> generator = (it) => {
                    if (it == 0) {
                        return null;
                    }
                    return (it, it - 1);
                };

                var result = Sequence.Unfold(this.number, generator);

                var expected = new List<int>() { 5, 4, 3, 2, 1 };

                Assert.True(result.SequenceEqual(expected));
            }

            [Fact]
            public void WhenNumbersAreGeneratedFromTheNumberToZero() {

                var result = Sequence.Generate(this.number, (x) => x < 0 ? null : x - 1);
            }
        }

        public class GivenAFactorialResult {

            private readonly int factorialResult;

            public GivenAFactorialResult() {
                this.factorialResult = 120; // 5!
            }


            [Fact]
            public void WhenUnfoldingTheFactorial() {
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

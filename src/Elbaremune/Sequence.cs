using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elbaremune {

    public static class Sequence {
        
        // TODO - can we consolidate this to 1 yet?
        // TODO - test runtime perf of using is vs explicit null check
        public static IEnumerable<TItem> Generate<TItem>(TItem seed, Func<TItem, TItem?> nextFn)
            where TItem : class {
            TItem currentItem = seed;
            while (nextFn(currentItem) is TItem nextState) {
                yield return nextState;
                currentItem = nextState;
            }
        }

        public static IEnumerable<TItem> Generate<TItem>(TItem seed, Func<TItem, TItem?> nextFn)
            where TItem : struct {
            TItem currentItem = seed;
            while (nextFn(currentItem) is TItem nextState) {
                yield return nextState;
                currentItem = nextState;
            }
        }

        
        // UnAggregate?
        public static IEnumerable<TItem> Unfold<TState, TItem>(this TState seed, Func<TState, (TItem, TState)?> gen) {
            var currentState = seed;
            while (gen(currentState) is (TItem, TState) nextResult) {
                var (item, nextState) = nextResult;
                yield return item;
                currentState = nextState;
            }
        }
    }
}

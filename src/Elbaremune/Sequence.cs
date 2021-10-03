using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elbaremune {

    public static class Sequence {
        
        public static IEnumerable<TItem> Generate<TItem>(TItem seed, Func<TItem, TItem?> nextFn) {
            var currentItem = seed;
            while (nextFn(currentItem) is TItem nextState) {
                yield return nextState;
                currentItem = nextState;
            }
        }

        
        // UnAggregate?
        public static IEnumerable<TItem> Unfold<TState, TItem>(this TState state, Func<TState, (TItem, TState)?> gen) {
            var currentState = state;
            while (gen(currentState) is (TItem, TState) nextResult) {
                var (item, nextState) = nextResult;
                yield return item;
                currentState = nextState;
            }
        }
    }
}

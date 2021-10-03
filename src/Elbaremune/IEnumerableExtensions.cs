using System;
using System.Collections.Generic;
using System.Linq;


namespace Elbaremune {

    public static class IEnumerableExtensions {

        public static IEnumerable<TState> Scan<TState, TItem>(
            this IEnumerable<TItem> @this, 
            TState initialState, 
            Func<TState, TItem, TState> reducer) {

            var currentState = initialState;
            foreach (var item in @this) {
                var newState = reducer(currentState, item);
                yield return newState;
                currentState = newState;
            }
        }
    }
}

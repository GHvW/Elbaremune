using System;
using System.Collections.Generic;
using System.Linq;


namespace Elbaremune {

    public static class IEnumerableExtensions {

        public static IEnumerable<TState> Scan<TState, TItem>(
            this IEnumerable<TItem> @this, 
            TState initialState, 
            Func<TState, TItem, TState> reducer
        ) {
            var currentState = initialState;
            foreach (var item in @this) {
                var newState = reducer(currentState, item);
                yield return newState;
                currentState = newState;
            }
        }

        public static IEnumerable<A[]> ChunkBySize<A>(
            this IEnumerable<A> @this, 
            int chunkSize
        ) {
            if (chunkSize <= 0) {
                throw new ArgumentOutOfRangeException("size must be a positive number");
            }

            var counter = 0;
            A[] chunk = new A[chunkSize];

            foreach (var item in @this) {
                if (counter < chunkSize) {
                    chunk[counter] = item;                   
                    counter = counter + 1;
                } else {
                    yield return chunk;
                    chunk = new A[chunkSize];
                    chunk[0] = item;
                    counter = 1;
                }
            }

            if (counter == chunkSize) {
                yield return chunk;
            } else if (counter != chunkSize && counter != 0) {
                yield return chunk[0..counter];
            }
        }
    }
}

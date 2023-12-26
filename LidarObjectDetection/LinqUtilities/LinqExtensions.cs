namespace LinqUtilities;



public static class LinqExtensions {

	public static IEnumerable<(T first, T second)> AdjacentPairs<T>(this IEnumerable<T> enumerable) {

		T[] array = enumerable as T[] ?? enumerable.ToArray();

		for (int index = 0; index < array.Length - 1;) {

			yield return (array[index], array[++index]);
		}
	}

	public static IEnumerable<(T first, T second)> AdjacentPairsWrapped<T>(this IEnumerable<T> enumerable) {

		T[] array = enumerable as T[] ?? enumerable.ToArray();

		for (int index = 0; index < array.Length - 1;) {

			yield return (array[index], array[++index % array.Length]);
		}
	}

}
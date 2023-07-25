using System.Diagnostics;

namespace LidarObjectDetection.Utilities;



public static class CollectionExtensions {

	public static List<T> Listify<T>(this T item) {

		return new() { item };
	}

	public static ReadOnlyArray<T> ToReadOnly<T>(this IEnumerable<T> enumerable) {

		return new(enumerable);
	}

	public static void AddIfNotNull<T>(this List<T> list, T? newValue) {

		if (newValue is null) {
			return;
		}

		list.Add(newValue);
	}

	public static void AddIfHasValue<T>(this List<T> list, Optional<T> newValue) {

		newValue.Match(list.Add, () => { });
	}

	////public static void AddValueIfIsSuccess<T>(this List<T> list, IResult<T> result) {

	////	if (result is IResult<T>.Success success) {
	////		list.Add(success.Value);
	////	}
	////}

	public static IEnumerable<T> AppendIfNotNull<T>(this IEnumerable<T> enumerable, T? newValue) {

		return newValue is null ? enumerable : enumerable.Append(newValue);
	}

	public static IEnumerable<TTarget?> SelectIfNotNull<TCollection, TTarget>
		(this IEnumerable<TCollection> enumerable, Func<TCollection, TTarget?> selector) {

		return enumerable.Select(selector).Where(x => x is not null);
	}

	public static IEnumerable<TTarget> SelectIfHasValue<TCollection, TTarget>
		(this IEnumerable<TCollection> enumerable, Func<TCollection, Optional<TTarget>> transformer) {

		return enumerable
			.Select(transformer)
			.Where(x => x.Match(_ => true, () => false))
			.Select(x => x.Match(value => value, () => throw new UnreachableException()));
	}

	public static void AddIfUnique<T>(this List<T> list, T item) {
		if (!list.Contains(item)) {
			list.Add(item);
		}
	}

	public static void AddUniqueItems<T>(this List<T> list, IEnumerable<T> newItems) {

		foreach (T item in newItems) {
			list.AddIfUnique(item);
		}
	}



	public static ReadOnlyArray<T> ReadOnlyListify<T>(this T item) {

		return new List<T> { item }.ToReadOnly();
	}



	public static void Foreach<T>(this IEnumerable<T> enumerable, Action<T> predicate) {

		foreach (T item in enumerable) {
			predicate(item);
		}
	}



	public static bool IsEmpty<T>(this IEnumerable<T> enumerable) {

		return !enumerable.Any();
	}

	public static bool None<T>(this IEnumerable<T> enumerable, T value) where T : IComparable {

		return enumerable.Count(x => x.CompareTo(value) == 0) == 0;
	}

	public static bool None<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate) {

		return !enumerable.Any(predicate);
	}

	public static bool OnlyOne<T>(this IEnumerable<T> enumerable) {

		return enumerable.Count() == 1;
	}

	public static bool OnlyOne<T>(this IEnumerable<T> enumerable, T value) where T : IComparable {

		return enumerable.Count(x => x.CompareTo(value) == 0) == 1;
	}

	public static bool OnlyOne<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate) {

		return enumerable.Count(predicate) == 1;
	}

	public static bool Multiple<T>(this IEnumerable<T> enumerable) {

		return enumerable.Count() > 1;
	}

	public static bool Multiple<T>(this IEnumerable<T> enumerable, T value) where T : IComparable {

		return enumerable.Count(x => x.CompareTo(value) == 0) > 1;
	}

	public static bool Multiple<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate) {

		return enumerable.Count(predicate) > 1;
	}



	public static IEnumerable<(T first, T second)> Pair<T>(this IEnumerable<T> enumerable1, IEnumerable<T> enumerable2) {

		T[] array1 = enumerable1 as T[] ?? enumerable1.ToArray();
		T[] array2 = enumerable2 as T[] ?? enumerable2.ToArray();

		if (array1.Length != array2.Length) {
			throw new InvalidOperationException();
		}

		return array1.Zip(array2);
	}



	public static string CharArrayToString(this IEnumerable<char> enumerable) {

		return enumerable.Aggregate("", (current, character) => current + character);
	}



	//public static ReadOnlyKeysDictionary<TKey, TValue> ToReadOnlyKeysDictionary<TKey, TValue>(
	//	this IEnumerable<TKey> keys, TValue defaultValue) where TKey : notnull {

	//	return new ReadOnlyKeysDictionary<TKey, TValue>(keys, defaultValue);
	//}


	public static bool ElementsAreUnique<T>(this IEnumerable<T> enumerable) {

		return !enumerable.ContainsDuplicates();
	}

	public static bool ContainsDuplicates<T>(this IEnumerable<T> enumerable) {

		T[] array = enumerable as T[] ?? enumerable.ToArray();

		// todo add an Any() overload that takes an index argument in the predicate (like the where show below)

		return array.Where((outerElement, i) => array.Where((innerElement, j) => i != j && outerElement!.Equals(innerElement)).Any()).Any();
	}

	// todo non-mutating equivalent to PruneDuplicates would be WithoutDuplicates or WithoutEntriesFrom(other enumerable)

	public static void PruneToUnion<T>(this List<T> list, IEnumerable<T> other) {

		T[] uniqueValuesInList = list.Where(item => !other.Contains(item)).ToArray();

		foreach (T item in uniqueValuesInList) {
			list.Remove(item);
		}
	}

	public static void PruneEntriesFrom<T>(this List<T> list, IEnumerable<T> other) {

		T[] duplicates = list.Where(other.Contains).ToArray();

		foreach (T duplicate in duplicates) {
			list.Remove(duplicate);
		}
	}

	public static void PruneEntriesFrom<T1, T2>(this List<T1> list, IEnumerable<T2> other, Func<T1, T2, bool> predicate) {

		T1[] duplicates = list.Where(item => other.Any(x => predicate(item, x))).ToArray();

		foreach (T1 item in duplicates) {
			list.Remove(item);
		}
	}

	public static void PruneEntriesFrom<T1, T2>(this List<T1> list, IEnumerable<T2> other, Func<T2, T1> selector) {

		IEnumerable<T1> transformedOther = other.Select(selector).ToArray();

		T1[] duplicates = list.Where(transformedOther.Contains).ToArray();

		foreach (T1 item in duplicates) {
			list.Remove(item);
		}
	}

	public static void PruneToUnionAndDispose<T>(this List<T> list, IEnumerable<T> other) where T : IDisposable {

		T[] uniqueValuesInList = list.Where(item => !other.Contains(item)).ToArray();

		foreach (T item in uniqueValuesInList) {
			list.RemoveAndDispose(item);
		}
	}

	public static bool RemoveAndDispose<T>(this List<T> list, T item) where T : IDisposable {

		bool result = list.Remove(item);

		if (result) {
			item.Dispose();
		}

		return result;
	}

}
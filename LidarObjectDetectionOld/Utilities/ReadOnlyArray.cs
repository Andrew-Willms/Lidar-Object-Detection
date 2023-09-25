using System.Collections;
using Utilities;

namespace LidarObjectDetection.Utilities;



public class ReadOnlyArray {

	public static readonly ReadOnlyArray Empty = new();

	private ReadOnlyArray() { }

}



public class ReadOnlyArray<T> : IEnumerable<T> {

	private readonly T[] Collection;

	public int Count => Collection.Length;

	public T this[int index] => Collection[index];



	public static readonly ReadOnlyArray<T> Empty = new();
	public static implicit operator ReadOnlyArray<T>(ReadOnlyArray empty) {

		if (empty != ReadOnlyArray.Empty) {
			throw new ArgumentException($"This casting operator should only be used with {nameof(ReadOnlyArray.Empty)}", nameof(empty));
		}

		return Empty;
	}



	private ReadOnlyArray() {
		Collection = Array.Empty<T>();
	}

	public ReadOnlyArray(IEnumerable<T> collection) {
		Collection = collection.ToArray();
	}




	public ReadOnlyArray<T> CopyAndAddRange(IEnumerable<T> newItems) {

		IEnumerable<T> enumerable = newItems.ToList();

		T[] array = new T[Count + enumerable.Count()];

		CopyTo(array, 0);

		int currentIndex = Count;
		foreach (T item in enumerable) {
			array[currentIndex] = item;
			currentIndex++;
		}

		return array.ToList().ToReadOnly();
	}

	public ReadOnlyArray<T> CopyAndAddRange(params T[] newItems) {

		return CopyAndAddRange(newItems.AsEnumerable());
	}

	public ReadOnlyArray<T> CopyAndAddRanges(params IEnumerable<T>[] newItems) {

		return CopyAndAddRange(newItems.SelectMany(x => x)); // I think this should work
	}



	public IEnumerator<T> GetEnumerator() {
		return ((IEnumerable<T>)Collection).GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator() {
		return GetEnumerator();
	}

	public void CopyTo(T[] array, int arrayIndex) {
		Collection.CopyTo(array, arrayIndex);
	}

}
using LidarObjectDetection.Utilities;

namespace LinearAlgebra;



public class Polygon {

	public required ReadOnlyArray<Point2> Points { get; init; }

	private Polygon() { }

	public static Polygon? Create(IEnumerable<Point2> points) {

		Point2[] array = points as Point2[] ?? points.ToArray();

		// if any adjacent points are identical
		if (array[0] == array[1] || 
		    array[0] == array.Last() ||
		    array[1..^1].Where((element, index) => element == array[index - 1] || element == array[index + 1]).Any()) {

			return null;
		}

		// decided to allow duplicates so long as they are not adjacent
		//if (array.ContainsDuplicates()) {
		//	return null;
		//}

		return new() { Points = array.ToReadOnly() };
	}

}
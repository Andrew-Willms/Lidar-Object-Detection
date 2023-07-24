using LidarObjectDetection.Utilities;

namespace LidarObjectDetection.LinearAlgebra; 



public class Polygon {
	
	public required ReadOnlyArray<Point> Points { get; init; }

	private Polygon() { }

	public static Polygon? Create(IEnumerable<Point> points) {

		Point[] array = points as Point[] ?? points.ToArray();

		if (array.ContainsDuplicates()) {
			return null;
		}

		return new() { Points = array.ToReadOnly() };
	}

}
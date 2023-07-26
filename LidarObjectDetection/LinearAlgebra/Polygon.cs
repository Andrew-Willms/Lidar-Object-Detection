using System.Diagnostics;
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

	public PolygonIntersection Intersection(LineSegment lineSegment) {

		List<LineSegmentIntersection> intersections = new();

		for (int i = 0; i < Points.Count; i++) {

			LineSegment polygonLineSegment = LineSegment.Create(Points[i], Points[i % Points.Count]) ?? throw new UnreachableException();

			Optional<LineSegmentIntersection> intersection = polygonLineSegment.Intersection(lineSegment);

			intersection.Match(intersections.Add, () => { });
		}

		return intersections.ToArray();
	}

}
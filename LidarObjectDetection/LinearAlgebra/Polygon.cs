using System.Collections.Generic;
using System.Diagnostics;

namespace LinearAlgebra;



public class Polygon {

	//public required ReadOnlyArray<Point2> Points { get; init; }

	//public ReadOnlyArray<LineSegment> Edges { get; }

	//private Polygon(ReadOnlyArray<LineSegment> edges) {
	//	Edges = edges;
	//}

	//public static Polygon? Create(IEnumerable<Point2> points) {

	//	Point2[] array = points as Point2[] ?? points.ToArray();

	//	// if any adjacent points are identical
	//	if (array.Where((element, i) => element == array[(i + 1) % array.Length]).Any()) {
	//		return null;
	//	}

	//	ReadOnlyArray<LineSegment> edges = array
	//		.Select((t, i) => LineSegment.Create(t, array[(i + 1) % array.Length]) ?? throw new UnreachableException())
	//		.ToReadOnly();

	//	return new(edges) { Points = array.ToReadOnly() };
	//}

	//public PolygonIntersection Intersection(LineSegment lineSegment) {

	//	return Edges.Select(x => x.Intersection(lineSegment)).WhereHasValue().ToArray();
	//}

	//public Point2[] NearestIntersection(LineSegment lineSegment, Point2 point) {
	//	LineSegmentIntersection[] intersections = Intersection(lineSegment).AsT0;
	//	foreach (LineSegmentIntersection intersection in intersections) {
	//		intersection.Match(
	//			point => point,
	//			lineSegment => {
	//				return new Vector2()
	//			}
	//		);
	//	}
	//}

}
using OneOf;

namespace LidarObjectDetection.LinearAlgebra;



public class NoIntersection {

	public static readonly NoIntersection Instance = new();

	private NoIntersection() { }

}

/// <summary>
/// Intersection of 2D line segments.
/// </summary>
[GenerateOneOf]
public partial class LineSegmentIntersection : OneOfBase<NoIntersection, Point, LineSegment> { }

//[GenerateOneOf]
//public partial class LineIntersection : OneOfBase<NoIntersection, Point, Line> { }



//public interface IZeroD {

//}

//public interface IOneD {

//	public bool Parallel(IOneD other);

//	public bool CoLinear(IOneD other);

//	public bool Intersects(IOneD other);

//	public bool Contains(IZeroD other);

//}


//public static class Intersections {

//	public static bool Intersects(this LineSegment a, LineSegment b) {
//		throw new NotImplementedException();
//	}

//	public static bool Intersects(this Line a, LineSegment b) {
//		throw new NotImplementedException();
//	}

//	public static bool Intersects(this LineSegment a, Line b) {
//		throw new NotImplementedException();
//	}

//	public static bool Intersects(this Line a, Line b) {
//		throw new NotImplementedException();
//	}



//	public static LineSegmentIntersection IntersectionWith(this LineSegment a, LineSegment b) {
//		throw new NotImplementedException();
//	}

//	public static LineSegmentIntersection IntersectionWith(this Line a, LineSegment b) {
//		throw new NotImplementedException();
//	}

//	public static LineSegmentIntersection IntersectionWith(this LineSegment a, Line b) {
//		throw new NotImplementedException();
//	}

//	public static LineIntersection IntersectionWith(this Line a, Line b) {
//		throw new NotImplementedException();
//	}



//	public static bool Contains(this Line line, Point point) {
//		throw new NotImplementedException();
//	}

//	public static bool Contains(this LineSegment line, Point point) {
//		throw new NotImplementedException();
//	}

//	public static bool IsOn(this Point point, Line line) {
//		throw new NotImplementedException();
//	}

//	public static bool IsOn(this Point point, LineSegment lineSegment) {
//		throw new NotImplementedException();
//	}



//	public static bool ParallelTo(this Line line) {
//		throw new NotImplementedException();
//	}

//}
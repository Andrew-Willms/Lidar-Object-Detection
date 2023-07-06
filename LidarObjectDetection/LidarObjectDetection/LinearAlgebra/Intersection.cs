using OneOf;

namespace LidarObjectDetection.LinearAlgebra;



public class NoIntersection {

	public static NoIntersection Instance = new();

	private NoIntersection() { }

}

/// <summary>
/// Intersection of 2D lines.
/// </summary>
[GenerateOneOf]
public partial class LineIntersection : OneOfBase<NoIntersection, Point, LineSegment> { }
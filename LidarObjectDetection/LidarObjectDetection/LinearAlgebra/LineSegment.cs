namespace LidarObjectDetection.LinearAlgebra;



/// <summary>
/// Line Segment in 2D
/// </summary>
public class LineSegment {

	public required double XStart { get; init; }

	public required double YStart { get; init; }

	public required double XEnd { get; init; }

	public required double YEnd { get; init; }

	public bool Intersects(LineSegment other) {

		return IntersectionPoint(other).Match(
			_ => false,
			_ => true,
			_ => true);
	}

	public LineIntersection IntersectionPoint(LineSegment other) {

		double distanceFromStart = (YEnd - YStart) * (other.XStart - XStart) - (XEnd - XStart) * (other.YStart - YStart);
		double distanceFromEnd = (YEnd - YStart) * (other.XEnd - XStart) - (XEnd - XStart) * (other.YEnd - YStart);

		// if the lines overlap
		if (distanceFromStart == 0 && distanceFromEnd == 0) {

		}

		// If both the the start and end points of other are on the positive side of the current line.
		if (distanceFromStart > 0 || distanceFromEnd > 0) {
			return NoIntersection.Instance;
		}

		// If both the the start and end points of other are on the negative side of the current line.
		if (distanceFromStart < 0 || distanceFromEnd < 0) {
			return NoIntersection.Instance;
		}

		double startPointWeight = distanceFromEnd / (distanceFromStart + distanceFromEnd); // todo: issues with negative signs here
		double endPointWeight = distanceFromStart / (distanceFromStart + distanceFromEnd); // todo: issues with negative signs here

		double intersectionX = startPointWeight * other.XStart + endPointWeight * other.XEnd;
		double intersectionY = startPointWeight * other.YStart + endPointWeight * other.YEnd;

		return new Point(intersectionX, intersectionY);
	}

}
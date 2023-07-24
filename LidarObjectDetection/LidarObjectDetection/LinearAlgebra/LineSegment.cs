namespace LidarObjectDetection.LinearAlgebra;



/// <summary>
/// Line Segment in 2D
/// </summary>
public class LineSegment {

	public required Point Start { get; init; }

	public required Point End { get; init; }

	public Vector UnitDirectionVector => new Vector(Start, End).GetUnitVector();



	private LineSegment() { }

	// todo determine how I want to do errors
	public static LineSegment? Create(Point start, Point end) {

		return start == end
			? null
			: new LineSegment { Start = start, End = end };
	}

	public static LineSegment? Create(Point start, Vector displacement) {

		return displacement == Vector.Zero
			? null
			: new LineSegment { Start = start, End = new() { X = start.X + displacement.X, Y = start.Y + displacement.Y } };
	}



	public bool Intersects(LineSegment other) {

		return Intersection(other).Match(
			_ => false,
			_ => true,
			_ => true);
	}

	// todo unit test this
	public LineSegmentIntersection Intersection(LineSegment other) {

		double distanceFromStart = SignedDistanceFromLine(other.Start);
		double distanceFromEnd = SignedDistanceFromLine(other.End);

		// if the lines are collinear
		if (distanceFromStart == 0 && distanceFromEnd == 0) {

			double minParameterization = Math.Min(ParameterAtPoint(Start), ParameterAtPoint(End));
			double maxParameterization = Math.Max(ParameterAtPoint(Start), ParameterAtPoint(End));

			double minOtherParameterization = Math.Min(ParameterAtPoint(other.Start), ParameterAtPoint(other.End));
			double maxOtherParameterization = Math.Max(ParameterAtPoint(other.Start), ParameterAtPoint(other.End));

			double parameterAtStartOfOverlap = Math.Max(minParameterization, minOtherParameterization);
			double parameterAtEndOfOverlap = Math.Min(maxParameterization, maxOtherParameterization);

			// if none of them overlap
			if (parameterAtStartOfOverlap > parameterAtEndOfOverlap) {
				return NoIntersection.Instance;
			}

			// if only one point overlaps
			if (parameterAtStartOfOverlap == parameterAtEndOfOverlap) {
				return PointAtParameterization(parameterAtStartOfOverlap);
			}

			return Create(PointAtParameterization(parameterAtStartOfOverlap), PointAtParameterization(parameterAtEndOfOverlap));
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

		double intersectionX = startPointWeight * other.Start.X + endPointWeight * other.End.X;
		double intersectionY = startPointWeight * other.Start.Y + endPointWeight * other.End.Y;

		return new Point { X = intersectionX, Y = intersectionY };
	}

	private double SignedDistanceFromLine(Point point) {

		return (End.Y - Start.Y) * (point.X - Start.X) - (End.X - Start.X) * (point.Y - Start.Y);
	}

	private double ParameterAtPoint(Point point) {

		if (SignedDistanceFromLine(point) != 0) {
			throw new ArgumentException();
		}

		Vector startToPoint = new(Start, point);
		bool positiveParameter = UnitDirectionVector.SameDirectionAs(startToPoint);
		double magnitude = startToPoint.Magnitude;

		return positiveParameter switch {
			true => magnitude,
			false => -magnitude
		};
	}

	private Point PointAtParameterization(double parameter) {

		return Start.Translate(UnitDirectionVector * parameter);
	}

	public bool Collinear(LineSegment other) {

		double distanceFromStart = SignedDistanceFromLine(other.Start);
		double distanceFromEnd = SignedDistanceFromLine(other.End);

		return distanceFromStart == 0 && distanceFromEnd == 0;
	}

	// todo adding comparison operators would be nice at some point

}
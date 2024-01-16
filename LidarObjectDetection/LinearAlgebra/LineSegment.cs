using System;
using System.Diagnostics.CodeAnalysis;
using OneOf;

namespace LinearAlgebra;



/// <summary>
/// Line Segment in 2D
/// </summary>
public readonly struct LineSegment {

	public Point2 Start { get; }

	public Point2 End { get; }

	public Vector2 UnitDirectionVector => new Vector2(Start, End).GetUnitVector();



	public LineSegment() {
		throw new NotSupportedException("don't use the empty constructor");
	}

	public LineSegment(Point2 start, Point2 end) {

		Start = start;
		End = end;

		if (Start == End) {
			throw new InvalidOperationException("You cannot create a line segment of zero length");
		}
	}

	[SetsRequiredMembers]
	public LineSegment(Point2 start, Vector2 displacement) {

		Start = start;
		End = new() { X = start.X + displacement.X, Y = start.Y + displacement.Y };

		if (Start == End) {
			throw new InvalidOperationException("You cannot create a line segment of zero length");
		}
	}

	


	public bool Intersects(LineSegment other) {

		return Intersection(other) is not null;
	}

	// todo unit test this
	public LineSegmentIntersection? Intersection(LineSegment other) {

		double distanceToStartOfOther = SignedDistanceFromLine(other.Start);
		double distanceToEndOfOther = SignedDistanceFromLine(other.End);

		double minParameterization = Math.Min(ParameterAtPoint(Start), ParameterAtPoint(End));
		double maxParameterization = Math.Max(ParameterAtPoint(Start), ParameterAtPoint(End));

		// if the lines are collinear
		if (distanceToStartOfOther == 0 && distanceToEndOfOther == 0) {

			double minParameterizationOfOther = Math.Min(ParameterAtPoint(other.Start), ParameterAtPoint(other.End));
			double maxParameterizationOfOther = Math.Max(ParameterAtPoint(other.Start), ParameterAtPoint(other.End));

			double parameterAtStartOfOverlap = Math.Max(minParameterization, minParameterizationOfOther);
			double parameterAtEndOfOverlap = Math.Min(maxParameterization, maxParameterizationOfOther);

			// if none of them overlap
			if (parameterAtStartOfOverlap > parameterAtEndOfOverlap) {
				return null;
			}

			// if only one point overlaps
			if (Math.Abs(parameterAtStartOfOverlap - parameterAtEndOfOverlap) < Constants.ComparisonTolerance) {
				return (LineSegmentIntersection)PointAtParameterization(parameterAtStartOfOverlap);
			}

			return (LineSegmentIntersection) new LineSegment(
				start: PointAtParameterization(parameterAtStartOfOverlap), 
				end: PointAtParameterization(parameterAtEndOfOverlap));
		}

		// If both the the start and end points of other are on the positive side of the current line.
		if (distanceToStartOfOther > 0 && distanceToEndOfOther > 0) {
			return null;
		}

		// If both the the start and end points of other are on the negative side of the current line.
		if (distanceToStartOfOther < 0 && distanceToEndOfOther < 0) {
			return null;
		}

		double startPointWeight = Math.Abs(distanceToEndOfOther) / (Math.Abs(distanceToStartOfOther) + Math.Abs(distanceToEndOfOther));
		double endPointWeight = Math.Abs(distanceToStartOfOther) / (Math.Abs(distanceToStartOfOther) + Math.Abs(distanceToEndOfOther));

		double intersectionX = startPointWeight * other.Start.X + endPointWeight * other.End.X;
		double intersectionY = startPointWeight * other.Start.Y + endPointWeight * other.End.Y;

		Point2 potentialIntersection = new() { X = intersectionX, Y = intersectionY };
		double parameterizationAtIntersection = ParameterAtPoint(potentialIntersection);

		if (minParameterization <= parameterizationAtIntersection && parameterizationAtIntersection <= maxParameterization) {
			return (LineSegmentIntersection)potentialIntersection;
		}

		return null;
	}

	private double SignedDistanceFromLine(Point2 point) {

		return (End.Y - Start.Y) * (point.X - Start.X) - (End.X - Start.X) * (point.Y - Start.Y);
	}

	private double ParameterAtPoint(Point2 point) {

		//if (SignedDistanceFromLine(point) != 0) {
		//	throw new ArgumentException();
		//}

		if (Start == point) {
			return 0;
		}

		Vector2 startToPoint = new(Start, point);
		bool positiveParameter = UnitDirectionVector.SameDirectionAs(startToPoint);
		double magnitude = startToPoint.Magnitude;

		return positiveParameter switch {
			true => magnitude,
			false => -magnitude
		};
	}

	private Point2 PointAtParameterization(double parameter) {

		return Start.Translated(UnitDirectionVector * parameter);
	}

	public bool Collinear(LineSegment other) {

		double distanceFromStart = SignedDistanceFromLine(other.Start);
		double distanceFromEnd = SignedDistanceFromLine(other.End);

		return distanceFromStart == 0 && distanceFromEnd == 0;
	}

	// todo adding comparison operators would be nice at some point



	public LineSegment RotateAround(double rotation, Point2 centerPoint) {
		
		return new(
			start: Start.Rotated(rotation, centerPoint),
			end: End.Rotated(rotation, centerPoint));
	}

	public LineSegment Translate(Vector2 vector) {

		return new(
			start: new() { X = Start.X + vector.X, Y = Start.Y + vector.Y },
			end: new() { X = End.X + vector.X, Y = End.Y + vector.Y });
	}

	// todo add comparison functions and operators

}


/// <summary>
/// Intersection of 2D line segments.
/// </summary>
[GenerateOneOf]
public partial class LineSegmentIntersection : OneOfBase<Point2, LineSegment>;

//[GenerateOneOf]
//public partial class LineIntersection : OneOfBase<NoIntersection, Point, Line> { }
[GenerateOneOf]
public partial class PolygonIntersection : OneOfBase<LineSegmentIntersection[]>;
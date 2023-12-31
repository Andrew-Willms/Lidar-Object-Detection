﻿using LidarObjectDetection.Utilities;

namespace LinearAlgebra;



/// <summary>
/// Line Segment in 2D
/// </summary>
public class LineSegment {

	public required Point2 Start { get; init; }

	public required Point2 End { get; init; }

	public Vector2 UnitDirectionVector2 => new Vector2(Start, End).GetUnitVector();



	private LineSegment() { }

	// todo determine how I want to do errors
	public static LineSegment? Create(Point2 start, Point2 end) {

		return start == end
			? null
			: new LineSegment { Start = start, End = end };
	}

	public static LineSegment? Create(Point2 start, Vector2 displacement) {

		return displacement == Vector2.Zero
			? null
			: new LineSegment { Start = start, End = new() { X = start.X + displacement.X, Y = start.Y + displacement.Y } };
	}



	public bool Intersects(LineSegment other) {

		return Intersection(other).Match(
			_ => true,
			() => false);
	}

	// todo unit test this
	public Optional<LineSegmentIntersection> Intersection(LineSegment other) {

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
				return Optional.NoValue;
			}

			// if only one point overlaps
			if (Math.Abs(parameterAtStartOfOverlap - parameterAtEndOfOverlap) < Point2.ComparisonTolerance) {
				return (LineSegmentIntersection)PointAtParameterization(parameterAtStartOfOverlap);
			}

			return (LineSegmentIntersection)Create(PointAtParameterization(parameterAtStartOfOverlap), PointAtParameterization(parameterAtEndOfOverlap));
		}

		// If both the the start and end points of other are on the positive side of the current line.
		if (distanceToStartOfOther > 0 && distanceToEndOfOther > 0) {
			return Optional.NoValue;
		}

		// If both the the start and end points of other are on the negative side of the current line.
		if (distanceToStartOfOther < 0 && distanceToEndOfOther < 0) {
			return Optional.NoValue;
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

		return Optional.NoValue;
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
		bool positiveParameter = UnitDirectionVector2.SameDirectionAs(startToPoint);
		double magnitude = startToPoint.Magnitude;

		return positiveParameter switch {
			true => magnitude,
			false => -magnitude
		};
	}

	private Point2 PointAtParameterization(double parameter) {

		return Start.Translate(UnitDirectionVector2 * parameter);
	}

	public bool Collinear(LineSegment other) {

		double distanceFromStart = SignedDistanceFromLine(other.Start);
		double distanceFromEnd = SignedDistanceFromLine(other.End);

		return distanceFromStart == 0 && distanceFromEnd == 0;
	}

	// todo adding comparison operators would be nice at some point

}
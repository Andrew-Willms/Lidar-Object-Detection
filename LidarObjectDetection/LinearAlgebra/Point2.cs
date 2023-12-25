using System;
using System.Diagnostics.CodeAnalysis;

namespace LinearAlgebra;



public readonly struct Point2 {

	public required double X { get; init; }

	public required double Y { get; init; }


	public const double ComparisonTolerance = 10e-6;


	[SetsRequiredMembers]
	public Point2(double x, double y) {
		X = x;
		Y = y;
	}



	public static bool operator ==(Point2 left, Point2 right) {

		return Math.Abs(left.X - right.X) < ComparisonTolerance && Math.Abs(left.Y - right.Y) < ComparisonTolerance;
	}

	public static bool operator !=(Point2 left, Point2 right) {

		return !(left == right);
	}

	public Point2 Translate(Vector2 translation) {
		return new() { X = X + translation.X, Y = Y + translation.Y };
	}

	public Point2 Rotate(double angle) {

		// todo would be nice to properly implement matrix math
		// this is just applying a rotation matrix

		double cosTheta = Math.Cos(angle * Math.PI / 180);
		double sinTheta = Math.Sin(angle * Math.PI / 180);

		return new() {
			X = cosTheta * X - sinTheta * Y,
			Y = sinTheta * X + cosTheta * Y
		};
	}

	public Point2 Rotate(double angle, Point2 centerPoint) {

		Vector2 centerOffset = new(centerPoint, new(0, 0));

		Point2 pointCentered = Translate(centerOffset);

		Point2 pointRotated = pointCentered.Rotate(angle);

		return pointRotated.Translate(-centerOffset);
	}

	public double DistanceFrom(Point2 otherPoint) {

		double deltaX = X - otherPoint.X;
		double deltaY = Y - otherPoint.Y;

		return double.Sqrt(deltaX * deltaX + deltaY * deltaY);
	}



	public override string ToString() {
		return $"{{X: {X}, Y: {Y}}}";
	}

}
using System;
using System.Diagnostics.CodeAnalysis;

namespace LinearAlgebra;



public readonly struct Point2 : IEquatable<Point2> {

	public required double X { get; init; }

	public required double Y { get; init; }



	[SetsRequiredMembers]
	public Point2(double x, double y) {
		X = x;
		Y = y;
	}

	public static readonly Point2 Origin = new() { X = 0, Y = 0 };



	public Point2 Translated(Vector2 translation) {
		return new() { X = X + translation.X, Y = Y + translation.Y };
	}

	public Point2 Rotated(double angle) {

		// todo would be nice to properly implement matrix math
		// this is just applying a rotation matrix

		double cosTheta = Math.Cos(angle * Math.PI / 180);
		double sinTheta = Math.Sin(angle * Math.PI / 180);

		return new() {
			X = cosTheta * X - sinTheta * Y,
			Y = sinTheta * X + cosTheta * Y
		};
	}

	public Point2 Rotated(double angle, Point2 centerPoint) {

		Vector2 centerOffset = new(centerPoint, new(0, 0));

		Point2 pointCentered = Translated(centerOffset);

		Point2 pointRotated = pointCentered.Rotated(angle);

		return pointRotated.Translated(-centerOffset);
	}

	public double DistanceFrom(Point2 otherPoint) {

		double deltaX = X - otherPoint.X;
		double deltaY = Y - otherPoint.Y;

		return double.Sqrt(deltaX * deltaX + deltaY * deltaY);
	}



	public static bool operator ==(Point2 left, Point2 right) {
		return left.Equals(right);
	}

	public static bool operator !=(Point2 left, Point2 right) {
		return !(left == right);
	}

	public bool Equals(Point2 other) {

		return Math.Abs(X - other.X) < Constants.ComparisonTolerance &&
		       Math.Abs(Y - other.Y) < Constants.ComparisonTolerance;
	}

	public override bool Equals(object? obj) {
		return obj is Point2 other && Equals(other);
	}

	public override int GetHashCode() {
		return HashCode.Combine(X, Y);
	}



	public override string ToString() {
		return $"{{X: {X}, Y: {Y}}}";
	}

}
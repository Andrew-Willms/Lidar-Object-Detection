using System;
using System.Diagnostics.CodeAnalysis;

namespace LinearAlgebra;



public readonly struct Point3 : IEquatable<Point3> {

	public required double X { get; init; }

	public required double Y { get; init; }

	public required double Z { get; init; }



	[SetsRequiredMembers]
	public Point3(double x, double y, double z) {
		X = x;
		Y = y;
		Z = z;
	}

	public static readonly Point3 Origin = new() { X = 0, Y = 0, Z = 0 };



	public Point3 Translated(Vector3 translation) {
		return new() { X = X + translation.X, Y = Y + translation.Y, Z = Z + translation.Z };
	}

	public double DistanceFrom(Point3 otherPoint) {

		double deltaX = X - otherPoint.X;
		double deltaY = Y - otherPoint.Y;
		double deltaZ = Z - otherPoint.Z;

		return double.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ);
	}



	public static bool operator ==(Point3 left, Point3 right) {
		return left.Equals(right);
	}

	public static bool operator !=(Point3 left, Point3 right) {

		return !(left == right);
	}

	public bool Equals(Point3 other) {

		return Math.Abs(X - other.X) < Constants.ComparisonTolerance &&
		       Math.Abs(Y - other.Y) < Constants.ComparisonTolerance &&
		       Math.Abs(Z - other.Z) < Constants.ComparisonTolerance;
	}

	public override bool Equals(object? obj) {
		return obj is Point3 other && Equals(other);
	}

	public override int GetHashCode() {
		return HashCode.Combine(X, Y, Z);
	}



	public override string ToString() {
		return $"{{X: {X}, Y: {Y}, Z: {Z}}}";
	}

}
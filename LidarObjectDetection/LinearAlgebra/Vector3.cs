using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace LinearAlgebra;



public readonly struct Vector3 : IEquatable<Vector3> {

	public required double X { get; init; }

	public required double Y { get; init; }

	public required double Z { get; init; }

	public double Magnitude => double.Sqrt(X * X + Y * Y + Z * Z);



	public Vector3() { }

	[SetsRequiredMembers]
	public Vector3(double x, double y, double z) {
		X = x;
		Y = y;
		Z = z;
	}

	[SetsRequiredMembers]
	public Vector3(Point3 point) {
		X = point.X;
		Y = point.Y;
		Z = point.Z;
	}

	[SetsRequiredMembers]
	public Vector3(Point3 start, Point3 end) {
		X = end.X - start.X;
		Y = end.Y - start.Y;
		Z = end.Z - start.Z;
	}

	public static readonly Vector3 Zero = new() { X = 0, Y = 0, Z = 0 };



	public bool SameDirectionAs(Vector3 other) {

		if (this == Zero || other == Zero) {
			throw new ArgumentException();
		}

		// neither of these can actually be null
		Vector3 thisUnit = GetUnitVector();
		Vector3 otherUnit = other.GetUnitVector();

		return thisUnit == otherUnit;
	}

	public bool SameDirectionAs(params Vector3[] others) {

		if (others.Any(x => x == Zero)) {
			throw new ArgumentException();
		}

		return others.All(SameDirectionAs);
	}

	public bool OppositeDirectionAs(Vector3 other) {

		if (this == Zero || other == Zero) {
			throw new ArgumentException();
		}

		// neither of these can actually be null
		Vector3 thisUnit = GetUnitVector();
		Vector3 otherUnit = other.GetUnitVector();

		return thisUnit == -1 * otherUnit;
	}

	public bool OppositeDirectionAs(params Vector3[] others) {

		if (others.Any(x => x == Zero)) {
			throw new ArgumentException();
		}

		return others.All(OppositeDirectionAs);
	}

	public Vector3 GetUnitVector() {

		if (this == Zero) {
			throw new ArgumentException();
		}

		return new() { X = X / Magnitude, Y = Y / Magnitude, Z = Z / Magnitude };
	}

	public static Vector3 Max(Vector3 left, Vector3 right) {

		return left.Magnitude > right.Magnitude
			? left
			: right;
	}

	public static Vector3 Min(Vector3 left, Vector3 right) {

		return left.Magnitude > right.Magnitude
			? right
			: left;
	}



	public static bool operator ==(Vector3 left, Vector3 right) {

		return left.Equals(right);
	}

	public static bool operator !=(Vector3 left, Vector3 right) {
		return !(left == right);
	}

	public static Vector3 operator *(Vector3 left, double right) {
		return new() { X = left.X * right, Y = left.Y * right, Z = left.Z * right };
	}

	public static Vector3 operator *(double left, Vector3 right) {
		return right * left;
	}

	public static Vector3 operator /(Vector3 left, double right) {
		return new() { X = left.X / right, Y = left.Y / right, Z = left.Z / right };
	}

	public static Vector3 operator /(double left, Vector3 right) {
		return right / left;
	}

	public static Vector3 operator +(Vector3 left, Vector3 right) {
		return new() { X = left.X + right.X, Y = left.Y + right.Y, Z = left.Z + right.Z };
	}

	public static Vector3 operator -(Vector3 left, Vector3 right) {
		return left + -1 * right;
	}

	public static Vector3 operator -(Vector3 vector) {
		return new(-vector.X, -vector.Y, -vector.Z);
	}

	public bool Equals(Vector3 other) {

		return Math.Abs(X - other.X) < Constants.ComparisonTolerance &&
		       Math.Abs(Y - other.Y) < Constants.ComparisonTolerance &&
		       Math.Abs(X - other.Z) < Constants.ComparisonTolerance;
	}

	public override bool Equals(object? obj) {
		return obj is Vector3 other && Equals(other);
	}

	public override int GetHashCode() {
		return HashCode.Combine(X, Y, Z);
	}



	public override string ToString() {
		return $"{{X: {X}, Y: {Y}, Z: {Z}}}";
	}

}
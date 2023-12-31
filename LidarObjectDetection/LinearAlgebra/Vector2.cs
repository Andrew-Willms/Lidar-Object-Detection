﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace LinearAlgebra;



public readonly struct Vector2 : IEquatable<Vector2> {

	public required double X { get; init; }

	public required double Y { get; init; }

	public double Magnitude => double.Sqrt(X * X + Y * Y);



	public Vector2() { }

	[SetsRequiredMembers]
	public Vector2(double x, double y) {
		X = x;
		Y = y;
	}

	[SetsRequiredMembers]
	public Vector2(Point2 point) {
		X = point.X;
		Y = point.Y;
	}

	[SetsRequiredMembers]
	public Vector2(Point2 start, Point2 end) {
		X = end.X - start.X;
		Y = end.Y - start.Y;
	}

	public static readonly Vector2 Zero = new() { X = 0, Y = 0 };



	public Vector2 GetUnitVector() {

		if (this == Zero) {
			throw new ArgumentException();
		}

		return new() { X = X / Magnitude, Y = Y / Magnitude };
	}

	public double DotProduct(Vector2 otherVector) {

		return X * otherVector.X + Y * otherVector.Y;
	}

	public double AngleTo(Vector2 otherVector) {

		double angle = Math.Atan2(Y, X);
		double otherAngle = Math.Atan2(otherVector.Y, otherVector.X);

		return otherAngle - angle;
	}

	public bool SameDirectionAs(Vector2 other) {

		if (this == Zero || other == Zero) {
			throw new ArgumentException();
		}

		// neither of these can actually be null
		Vector2 thisUnit = GetUnitVector();
		Vector2 otherUnit = other.GetUnitVector();

		return thisUnit == otherUnit;
	}

	public bool SameDirectionAs(params Vector2[] others) {

		if (others.Any(x => x == Zero)) {
			throw new ArgumentException();
		}

		return others.All(SameDirectionAs);
	}

	public bool OppositeDirectionAs(Vector2 other) {

		if (this == Zero || other == Zero) {
			throw new ArgumentException();
		}

		// neither of these can actually be null
		Vector2 thisUnit = GetUnitVector();
		Vector2 otherUnit = other.GetUnitVector();

		return thisUnit == -1 * otherUnit;
	}

	public bool OppositeDirectionAs(params Vector2[] others) {

		if (others.Any(x => x == Zero)) {
			throw new ArgumentException();
		}

		return others.All(OppositeDirectionAs);
	}

	public Vector2 GetClockwisePerpendicularVector() {
		return new() { X = Y, Y = -X };
	}

	public Vector2 GetCounterClockwisePerpendicularVector() {
		return new() { X = -Y, Y = X };
	}

	public static Vector2 Max(Vector2 left, Vector2 right) {

		return left.Magnitude > right.Magnitude
			? left
			: right;
	}

	public static Vector2 Min(Vector2 left, Vector2 right) {

		return left.Magnitude > right.Magnitude
			? right
			: left;
	}




	public static bool operator ==(Vector2 left, Vector2 right) {
		return left.Equals(right);
	}

	public static bool operator !=(Vector2 left, Vector2 right) {
		return !(left == right);
	}

	public static Vector2 operator *(Vector2 left, double right) {
		return new() { X = left.X * right, Y = left.Y * right };
	}

	public static Vector2 operator *(double left, Vector2 right) {
		return right * left;
	}

	public static Vector2 operator /(Vector2 left, double right) {
		return new() { X = left.X / right, Y = left.Y / right };
	}

	public static Vector2 operator /(double left, Vector2 right) {
		return right / left;
	}

	public static Vector2 operator +(Vector2 left, Vector2 right) {
		return new() { X = left.X + right.X, Y = left.Y + right.Y };
	}

	public static Vector2 operator -(Vector2 left, Vector2 right) {
		return left + -1 * right;
	}

	public static Vector2 operator -(Vector2 vector) {
		return new(-vector.X, -vector.Y);
	}

	public bool Equals(Vector2 other) {

		return Math.Abs(X - other.X) < Constants.ComparisonTolerance &&
		       Math.Abs(Y - other.Y) < Constants.ComparisonTolerance;
	}

	public override bool Equals(object? obj) {
		return obj is Vector2 other && Equals(other);
	}

	public override int GetHashCode() {
		return HashCode.Combine(X, Y);
	}



	public override string ToString() {
		return $"{{X: {X}, Y: {Y}}}";
	}

}
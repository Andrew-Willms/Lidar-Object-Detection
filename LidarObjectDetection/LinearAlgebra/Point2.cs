using System.Diagnostics.CodeAnalysis;
using System.Numerics;

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



	public override string ToString() {
		return $"{{X: {X}, Y: {Y}}}";
	}

}
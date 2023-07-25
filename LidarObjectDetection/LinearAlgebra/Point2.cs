using System.Diagnostics.CodeAnalysis;

namespace LinearAlgebra;



public readonly struct Point2 {

	public required double X { get; init; }

	public required double Y { get; init; }



	[SetsRequiredMembers]
	public Point2(double x, double y) {
		X = x;
		Y = y;
	}



	public static bool operator ==(Point2 left, Point2 right) {

		return left.X == right.X && left.Y == right.Y;
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
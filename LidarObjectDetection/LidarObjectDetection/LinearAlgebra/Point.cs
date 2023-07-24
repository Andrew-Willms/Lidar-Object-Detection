namespace LidarObjectDetection.LinearAlgebra; 



public readonly struct Point {

	public required double X { get; init; }

	public required double Y { get; init; }

	public static bool operator ==(Point left, Point right) {

		return left.X == right.X && left.Y == right.Y;
	}

	public static bool operator !=(Point left, Point right) {

		return !(left == right);
	}

	public Point Translate(Vector translation) {
		return new() { X = X + translation.X, Y = Y + translation.Y };
	}

}
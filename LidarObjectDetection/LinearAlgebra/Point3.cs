using System.Diagnostics.CodeAnalysis;

namespace LinearAlgebra;



public readonly struct Point3 {

	public required double X { get; init; }

	public required double Y { get; init; }

	public required double Z { get; init; }



	[SetsRequiredMembers]
	public Point3(double x, double y, double z) {
		X = x;
		Y = y;
		Z = z;
	}



	public static bool operator ==(Point3 left, Point3 right) {

		return left.X == right.X && left.Y == right.Y && left.Z == right.Z;
	}

	public static bool operator !=(Point3 left, Point3 right) {

		return !(left == right);
	}

	public Point3 Translate(Vector3 translation) {
		return new() { X = X + translation.X, Y = Y + translation.Y, Z = Z + translation.Z };
	}

	public double DistanceFrom(Point3 otherPoint) {

		double deltaX = X - otherPoint.X;
		double deltaY = Y - otherPoint.Y;
		double deltaZ = Z - otherPoint.Z;

		return double.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ);
	}



	public override string ToString() {
		return $"{{X: {X}, Y: {Y}, Z: {Z}}}";
	}

}
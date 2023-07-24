using System.Diagnostics.CodeAnalysis;

namespace LidarObjectDetection.LinearAlgebra;



public readonly struct Vector {

	public required double X { get; init; }

	public required double Y { get; init; }

	public double Magnitude => double.Sqrt(X * X + Y * Y);





	public Vector() { }

	[SetsRequiredMembers]
	public Vector(Point point) {
		X = point.X;
		Y = point.Y;
	}

	[SetsRequiredMembers]
	public Vector(Point start, Point end) {
		X = end.X - start.X;
		Y = end.Y - start.Y;
	}

	public static readonly Vector Zero = new() { X = 0, Y = 0 };



	public bool SameDirectionAs(Vector other) {

		if (this == Zero || other == Zero) {
			throw new ArgumentException();
		}

		// neither of these can actually be null
		Vector thisUnit = GetUnitVector();
		Vector otherUnit = other.GetUnitVector();

		return thisUnit == otherUnit;
	}

	public bool SameDirectionAs(params Vector[] others) {

		if (others.Any(x => x == Zero)) {
			throw new ArgumentException();
		}

		return others.All(SameDirectionAs);
	}

	public bool OppositeDirectionAs(Vector other) {

		if (this == Zero || other == Zero) {
			throw new ArgumentException();
		}

		// neither of these can actually be null
		Vector thisUnit = GetUnitVector();
		Vector otherUnit = other.GetUnitVector();

		return thisUnit == -1 * otherUnit;
	}

	public bool OppositeDirectionAs(params Vector[] others) {

		if (others.Any(x => x == Zero)) {
			throw new ArgumentException();
		}

		return others.All(OppositeDirectionAs);
	}

	public Vector GetUnitVector() {

		if (this == Zero) {
			throw new ArgumentException();
		}

		double magnitude = double.Sqrt(X*X + Y*Y);

		return new() { X = X / magnitude, Y = Y / magnitude };
	}



	public static bool operator ==(Vector left, Vector right) {
		return left.X == right.X && left.Y == right.Y;
	}

	public static bool operator !=(Vector left, Vector right) {
		return !(left == right);
	}

	public static Vector operator *(Vector left, double right) {
		return new() { X = left.X * right, Y = left.Y * right };
	}

	public static Vector operator *(double left, Vector right) {
		return right * left;
	}

	public static Vector operator /(Vector left, double right) {
		return new() { X = left.X / right, Y = left.Y / right };
	}

	public static Vector operator /(double left, Vector right) {
		return right / left;
	}

	public static Vector operator +(Vector left, Vector right) {
		return new() { X = left.X + right.X, Y = left.Y + right.Y};
	}

	public static Vector operator -(Vector left, Vector right) {
		return left + -1 * right;
	}

}
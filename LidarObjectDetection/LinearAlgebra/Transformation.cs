namespace LinearAlgebra; 



public static class Transformation {

	public static Point2 Translate(this Point2 point, Vector2 offset) {

		return new() { X = point.X + offset.X, Y = point.Y + offset.Y };
	}

	public static Point3 Translate(this Point3 point, Vector3 offset) {

		return new() { X = point.X + offset.X, Y = point.Y + offset.Y,Z = point.Z + offset.Z };
	}

	public static Polygon Translate(this Polygon polygon, Vector2 offset) {

		return Polygon.Create(polygon.Points.Select(x => x.Translate(offset)))!;
	}



	/// <summary>
	/// Rotates a point around the point {X=0, Y=0}
	/// </summary>
	/// <param name="point"></param>
	/// <param name="angle">Angle in degrees.</param>
	/// <returns></returns>
	public static Point2 Rotate(this Point2 point, double angle) {

		// todo would be nice to properly implement matrix math
		// this is just applying a rotation matrix

		double cosTheta = Math.Cos(angle * Math.PI / 180);
		double sinTheta = Math.Sin(angle * Math.PI / 180);

		return new() {
			X = cosTheta * point.X - sinTheta * point.Y,
			Y =	sinTheta * point.X + cosTheta * point.Y
		};
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="polygon"></param>
	/// <param name="angle">Angle in degrees</param>
	/// <returns></returns>
	public static Polygon Rotate(this Polygon polygon, double angle) {

		return Polygon.Create(polygon.Points.Select(x => x.Rotate(angle)))!;
	}

}
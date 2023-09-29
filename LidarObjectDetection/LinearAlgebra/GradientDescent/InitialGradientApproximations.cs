namespace LinearAlgebra.GradientDescent;



public delegate Vector3 InitialGradientApproximation(Func<Point3, double> function, Point3 point, Vector3 approximationDeltaSize);



public static class InitialGradientApproximations {

	public static Vector3 Basic(Func<Point3, double> function, Point3 point, Vector3 approximationDeltaSize) {

		double valueAtPoint = function(point);

		double valueAtXOffset = function(point.Translate(new() { X = approximationDeltaSize.X, Y = 0, Z = 0 }));
		double valueAtYOffset = function(point.Translate(new() { X = 0, Y = approximationDeltaSize.Y, Z = 0 }));
		double valueAtZOffset = function(point.Translate(new() { X = 0, Y = 0, Z = approximationDeltaSize.Z }));

		double xPartial = (valueAtXOffset - valueAtPoint) / approximationDeltaSize.X;
		double yPartial = (valueAtYOffset - valueAtPoint) / approximationDeltaSize.Y;
		double zPartial = (valueAtZOffset - valueAtPoint) / approximationDeltaSize.Z;

		return new() { X = xPartial, Y = yPartial, Z = zPartial };
	}

}
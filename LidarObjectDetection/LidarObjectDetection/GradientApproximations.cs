using LinearAlgebra;

namespace LidarObjectDetection;




public delegate Vector3 GradientApproximation(Func<Point3, double> function, Point3 point);



public static class GradientApproximations {

	public static Vector3 Basic(Func<Point3, double> function, Point3 startingPoint) {

		double valueAtPoint = function(startingPoint);

		double valueAtXOffset = function(startingPoint.Translate(new() { X = DerivativeStepSize, Y = 0, Z = 0 }));
		double valueAtYOffset = function(startingPoint.Translate(new() { X = 0, Y = DerivativeStepSize, Z = 0 }));
		double valueAtZOffset = function(startingPoint.Translate(new() { X = 0, Y = 0, Z = DerivativeStepSize }));

		double xPartial = (valueAtXOffset - valueAtPoint) / DerivativeStepSize;
		double yPartial = (valueAtYOffset - valueAtPoint) / DerivativeStepSize;
		double zPartial = (valueAtZOffset - valueAtPoint) / DerivativeStepSize;

		return new() { X = xPartial, Y = yPartial, Z = zPartial };
	}

}
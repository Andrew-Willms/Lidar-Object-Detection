using System;

namespace LinearAlgebra.GradientDescent; 



public static class GradientDescent {

	public static Point3? Descent(Func<Point3, double> function, Point3 startingPoint, GradientDescentParameters parameters) {

		Point3 previousPoint = startingPoint;
		Vector3 previousGradient = parameters.InitialGradientApproximation(function, startingPoint, parameters.ApproximationDeltaSize);

		while (true) {

			Vector3 gradient = parameters.GradientApproximation(function, startingPoint, previousGradient, parameters.ApproximationDeltaSize);
			Vector3 step = parameters.StepSizeCalculator(gradient);
			Point3 point = previousPoint.Translate(step);

			previousPoint = point;
			previousGradient = gradient;

			if (parameters.ConvergenceCriteria()) {
				return point;
			}

			if (parameters.FailureCriteria()) {
				return null;
			}
		}
	}

}
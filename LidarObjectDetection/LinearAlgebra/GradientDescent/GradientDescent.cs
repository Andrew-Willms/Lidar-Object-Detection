using System;

namespace LinearAlgebra.GradientDescent; 



public static class GradientDescent {

	public static Point3? Descent(Func<Point3, double> function, Point3 startingPoint, GradientDescentParameters parameters) {

		Vector3 previousGradient = parameters.GradientApproximation(function, startingPoint, parameters.ApproximationDeltaSize);
		Vector3 step = parameters.InitialStepCalculator(previousGradient);
		Point3 previousPoint = startingPoint.Translate(step);

		while (true) {

			Vector3 gradient = parameters.GradientApproximation(function, startingPoint, parameters.ApproximationDeltaSize);
			step = parameters.StepCalculator(previousGradient, gradient);
			Point3 point = previousPoint.Translate(step);

			previousPoint = point;

			if (parameters.ConvergenceCriteria()) {
				return point;
			}

			if (parameters.FailureCriteria()) {
				return null;
			}
		}
	}

}
using System;

namespace LinearAlgebra.GradientDescent; 



public static class GradientDescent {

	public static Point3? Descent(Func<Point3, double> function, Point3 startingPoint, GradientDescentParameters parameters, out GradientDescentData data) {

		IConvergenceDecider convergenceDecider = parameters.ConvergenceDeciderFactory();
		IDivergenceDecider divergenceDecider = parameters.DivergenceDeciderFactory();

		Vector3 previousNegativeGradient = -parameters.InitialGradientApproximation(function, startingPoint);
		Vector3 previousStep = parameters.InitialStepCalculator(previousNegativeGradient);
		Point3 previousPoint = startingPoint;
		double previousError = function(startingPoint);

		data = new() { Parameters = parameters };
		data.Gradients.Add(previousNegativeGradient);
		data.Steps.Add(previousStep);
		data.Points.Add(previousPoint);

		while (true) {

			Vector3 negativeGradient = -parameters.GradientApproximation(function, previousPoint, previousStep);
			Vector3 step = parameters.StepCalculator(previousStep, previousNegativeGradient, negativeGradient);
			step = step * 1 + previousStep * 0;
			Point3 point = previousPoint.Translated(step);
			double errorValue = function(point);

			int retryCount = 0;
			while (retryCount < 10 && errorValue >= previousError) {

				step /= 2;
				point = previousPoint.Translated(step);
				errorValue = function(point);

				retryCount++;
			}

			data.Gradients.Add(negativeGradient);
			data.Steps.Add(step);
			data.Points.Add(point);
			data.Errors.Add(errorValue);

			previousPoint = point;
			previousStep = step;
			previousNegativeGradient = negativeGradient;
			previousError = errorValue;

			if (convergenceDecider.HasConverged(point, errorValue, negativeGradient, step)) {
				return point;
			}

			if (divergenceDecider.HasDiverged(point, negativeGradient, step)) {
				//return null;
			}
		}
	}

}
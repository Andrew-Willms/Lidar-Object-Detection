using System;

namespace LinearAlgebra.GradientDescent; 



public static class GradientDescent {

	public static Point3? Descent(Func<Point3, double> function, Point3 startingPoint, GradientDescentParameters parameters, out GradientDescentData data) {

		IConvergenceDecider convergenceDecider = parameters.ConvergenceDeciderFactory();
		IDivergenceDecider divergenceDecider = parameters.DivergenceDeciderFactory();

		Vector3 previousNegativeGradient = -parameters.InitialGradientApproximation(function, startingPoint);
		Vector3 step = parameters.InitialStepCalculator(previousNegativeGradient);
		Point3 previousPoint = startingPoint;

		data = new() { Parameters = parameters };
		data.Gradients.Add(previousNegativeGradient);
		data.Steps.Add(step);
		data.Points.Add(previousPoint);

		while (true) {

			Vector3 negativeGradient = -parameters.GradientApproximation(function, previousPoint, step);
			step = parameters.StepCalculator(step, previousNegativeGradient, negativeGradient);
			Point3 point = previousPoint.Translated(step);

			data.Gradients.Add(negativeGradient);
			data.Steps.Add(step);
			data.Points.Add(point);
			data.Errors.Add(function(point));

			if (data.Errors.Count == 800) {
				Console.WriteLine();
			}

			previousPoint = point;
			previousNegativeGradient = negativeGradient;

			if (convergenceDecider.HasConverged(point, negativeGradient, step)) {
				return point;
			}

			if (divergenceDecider.HasDiverged(point, negativeGradient, step)) {
				return null;
			}
		}
	}

}
using System;

namespace LinearAlgebra.GradientDescent; 



public static class GradientDescent {

	public static Point3? Descent(Func<Point3, double> function, Point3 startingPoint, GradientDescentParameters parameters, out GradientDescentData data) {

		IConvergenceDecider convergenceDecider = parameters.ConvergenceDeciderFactory();
		IDivergenceDecider divergenceDecider = parameters.DivergenceDeciderFactory();

		Vector3 previousGradient = parameters.InitialNegativeGradientApproximation(function, startingPoint);
		Vector3 step = parameters.InitialStepCalculator(previousGradient);
		Point3 previousPoint = startingPoint;

		data = new() { Parameters = parameters };
		data.Gradients.Add(previousGradient);
		data.Steps.Add(step);
		data.Points.Add(previousPoint);

		while (true) {

			Vector3 gradient = parameters.NegativeGradientApproximation(function, previousPoint, step);
			step = parameters.StepCalculator(step, previousGradient, gradient);
			Point3 point = previousPoint.Translated(step);

			data.Gradients.Add(gradient);
			data.Steps.Add(step);
			data.Points.Add(point);
			data.Errors.Add(function(point));

			if (data.Errors.Count == 800) {
				Console.WriteLine();
			}

			previousPoint = point;
			previousGradient = gradient;

			if (convergenceDecider.HasConverged(point, gradient, step)) {
				return point;
			}

			if (divergenceDecider.HasDiverged(point, gradient, step)) {
				return null;
			}
		}
	}

}
using System;

namespace LinearAlgebra.GradientDescent; 



public static class GradientDescent {

#if DEBUG
	public static Point3? Descent(Func<Point3, double> function, Point3 startingPoint, GradientDescentParameters parameters, out GradientDescentData data) {
#else
	public static Point3? Descent(Func<Point3, double> function, Point3 startingPoint, GradientDescentParameters parameters) {
#endif
		Vector3 previousGradient = parameters.InitialGradientApproximation(function, startingPoint);
		Vector3 step = parameters.InitialStepCalculator(previousGradient);
		Point3 previousPoint = startingPoint.Translate(step);
#if DEBUG
		data = new() { Parameters = parameters };
		data.Gradients.Add(previousGradient);
		data.Steps.Add(step);
		data.Points.Add(previousPoint);
#endif
		while (true) {

			Vector3 gradient = parameters.GradientApproximation(function, startingPoint, step);
			step = parameters.StepCalculator(step, previousGradient, gradient);
			Point3 point = previousPoint.Translate(step);
#if DEBUG
			data.Gradients.Add(gradient);
			data.Steps.Add(step);
			data.Points.Add(point);
#endif
			previousPoint = point;
			previousGradient = gradient;

			if (parameters.ConvergenceDecider.HasConverged(point, gradient, step)) {
				return point;
			}

			if (parameters.DivergenceDecider.HasDiverged(point, gradient, step)) {
				return null;
			}
		}
	}

}
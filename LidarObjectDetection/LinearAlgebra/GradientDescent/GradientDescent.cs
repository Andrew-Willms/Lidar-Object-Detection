using System;

namespace LinearAlgebra.GradientDescent; 



public static class GradientDescent {

	public static Point3? Descent(Func<Point3, double> function, Point3 startingPoint, GradientDescentParameters parameters
#if DEBUG
		, out GradientDescentData data
#endif
		) {

		Vector3 previousGradient = parameters.GradientApproximation(function, startingPoint, parameters.ApproximationDeltaSize);
		Vector3 step = parameters.InitialStepCalculator(previousGradient);
		Point3 previousPoint = startingPoint.Translate(step);

#if DEBUG
		data = new() { Parameters = parameters };
		data.Gradients.Add(previousGradient);
		data.Steps.Add(step);
		data.Points.Add(previousPoint);
#endif

		while (true) {

			Vector3 gradient = parameters.GradientApproximation(function, startingPoint, parameters.ApproximationDeltaSize);
			step = parameters.StepCalculator(previousGradient, gradient);
			Point3 point = previousPoint.Translate(step);

			previousPoint = point;

#if DEBUG
			data.Gradients.Add(previousGradient);
			data.Steps.Add(step);
			data.Points.Add(previousPoint);
#endif

			if (parameters.ConvergenceCriteria()) {
				return point;
			}

			if (parameters.FailureCriteria()) {
				return null;
			}
		}
	}

}
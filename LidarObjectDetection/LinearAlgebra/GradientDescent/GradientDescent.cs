namespace LinearAlgebra.GradientDescent; 



public static class GradientDescent {

	public static Vector3 Descent(Func<Point3, double> function, Point3 startingPoint, GradientDescentParameters parameters) {

		Vector3 gradient = parameters.InitialGradientApproximation(function, startingPoint, parameters.ApproximationDeltaSize);

		Vector3 step = parameters.StepSizeCalculator(gradient);

		throw new NotImplementedException();
	}

}
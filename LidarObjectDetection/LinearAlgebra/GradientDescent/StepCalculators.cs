using System;

namespace LinearAlgebra.GradientDescent;



public delegate Vector3 InitialStepCalculator(Vector3 gradient);

public delegate Vector3 StepCalculator(Vector3 previousGradient, Vector3 gradient);





public static class StepCalculators {

	// todo: figure out if step size is relevant, if I my space is 1m x 1m x 360 degrees do I want to traverse the degrees 360x faster?

	public static Vector3 LimitedScalingFactor(Vector3 scalingFactor, Vector3 maximumStepSize) {

		throw new NotImplementedException();
	}

}
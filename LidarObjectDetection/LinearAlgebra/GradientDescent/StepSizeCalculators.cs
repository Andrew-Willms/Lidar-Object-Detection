using System;

namespace LinearAlgebra.GradientDescent;



public delegate Vector3 StepSizeCalculator(Vector3 gradient);



public static class StepSizeCalculators {

	public static Vector3 LimitedScalingFactor(Vector3 scalingFactor, Vector3 maximumStepSize) {

		throw new NotImplementedException();
	}

}
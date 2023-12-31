﻿namespace LinearAlgebra.GradientDescent;



public delegate Vector3 InitialStepCalculator(Vector3 gradient);

public delegate Vector3 StepCalculator(Vector3 previousStep, Vector3 previousGradient, Vector3 gradient);





public static class StepCalculators {

	public static Vector3 ConstantStep(Vector3 gradient, double constantSize) {

		return gradient.GetUnitVector() * constantSize;
	}

	public static Vector3 ScaleGradient(Vector3 gradient, double scalingFactor) {

		return gradient * scalingFactor;
	}

}
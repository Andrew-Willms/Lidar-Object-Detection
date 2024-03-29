﻿using System;

namespace LinearAlgebra.GradientDescent;



public delegate Vector3 InitialStepCalculator(Vector3 gradient);

public delegate Vector3 StepCalculator(Vector3 previousStep, Vector3 previousGradient, Vector3 gradient);



public static class InitialStepCalculators {

	public static Vector3 ConstantStep(Vector3 gradient, double constantSize) {

		if (gradient == Vector3.Zero) {
			return Vector3.Zero;
		}

		return gradient.GetUnitVector() * constantSize;
	}

	public static Vector3 ScaleGradient(Vector3 gradient, double scalingFactor) {

		return gradient * scalingFactor;
	}

}

public static class StepCalculators {

	public static Vector3 ConstantStep(Vector3 previousStep, Vector3 previousGradient, Vector3 gradient, double constantSize) {

		if (gradient == Vector3.Zero) {
			return Vector3.Zero;
		}

		return gradient.GetUnitVector() * constantSize;
	}

	public static Vector3 ScaleGradient(Vector3 previousStep, Vector3 previousGradient, Vector3 gradient, double scalingFactor) {

		return gradient * scalingFactor;
	}

	public static Vector3 ScaleGradientByPart(Vector3 previousStep, Vector3 previousGradient, Vector3 gradient, Vector3 scalingFactor) {

		return new(gradient.X * scalingFactor.X, gradient.Y * scalingFactor.Y, gradient.Z * scalingFactor.Z);
	}

	public static Vector3 LimitedScaleGradientByPart(Vector3 previousStep, Vector3 previousGradient, Vector3 gradient, Vector3 scalingFactor, Vector3 limit) {

		double x = Math.Clamp(gradient.X * scalingFactor.X, -limit.X, limit.X);
		double y = Math.Clamp(gradient.Y * scalingFactor.Y, -limit.Y, limit.Y);
		double z = Math.Clamp(gradient.Z * scalingFactor.Z, -limit.Z, limit.Z);

		return new(x, y, z);
	}

}
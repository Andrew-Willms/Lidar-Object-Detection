using System;

namespace LinearAlgebra.GradientDescent;



public delegate Vector3 InitialStepCalculator(Vector3 gradient);

public delegate Vector3 StepCalculator(Vector3 previousStep, Vector3 previousGradient, Vector3 gradient);





public static class StepCalculators {

	// todo add a step counter with "inertia"

}
using System;

namespace LinearAlgebra.GradientDescent;



public class GradientDescentParameters {

	public required InitialGradientApproximation InitialGradientApproximation { get; init; }

	public required GradientApproximation GradientApproximation { get; init; }

	public required InitialStepCalculator InitialStepCalculator { get; init; }

	public required StepCalculator StepCalculator {get; init; }

	public required IConvergenceDecider ConvergenceDecider { get; init; }

	public required IDivergenceDecider DivergenceDecider { get; set; }

}
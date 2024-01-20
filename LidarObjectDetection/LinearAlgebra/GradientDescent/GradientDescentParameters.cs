using System;

namespace LinearAlgebra.GradientDescent;



public class GradientDescentParameters {

	public required InitialGradientApproximation InitialGradientApproximation { get; init; }

	public required GradientApproximation GradientApproximation { get; init; }

	public required InitialStepCalculator InitialStepCalculator { get; init; }

	public required StepCalculator StepCalculator {get; init; }

	public required Func<IConvergenceDecider> ConvergenceDeciderFactory { get; init; }

	public required Func<IDivergenceDecider> DivergenceDeciderFactory { get; init; }

	//public required RectangularRegion SearchBounds { get; init; } // todo, limit search from trying beyond certain limits, maybe include wrapping rotation

}
namespace LinearAlgebra.GradientDescent;



public class GradientDescentParameters {

	public required InitialNegativeGradientApproximation InitialNegativeGradientApproximation { get; init; }

	public required NegativeGradientApproximation NegativeGradientApproximation { get; init; }

	public required InitialStepCalculator InitialStepCalculator { get; init; }

	public required StepCalculator StepCalculator {get; init; }

	public required IConvergenceDecider ConvergenceDecider { get; init; }

	public required IDivergenceDecider DivergenceDecider { get; init; }

	//public required RectangularRegion SearchBounds { get; init; } // todo, limit search from trying beyond certain limits, maybe include wrapping rotation

}
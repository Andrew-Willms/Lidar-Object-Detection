namespace LinearAlgebra.GradientDescent;



public class GradientDescentParameters {

	public required InitialGradientApproximation InitialGradientApproximation { get; init; }

	public required GradientApproximation GradientApproximation { get; init; }

	public required Vector3 ApproximationDeltaSize { get; init; }

	public required StepSizeCalculator StepSizeCalculator {get; init; }

}
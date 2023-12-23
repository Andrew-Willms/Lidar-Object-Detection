namespace LinearAlgebra.GradientDescent;



public class GradientDescentParameters {

	public required InitialGradientApproximation InitialGradientApproximation { get; init; }

	public required GradientApproximation GradientApproximation { get; init; }

	public required Vector3 ApproximationDeltaSize { get; init; }

	public required StepSizeCalculator StepSizeCalculator {get; init; }

	public required ConvergenceCriteria ConvergenceCriteria { get; init; }

	public required FailureCriteria FailureCriteria { get; set; }

}



public delegate bool ConvergenceCriteria();

public delegate bool FailureCriteria();
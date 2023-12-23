namespace LinearAlgebra.GradientDescent;



public class GradientDescentParameters {

	public required GradientApproximation GradientApproximation { get; init; }

	public required Vector3 ApproximationDeltaSize { get; init; }

	public required InitialStepCalculator InitialStepCalculator { get; init; }

	public required StepCalculator StepCalculator {get; init; }

	public required ConvergenceCriteria ConvergenceCriteria { get; init; }

	public required FailureCriteria FailureCriteria { get; set; }

}



public delegate bool ConvergenceCriteria();

public delegate bool FailureCriteria();
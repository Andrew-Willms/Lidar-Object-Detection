namespace LinearAlgebra.GradientDescent;



public class GradientDescentParameters {

	public required InitialGradientApproximation InitialGradientApproximation { get; init; }

	public required GradientApproximation GradientApproximation { get; init; }

	public required InitialStepCalculator InitialStepCalculator { get; init; }

	public required StepCalculator StepCalculator {get; init; }

	public required ConvergenceCriteria ConvergenceCriteria { get; init; }

	public required FailureCriteria FailureCriteria { get; set; }

}



// todo convert these into interfaces so they can keep track of whatever state they want to determine success or failure?
public delegate bool ConvergenceCriteria(Point3 previousPoint, Point3 point, Vector3 previousGradient, Vector3 gradient);

public delegate bool FailureCriteria(int iterations, Point3 previousPoint, Point3 point, Vector3 previousGradient, Vector3 gradient);
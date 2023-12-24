namespace LinearAlgebra.GradientDescent;



public interface IDivergenceDecider {

	public bool HasDiverged(Point3 point, Vector3 gradient, Vector3 step);

}



public class IterationDivergenceDecider : IDivergenceDecider {

	public required int MaxAllowedIterations { get; set; }

	private int IterationCount = 0;

	public bool HasDiverged(Point3 point, Vector3 gradient, Vector3 step) {

		IterationCount++;

		return IterationCount > MaxAllowedIterations;
	}

}
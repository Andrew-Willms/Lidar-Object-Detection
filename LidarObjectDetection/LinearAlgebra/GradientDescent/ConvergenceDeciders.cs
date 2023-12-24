namespace LinearAlgebra.GradientDescent; 



public interface IConvergenceDecider {

	public bool HasConverged(Point3 point, Vector3 gradient, Vector3 step);

}



public class ConsecutiveSmallGradientAndPointChange() : IConvergenceDecider {

	public required int ConsecutiveSmallIterationsRequired { get; set; }

	public required double GradientThreshold { get; set; }

	public required	double PointChangeThreshold { get; set; }

	public required Vector3 PointComponentScalingFactor { get; set; }



	private int ConsecutiveSmallIterations = 0;

	private Point3 PreviousPoint = default;



	public bool HasConverged(Point3 point, Vector3 gradient, Vector3 step) {

		point = new() {
			X = point.X * PointComponentScalingFactor.X,
			Y = point.Y * PointComponentScalingFactor.Y,
			Z = point.Z * PointComponentScalingFactor.Z
		};

		if (gradient.Magnitude < GradientThreshold && point.DistanceFrom(PreviousPoint) < PointChangeThreshold) {
			ConsecutiveSmallIterations++;
		}

		PreviousPoint = point;

		return ConsecutiveSmallIterations == ConsecutiveSmallIterationsRequired;
	}

}
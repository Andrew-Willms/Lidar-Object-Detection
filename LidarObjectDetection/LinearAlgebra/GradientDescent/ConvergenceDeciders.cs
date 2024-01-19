using System;
using static System.Double;

namespace LinearAlgebra.GradientDescent; 



public interface IConvergenceDecider {

	public bool HasConverged(Point3 point, Vector3 gradient, Vector3 step);

}



public class ConsecutiveSmallGradientAndPointChange : IConvergenceDecider {

	public required int MaxAllowedIterations { get; set; }
	private int IterationCount = 0;

	public required int ConsecutiveSmallIterationsRequired { get; set; }

	public required double GradientThreshold { get; set; }

	public required Vector3 PointChangeThreshold { get; set; }



	private int ConsecutiveSmallIterations;

	private Point3 PreviousPoint = new() { X = MaxValue, Y = MaxValue, Z = MaxValue};



	public bool HasConverged(Point3 point, Vector3 gradient, Vector3 step) {

		IterationCount++;
		if (IterationCount > MaxAllowedIterations) {
			return true;
		}

		return false;

		if (gradient.Magnitude < GradientThreshold
		    && Math.Abs(point.X - PreviousPoint.X) > PointChangeThreshold.X
		    && Math.Abs(point.Y - PreviousPoint.Y) > PointChangeThreshold.Y
		    && Math.Abs(point.Z - PreviousPoint.Z) > PointChangeThreshold.Z) {

			ConsecutiveSmallIterations++;
		}

		PreviousPoint = point;

		return ConsecutiveSmallIterations == ConsecutiveSmallIterationsRequired;
	}

}
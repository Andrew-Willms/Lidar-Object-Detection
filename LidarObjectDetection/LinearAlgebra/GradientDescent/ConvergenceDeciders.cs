using System;
using System.Diagnostics;
using static System.Double;

namespace LinearAlgebra.GradientDescent; 



public interface IConvergenceDecider {

	public bool HasConverged(Point3 point, double error, Vector3 gradient, Vector3 step);

}



public class ConsecutiveSmallGradientAndPointChange : IConvergenceDecider {

	public required int MaxAllowedIterations { get; init; }
	private int IterationCount;

	public required int ConsecutiveSmallIterationsRequired { get; init; }

	//public required double GradientThreshold { get; init; }

	public required Vector3 PointChangeThreshold { get; init; }
	public required double ErrorChangeThreshold { get; init; }



	private int ConsecutiveSmallIterations;

	private Point3 PreviousPoint = new() { X = MaxValue, Y = MaxValue, Z = MaxValue};

	private double PreviousError = MaxValue;


	public bool HasConverged(Point3 point, double error, Vector3 gradient, Vector3 step) {

		if (IterationCount > MaxAllowedIterations) {
			return true;
		}

		IterationCount++;

		if (error < ErrorChangeThreshold) {
			Trace.WriteLine("here");
		}

		if (Math.Abs(error - PreviousError) < ErrorChangeThreshold
		    && Math.Abs(point.X - PreviousPoint.X) < PointChangeThreshold.X
		    && Math.Abs(point.Y - PreviousPoint.Y) < PointChangeThreshold.Y
		    && Math.Abs(point.Z - PreviousPoint.Z) < PointChangeThreshold.Z) {

			ConsecutiveSmallIterations++;
		}

		PreviousPoint = point;
		PreviousError = error;

		if (ConsecutiveSmallIterations == ConsecutiveSmallIterationsRequired) {
			Trace.WriteLine("here");
		}

		if (ConsecutiveSmallIterations == ConsecutiveSmallIterationsRequired) {
			Trace.WriteLine("here");
		}

		return ConsecutiveSmallIterations == ConsecutiveSmallIterationsRequired;
	}

}
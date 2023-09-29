using LinearAlgebra;
using LinearAlgebra.GradientDescent;

namespace LidarObjectDetection;



public static class Detection {

	public static Point3 Detect(Point2[] lidarPoints, Polygon crossSection, DetectionParameters parameters) {

		throw new NotImplementedException();
	}

}


public class DetectionParameters {

	public required int StartingPointCount { get; init; }

	public required RectangularRegion SearchRegion { get; init; }

	public required StartingPointDistributor StartingPointDistributor { get; init; }



	public required GradientDescentParameters GradientDescentParameters { get; init; }



	public required LeastDistanceCalculatorCreator LeastDistanceCalculatorCreator { get; init; }

	public required CumulativeErrorFunction CumulativeErrorFunction { get; init; }



	public required Point3 PreviousPosition { get; init; }

	public required Point3 RobotVelocity { get; init; }

}

public delegate Point3[] StartingPointDistributor(int startingPointCount, RectangularRegion searchRegion);



public delegate bool ConvergenceCriteria();
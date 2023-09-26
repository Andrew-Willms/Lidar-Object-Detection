using LinearAlgebra;

namespace LidarObjectDetection;



public static class Detection {

	public static Point3[] Detect(Point2[] lidarPoints, Polygon crossSection, DetectionParameters parameters) {

		throw new NotImplementedException();
	}

}


public class DetectionParameters {

	public required int StartingPointCount { get; init; }

	public required RectangularRegion SearchRegion { get; init; }

	public required StartingPointDistributor StartingPointDistributor { get; init; }



	public required GradientDescentParameters GradientDescentParameters { get; init; }



	public required NearestDistanceCalculator NearestDistanceCalculator { get; init; }

	public required CumulativeErrorFunction CumulativeErrorFunction { get; init; }



	public required Point3 PreviousPosition { get; init; }

	public required Point3 RobotVelocity { get; init; }

}

public delegate Point3[] StartingPointDistributor(int startingPointCount, RectangularRegion searchRegion);

public delegate double NearestDistanceCalculator(Point2[] otherPoints, Point2 point);

public delegate Point3[] CumulativeErrorFunction(double[] errors);



public class GradientDescentParameters {




}

public delegate void GradientApproximation(Func<Point3, double> function, Point3 point);

public delegate bool ConvergenceCriteria();
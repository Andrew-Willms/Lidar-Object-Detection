using LinearAlgebra.GradientDescent;
using LinearAlgebra;

namespace LidarObjectDetection;



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
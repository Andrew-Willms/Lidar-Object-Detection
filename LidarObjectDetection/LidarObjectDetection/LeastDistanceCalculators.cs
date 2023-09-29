using LinearAlgebra;

namespace LidarObjectDetection;



public delegate ILeastDistanceCalculator LeastDistanceCalculatorCreator(Point2[] otherPoints);



public interface ILeastDistanceCalculator {

	public double DistanceTo(Point2 point);

}



public class LeastDistanceCalculator : ILeastDistanceCalculator {

	private Point2[] OtherPoints { get; }

	private LeastDistanceCalculator(Point2[] otherPoints) {
		OtherPoints = otherPoints;
	}

	public static LeastDistanceCalculator? Create(Point2[] otherPoints) {

		return otherPoints.Any() 
			? new(otherPoints) 
			: null;
	}

	public double DistanceTo(Point2 point) {
		return OtherPoints.Select(otherPoint => new Vector2(otherPoint, point).Magnitude).Min();
	}

}
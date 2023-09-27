using LinearAlgebra;

namespace LidarObjectDetection;



public delegate double NearestDistanceCalculator(Point2[] otherPoints, Point2 point);



public class NearestDistanceCalculators {

	public double Dumb(Point2[] otherPoints, Point2 point) {

		return otherPoints.Select(otherPoint => new Vector2(otherPoint, point).Magnitude).Min();
	}

}
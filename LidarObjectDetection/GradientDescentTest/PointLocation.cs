using LinearAlgebra;

namespace GradientDescentTest; 



public static class PointLocation {



	public static double DistanceToNearestLidarPoint(IEnumerable<Point2> lidarPoints, Point2 point) {

		return lidarPoints.Select(lidarPoint => new Vector2(lidarPoint, point).Magnitude).Min();
	}

	public static double AverageDistanceToNearestLidarPoint(IEnumerable<Point2> lidarPoints, IEnumerable<Point2> points) {

		return points.Average(x => DistanceToNearestLidarPoint(lidarPoints, x));
	}

}
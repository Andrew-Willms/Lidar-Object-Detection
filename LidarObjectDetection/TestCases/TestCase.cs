using System.Collections.Generic;
using System.Collections.Immutable;
using LidarObjectDetection;
using LinearAlgebra;
using LinearAlgebra.GradientDescent;

namespace TestCases; 



public class TestCase {

	public required Polygon ShapeToFind { get; init; }

	public required World World { get; init; }

	public required LidarScanner Lidar { get; init; }

	public required Vector2 LidarOffset { get; init; }

	public required double LidarRotation { get; init; }

	public required DetectionParameters DetectionParameters { get; init; }

#if DEBUG
	public (Point3? position, List<GradientDescentData> data) Execute() {

		ImmutableArray<Point2> lidarPoints = GetLidarData();

		return Detection.Detect(lidarPoints, ShapeToFind, Lidar, LidarOffset, LidarRotation, DetectionParameters);
	}
#else
	public Point3? Execute() {

		ImmutableArray<Point2> lidarPoints = Lidar.ScanInLidarCoords(World, LidarOffset, LidarRotation);

		return Detection.Detect(lidarPoints, ShapeToFind, Lidar, LidarOffset, LidarRotation, DetectionParameters);
	}
#endif

	public ImmutableArray<Point2> GetLidarData() {
		return Lidar.ScanInLidarCoords(World, LidarOffset, LidarRotation);
	}

}
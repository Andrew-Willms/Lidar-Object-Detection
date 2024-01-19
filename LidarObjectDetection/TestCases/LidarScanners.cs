using LidarObjectDetection;
using LinearAlgebra;

namespace TestCases; 



public static class LidarScanners {

	public static LidarScanner Lidar21Beams1mWide = new() {
		Beams = new[] {
			new LineSegment(new(-1, 0), new Point2(-1, 5)),
			new LineSegment(new(-0.9, 0), new Point2(-0.9, 5)),
			new LineSegment(new(-0.8, 0), new Point2(-0.8, 5)),
			new LineSegment(new(-0.7, 0), new Point2(-0.7, 5)),
			new LineSegment(new(-0.6, 0), new Point2(-0.6, 5)),
			new LineSegment(new(-0.5, 0), new Point2(-0.5, 5)),
			new LineSegment(new(-0.4, 0), new Point2(-0.4, 5)),
			new LineSegment(new(-0.3, 0), new Point2(-0.3, 5)),
			new LineSegment(new(-0.2, 0), new Point2(-0.2, 5)),
			new LineSegment(new(-0.1, 0), new Point2(-0.1, 5)),
			new LineSegment(new(0, 0), new Point2(0, 5)),
			new LineSegment(new(0.1, 0), new Point2(0.1, 5)),
			new LineSegment(new(0.2, 0), new Point2(0.2, 5)),
			new LineSegment(new(0.3, 0), new Point2(0.3, 5)),
			new LineSegment(new(0.4, 0), new Point2(0.4, 5)),
			new LineSegment(new(0.5, 0), new Point2(0.5, 5)),
			new LineSegment(new(0.6, 0), new Point2(0.6, 5)),
			new LineSegment(new(0.7, 0), new Point2(0.7, 5)),
			new LineSegment(new(0.8, 0), new Point2(0.8, 5)),
			new LineSegment(new(0.9, 0), new Point2(0.9, 5)),
			new LineSegment(new(1, 0), new Point2(1, 5)),
		}
	};

}
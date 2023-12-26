using LinearAlgebra;

namespace LidarObjectDetection;



public class LidarScanner {

	public required Point2 Center { get; init; }

	public required LineSegment[] Beams { get; init; }

}
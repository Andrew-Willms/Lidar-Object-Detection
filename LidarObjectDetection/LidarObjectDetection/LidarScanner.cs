using LinearAlgebra;

namespace LidarObjectDetection;



public class LidarScanner {

	public required Point2 Center { get; init; }

	// remove?
	public required Vector2 ForwardsDirection { get; set; }

	public required LineSegment[] Beams { get; init; }

}
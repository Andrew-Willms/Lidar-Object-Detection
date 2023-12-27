using LinearAlgebra;

namespace LidarObjectDetection;



public readonly struct LidarScanner {

	public required Point2 Center { get; init; }

	public required Vector2 ForwardsDirection { get; init; }

	public required LineSegment[] Beams { get; init; }

}
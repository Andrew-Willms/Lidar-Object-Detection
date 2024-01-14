using System.Linq;
using LinearAlgebra;

namespace LidarObjectDetection;



public readonly struct LidarScanner {

	public LidarScanner() { }

	public Point2 Center { get; init; } = Point2.Origin;

	public required LineSegment[] Beams { get; init; }

	public Point2[] ScanWorld(World world, Vector2 offsetFromWorldCenter, double rotation) {

		Point2 center = Center;

		return Beams
			.Select(beam => beam.RotateAround(rotation, center))
			.Select(beams => beams.Translate(offsetFromWorldCenter))
			.Select(beam => world.Objects
				.Select(@object => @object.NearestIntersection(beam, beam.Start))
				.MinBy(intersection => intersection.DistanceFrom(beam.Start)))
			.ToArray();
	}

}
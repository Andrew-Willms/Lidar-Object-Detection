using System.Linq;
using LinearAlgebra;

namespace LidarObjectDetection;



public readonly struct LidarScanner {

	public LidarScanner() { }

	public Point2 Center { get; init; } = Point2.Origin;

	public required LineSegment[] Beams { get; init; }

	public Point2[] ScanInWorldCoord(World world, Vector2 lidarOffsetFromWorldCenter, double lidarRotation) {

		Point2 center = Center;

		return Beams
			.Select(beam => beam.RotateAround(lidarRotation, center))
			.Select(beams => beams.Translate(lidarOffsetFromWorldCenter))
			.Select(beam => world.Objects
				.Select(@object => @object.NearestIntersection(beam, beam.Start))
				.MinBy(intersection => intersection.DistanceFrom(beam.Start)))
			.ToArray();
	}

	public Point2[] ScanInLidarCoords(World world, Vector2 lidarOffsetFromWorldCenter, double lidarRotation) {

		Point2 center = Center;

		return Beams
			.Select(beam => world.Objects
				.Select(polygon => polygon.Translated(-lidarOffsetFromWorldCenter))
				.Select(polygon => polygon.Rotated(-lidarRotation))
				.Select(@object => @object.NearestIntersection(beam, beam.Start))
				.MinBy(intersection => intersection.DistanceFrom(beam.Start)))
			.ToArray();
	}

}
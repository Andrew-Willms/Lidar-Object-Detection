using System;
using System.Collections.Immutable;
using System.Linq;
using LinearAlgebra;

namespace LidarObjectDetection;



public readonly struct LidarScanner {

	public LidarScanner() { }

	public Point2 Center { get; init; } = Point2.Origin;

	public required LineSegment[] Beams { get; init; }

	public ImmutableArray<Point2> ScanInWorldCoord(World world, Vector2 lidarOffsetFromWorldCenter, double lidarRotation) {

		throw new NotImplementedException(); // there is probably a bug in this, it should be more like ScanInLidarCoords

		Point2 center = Center;

		return Beams
			.Select(beam => beam.RotateAround(lidarRotation, center))
			.Select(beams => beams.Translate(lidarOffsetFromWorldCenter))
			.Select(beam => world.Objects
				.Select(polygon => polygon.NearestIntersection(beam, beam.Start))
				.Where(intersection => intersection is not null)
				.Select(intersection => (Point2)intersection!)
				.MinBy(intersection => intersection.DistanceFrom(beam.Start)))
			.ToImmutableArray();
	}

	public ImmutableArray<Point2> ScanInLidarCoords(World world, Vector2 lidarOffsetFromWorldCenter, double lidarRotation) {

		return Beams
			.Select(beam => world.Objects
				.Select(polygon => polygon.Translated(-lidarOffsetFromWorldCenter))
				.Select(polygon => polygon.Rotated(-lidarRotation))
				.Select(polygon => polygon.NearestIntersection(beam, beam.Start))
				.Where(intersection => intersection is not null)
				.Select(intersection => (Point2)intersection!)
				.OrderBy(intersection => intersection.DistanceFrom(beam.Start)))
			.Where(intersectionList => intersectionList.Any())
			.Select(intersectionList => intersectionList.First())
			.ToImmutableArray();
	}

}
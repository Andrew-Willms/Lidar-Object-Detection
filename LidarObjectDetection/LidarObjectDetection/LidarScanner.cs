﻿using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using LinearAlgebra;
using LinqUtilities;

namespace LidarObjectDetection;



public readonly struct LidarScanner {

	public LidarScanner() { }

	public Point2 Center { get; init; } = Point2.Origin;

	public required LineSegment[] Beams { get; init; }

	public ImmutableArray<Point2> ScanInWorldCoord(World world, Vector2 lidarOffsetFromWorldCenter, double lidarRotation) {

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

		foreach (LineSegment beam in Beams) {

			foreach (Polygon worldObject in world.Objects
				         .Select(polygon => polygon.Translated(-lidarOffsetFromWorldCenter))
				         .Select(polygon => polygon.Rotated(-lidarRotation))) {

				Point2? nearestIntersection = worldObject.NearestIntersection(beam, beam.Start);
			}
		}

		return Beams
			.Select(beam => world.Objects
				.Select(polygon => polygon.Translated(-lidarOffsetFromWorldCenter))
				.Select(polygon => polygon.Rotated(-lidarRotation))
				.Select(polygon => polygon.NearestIntersection(beam, beam.Start))
				.Where(intersection => intersection is not null)
				.Select(intersection => (Point2)intersection!)
				.MinByOrDefault(intersection => intersection.DistanceFrom(beam.Start)))
			.ToImmutableArray();
	}

}
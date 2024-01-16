using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using LinqUtilities;

namespace LinearAlgebra;



public readonly struct Polygon : IEquatable<Polygon> {

	public ImmutableArray<Point2> Points { get; }

	public ImmutableArray<LineSegment> Edges { get; }



	public Polygon() {
		throw new InvalidOperationException("Use a parametered constructor.");
	}

	public Polygon(params LineSegment[] edges) : this(edges.ToImmutableArray()) { }

	public Polygon(ImmutableArray<LineSegment> edges) {

		if (edges.Length < 3) {
			throw new InvalidOperationException("Polygons must have at least three vertexes.");
		}

		Edges = edges;
		Points = edges
			.Select(lineSegment => lineSegment.Start)
			.ToImmutableArray();
	}

	public Polygon(params Point2[] points) : this(points.ToImmutableArray()) { }

	public Polygon(ImmutableArray<Point2> points) {

		if (points.Length < 3) {
			throw new InvalidOperationException("Polygons must have at least three vertexes.");
		}

		// if any adjacent points are identical
		if (points.AdjacentPairsWrapped().Any(x => x.first == x.second)) {
			throw new InvalidOperationException();
		}

		ImmutableArray<LineSegment> edges = points
			.AdjacentPairsWrapped()
			.Select(pointPair => new LineSegment(pointPair.first, pointPair.second))
			.ToImmutableArray();

		Edges = edges;
		Points = points;
	}



	public PolygonIntersection Intersection(LineSegment lineSegment) {

		return Edges.Select(x => x.Intersection(lineSegment)).Where(x => x is not null).ToArray();
	}

	public Point2? NearestIntersection(LineSegment lineSegment, Point2 point) {

		PolygonIntersection polygonIntersection = Intersection(lineSegment);

		if (polygonIntersection.AsT0.Length == 0) {
			return null;
		}

		return polygonIntersection.AsT0
			.SelectMany(intersection => intersection.Match<IEnumerable<Point2>>(
				pointIntersection => new[] { pointIntersection },
				lineIntersection => new[] { lineIntersection.Start, lineIntersection.End })
			)
			.MinBy(point.DistanceFrom);
	}



	public Polygon Rotated(double angle) {

		return new(Edges
			.Select(lineSegment => lineSegment.RotateAround(angle, Point2.Origin))
			.ToImmutableArray()
		);
	}

	public Polygon Translated(Vector2 offset) {

		return new(Edges
			.Select(lineSegment => lineSegment.Translate(offset))
			.ToImmutableArray()
		);
	}



	public static bool operator ==(Polygon left, Polygon right) {
		return left.Equals(right);
	}

	public static bool operator !=(Polygon left, Polygon right) {
		return !(left == right);
	}

	public bool Equals(Polygon other) {
		return Points.Equals(other.Points);
	}

	public override bool Equals(object? obj) {
		return obj is Polygon other && Equals(other);
	}

	public override int GetHashCode() {
		return HashCode.Combine(Points, Edges);
	}

}
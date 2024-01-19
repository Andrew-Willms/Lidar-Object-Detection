using System;
using System.Collections.Immutable;
using System.Linq;
using LinearAlgebra;

namespace LidarObjectDetection;



public delegate ILeastDistanceCalculator? LeastDistanceCalculatorCreator(ImmutableArray<Point2> otherPoints);



public interface ILeastDistanceCalculator {

	public double DistanceTo(Point2 point);

}



public readonly struct DumbLeastDistanceCalculator : ILeastDistanceCalculator {

	private ImmutableArray<Point2> OtherPoints { get; }

	public DumbLeastDistanceCalculator() {
		throw new InvalidOperationException("Use create function");
	}

	private DumbLeastDistanceCalculator(ImmutableArray<Point2> otherPoints) {
		OtherPoints = otherPoints;
	}

	public static DumbLeastDistanceCalculator? Create(ImmutableArray<Point2> otherPoints) {

		return otherPoints.Any() 
			? new(otherPoints) 
			: null;
	}

	public double DistanceTo(Point2 point) {

		double test = OtherPoints.Select(point.DistanceFrom).Min();

		return OtherPoints.Select(point.DistanceFrom).Min();
	}

}
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using LinearAlgebra;
using LinearAlgebra.GradientDescent;

namespace LidarObjectDetection;



public static class Detection {

	public static (Point3?, List<GradientDescentData>) Detect(ImmutableArray<Point2> lidarPoints,
		Polygon shapeToFind,
		LidarScanner lidar,
		Vector2 lidarOffset,
		double lidarRotation,
		DetectionParameters parameters) {

		ILeastDistanceCalculator? leastDistanceCalculator = parameters.LeastDistanceCalculatorCreator(lidarPoints); // todo translate to lidar coords
		if (leastDistanceCalculator is null) {
			return (null, new());
		}

		Func<Point3, double> errorFunction = point => {

			Polygon transformedShape = shapeToFind
				.Rotated(point.Z)
				.Translated(new(point.X, point.Y));

			World world = new(transformedShape);

			ImmutableArray<Point2> theoreticalLidarPoints = lidar.ScanInLidarCoords(world, Vector2.Zero, 0);

			if (theoreticalLidarPoints.Length == 0) {
				return double.MaxValue;
			}

			double[] errors = theoreticalLidarPoints.Select(leastDistanceCalculator.DistanceTo).ToArray();

			return parameters.CumulativeErrorFunction(errors);
		};


		Point3[] startingPoints = parameters.StartingPointDistributor(parameters.StartingPointCount, parameters.SearchRegion);

		List<Point3> localMinima = new();
		List<GradientDescentData> gradientDescentData = new();

		foreach (Point3 startingPoint in startingPoints) {

			Point3? result = GradientDescent.Descent(errorFunction, startingPoint, parameters.GradientDescentParameters, out GradientDescentData data);
			gradientDescentData.Add(data);

			if (result is not null) {
				localMinima.Add((Point3)result);
			}
		}

		if (localMinima.Count == 0) {
			return (null, gradientDescentData);
		}

		Point3 finalPoint = localMinima.MinBy(errorFunction);

		Point2 finalPoint2d = new Point2(finalPoint.X, finalPoint.Y)
			.Rotated(lidarRotation)
			.Translated(lidarOffset);

		finalPoint = new() { X = finalPoint2d.X, Y = finalPoint2d.Y, Z = finalPoint.Z + lidarRotation};
		return (finalPoint, gradientDescentData);
	}

}
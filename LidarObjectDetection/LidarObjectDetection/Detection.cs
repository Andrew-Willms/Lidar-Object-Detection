using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using LinearAlgebra;
using LinearAlgebra.GradientDescent;
using LinqUtilities;

namespace LidarObjectDetection;



public static class Detection {

#if DEBUG
	public static (Point3?, List<GradientDescentData>) Detect(ImmutableArray<Point2> lidarPoints, Polygon shapeToFind, LidarScanner lidar, Vector2 lidarOffset, double lidarRotation, DetectionParameters parameters) {
#else
	public static Point3? Detect(ImmutableArray<Point2> lidarPoints, Polygon shapeToFind, LidarScanner lidar, DetectionParameters parameters) {
#endif

		ILeastDistanceCalculator? leastDistanceCalculator = parameters.LeastDistanceCalculatorCreator(lidarPoints);
		if (leastDistanceCalculator is null) {
#if DEBUG
			return (null, new());
#else
			return null;
#endif
		}

		Func<Point3, double> errorFunction = point => {

			Polygon transformedShape = shapeToFind
				.Rotated(point.Z)
				.Translated(new(point.X, point.Y));

			World world = new();
			world.AddObject(transformedShape);

			ImmutableArray<Point2> theoreticalLidarPoints = lidar.ScanInLidarCoords(world, Vector2.Zero, 0);

			double[] errors = theoreticalLidarPoints.Select(leastDistanceCalculator.DistanceTo).ToArray();

			return parameters.CumulativeErrorFunction(errors);
		};


		Point3[] startingPoints = parameters.StartingPointDistributor(parameters.StartingPointCount, parameters.SearchRegion);

		List<Point3> localMinima = new();
		List<GradientDescentData> gradientDescentData = new();

		foreach (Point3 startingPoint in startingPoints) {
#if DEBUG
			Point3? result = GradientDescent.Descent(errorFunction, startingPoint, parameters.GradientDescentParameters, out GradientDescentData data);
			gradientDescentData.Add(data);
#else
			Point3? result = GradientDescent.Descent(parameters.GradientDescentParameters);
#endif
			if (result is not null) {
				localMinima.Add((Point3)result);
			}
		}

		Point3? finalPoint = localMinima.MinByOrDefault(errorFunction);
		if (finalPoint is not null) {

			Point3 finalPointNotNull = (Point3)finalPoint;
			Point2 finalPoint2d = new() { X = finalPointNotNull.X, Y = finalPointNotNull.Y};
			finalPoint2d.Rotated(lidarRotation).Translated(lidarOffset);
			finalPoint = new() { X = finalPoint2d.X, Y = finalPoint2d.Y, Z = finalPointNotNull.Z };
		}

#if DEBUG
		return (finalPoint, gradientDescentData);
#else
		return finalPoint;
#endif
	}

}
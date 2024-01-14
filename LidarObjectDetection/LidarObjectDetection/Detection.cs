using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using LinearAlgebra;
using LinearAlgebra.GradientDescent;

namespace LidarObjectDetection;



public static class Detection {

	public static Point3? Detect(Point2[] lidarPoints, Polygon shapeToFind, DetectionParameters parameters) {

		ILeastDistanceCalculator? leastDistanceCalculator = parameters.LeastDistanceCalculatorCreator(lidarPoints);
		if (leastDistanceCalculator is null) {
			return null;
		}

		Func<Point3, double> errorFunction = point => {

			Polygon transformedShape = shapeToFind
				.Rotated(point.Z)
				.Translated(new(point.X, point.Y));

			World world = new();
			world.AddObject(transformedShape);

			Point2 theoreticalLidarPoints = lidarPoints

			double[] errors = null!;

			return parameters.CumulativeErrorFunction(errors);
		};


		Point3[] startingPoints = parameters.StartingPointDistributor(parameters.StartingPointCount, parameters.SearchRegion);

		List<Point3> localMinima = new();

		foreach (Point3 startingPoint in startingPoints) {

#if DEBUG
			Point3? result = GradientDescent.Descent(errorFunction, startingPoint, parameters.GradientDescentParameters, out GradientDescentData data);
#else
			Point3? result = GradientDescent.Descent(parameters.GradientDescentParameters);
#endif
			if (result is not null) {
				localMinima.Add((Point3)result);
			}
		}

		return localMinima.MinBy(errorFunction);
	}

}
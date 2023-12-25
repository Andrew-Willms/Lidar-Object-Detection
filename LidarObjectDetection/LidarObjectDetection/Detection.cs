using System;
using System.Collections.Generic;
using System.Linq;
using LinearAlgebra;
using LinearAlgebra.GradientDescent;

namespace LidarObjectDetection;



public static class Detection {

	public static Point3? Detect(Point2[] lidarPoints, Polygon crossSection, DetectionParameters parameters) {



		Func<Point3, double> errorFunction = null!;


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

		return localMinima.OrderBy(errorFunction).FirstOrDefault();
	}

}
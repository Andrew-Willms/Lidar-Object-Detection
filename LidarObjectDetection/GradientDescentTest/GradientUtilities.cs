using LinearAlgebra;

namespace GradientDescentTest; 



public static class GradientUtilities {

	private const double DerivativeStepSize = 0.0000001;
	private const double ValueDifferenceConversionThreshold = 0.0001;
	private const double PointDifferenceConversionThreshold = 0.0001;
	private const double GradientStepScalingFactor = 1;
	private const double MaxGradientStepSize = 10;
	private const int MaxRetryCount = 10;



	public static Vector3 Gradient(Func<Point3, double> function, Point3 startingPoint) {

		double valueAtPoint = function(startingPoint);

		double valueAtXOffset = function(startingPoint.Translate(new() { X = DerivativeStepSize, Y = 0, Z = 0 }));
		double valueAtYOffset = function(startingPoint.Translate(new() { X = 0, Y = DerivativeStepSize, Z = 0 }));
		double valueAtZOffset = function(startingPoint.Translate(new() { X = 0, Y = 0, Z = DerivativeStepSize }));

		double xPartial = (valueAtXOffset - valueAtPoint) / DerivativeStepSize;
		double yPartial = (valueAtYOffset - valueAtPoint) / DerivativeStepSize;
		double zPartial = (valueAtZOffset - valueAtPoint) / DerivativeStepSize;

		return new() { X = xPartial, Y = yPartial, Z = zPartial };
	}

	public static Point3 GradientDescent(Func<Point3, double> function, Point3 startingPoint) {

		Point3 currentPoint = startingPoint;
		double currentValue = function(currentPoint);

		int iterationCount = 0;

		bool valueChangeWithinThreshold;
		bool pointChangeWithinThreshold;
		do {
			double previousValue = currentValue;
			Point3 previousPoint = currentPoint;

			Vector3 step = -1 * GradientStepScalingFactor * Gradient(function, previousPoint);
			step = Vector3.Min(step, step.GetUnitVector() * MaxGradientStepSize);

			int retryCount = 0;
			while (retryCount < MaxRetryCount && currentValue >= previousValue) {

				step /= 2;
				currentPoint = previousPoint.Translate(step);
				currentValue = function(currentPoint);

				retryCount++;
			}

			if (retryCount == MaxRetryCount && currentValue > previousValue) {
				currentPoint = previousPoint;
				currentValue = previousValue;
			}

			Console.WriteLine($"Step {step}");
			Console.WriteLine($"Current Point {currentPoint}");
			Console.WriteLine($"Current Value {currentValue}");
			Console.WriteLine("");

			valueChangeWithinThreshold = Math.Abs(currentValue - previousValue) < ValueDifferenceConversionThreshold;
			pointChangeWithinThreshold = new Vector3(currentPoint, previousPoint).Magnitude < PointDifferenceConversionThreshold;

			iterationCount++;

		} while (!valueChangeWithinThreshold || pointChangeWithinThreshold! || SecondDerivativesAreNegativeOrZero(function, currentPoint));

		Console.WriteLine(iterationCount);

		return currentPoint;
	}

	public static Point3 MultiStartGradientDescent(Func<Point3, double> function, Point3 rangeStart,
		Point3 rangeEnd, int xTestCount, int yTestCount, int zTestCount) {

		// not sure if I will use these long term but I am keeping them around for debugging for now
		Point3[,,] searchPoints = new Point3[xTestCount, yTestCount, zTestCount];
		Point3[,,] results = new Point3[xTestCount, yTestCount, zTestCount];

		double xStep = (rangeEnd.X - rangeStart.X) / xTestCount;
		double yStep = (rangeEnd.Y - rangeStart.Y) / yTestCount;
		double zStep = (rangeEnd.Z - rangeStart.Z) / zTestCount;

		double xStart = rangeStart.X + xStep / 2;
		double yStart = rangeStart.Y + yStep / 2;
		double zStart = rangeStart.Z + zStep / 2;

		Point3 minPoint = new() { X = xStart, Y = yStart, Z = zStart };
		double minValue = function(minPoint);

		for (int xIndex = 0; xIndex < xTestCount; xIndex++) {
			for (int yIndex = 0; yIndex < yTestCount; yIndex++) {
				for (int zIndex = 0; zIndex < zTestCount; zIndex++) {

					searchPoints[xIndex, yIndex, zIndex] = new() {
						X = xStart + xStep * xIndex,
						Y = yStart + yStep * yIndex,
						Z = zStart + zStep * zIndex
					};

					results[xIndex, yIndex, zIndex] = GradientDescent(function, searchPoints[xIndex, yIndex, zIndex]);

					if (function(results[xIndex, yIndex, zIndex]) > minValue) {
						continue;
					}

					minValue = function(results[xIndex, yIndex, zIndex]);
					minPoint = results[xIndex, yIndex, zIndex];
				}
			}
		}

		return minPoint;
	}

	public static bool SecondDerivativesAreNegativeOrZero(Func<Point3, double> function, Point3 point) {

		double valueAtPoint = function(point);

		double valueAtXSingleOffset = function(point.Translate(new() { X = DerivativeStepSize, Y = 0, Z = 0 }));
		double valueAtYSingleOffset = function(point.Translate(new() { X = 0, Y = DerivativeStepSize, Z = 0 }));
		double valueAtZSingleOffset = function(point.Translate(new() { X = 0, Y = 0, Z = DerivativeStepSize }));

		double xFirstPartial1 = (valueAtXSingleOffset - valueAtPoint) / DerivativeStepSize;
		double yFirstPartial1 = (valueAtYSingleOffset - valueAtPoint) / DerivativeStepSize;
		double zFirstPartial1 = (valueAtZSingleOffset - valueAtPoint) / DerivativeStepSize;

		double valueAtXDoubleOffset = function(point.Translate(new() { X = 2 * DerivativeStepSize, Y = 0, Z = 0 }));
		double valueAtYDoubleOffset = function(point.Translate(new() { X = 0, Y = 2 * DerivativeStepSize, Z = 0 }));
		double valueAtZDoubleOffset = function(point.Translate(new() { X = 0, Y = 0, Z = 2 * DerivativeStepSize }));

		double xFirstPartial2 = (valueAtXDoubleOffset - valueAtXSingleOffset) / DerivativeStepSize;
		double yFirstPartial2 = (valueAtYDoubleOffset - valueAtYSingleOffset) / DerivativeStepSize;
		double zFirstPartial2 = (valueAtZDoubleOffset - valueAtZSingleOffset) / DerivativeStepSize;

		double xSecondPartial = (xFirstPartial2 - xFirstPartial1) / DerivativeStepSize;
		double ySecondPartial = (yFirstPartial2 - yFirstPartial1) / DerivativeStepSize;
		double zSecondPartial = (zFirstPartial2 - zFirstPartial1) / DerivativeStepSize;

		return xSecondPartial <= 0 && ySecondPartial <= 0 && zSecondPartial <= 0;
	}

}
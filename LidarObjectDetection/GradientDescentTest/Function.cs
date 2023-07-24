namespace GradientDescentTest; 



public class Function {

	public required Func<double, double, double, double> Math { get; init; }

	public double Evaluate(double x, double y, double z) {
		return Math(x, y, z);
	}

}

public static class GradientDescent {

	public const double OffsetSize = 0.001;

	public const double SetSize = 0.05;

	public static double Gradient(Func<double, double, double, double> function, double x, double y, double z) {

		double valueAtPoint = function(x, y, z);

		double valueAtXOffset = function(x + OffsetSize, y, z);
		double valueAtYOffset = function(x, y + OffsetSize, z);
		double valueAtZOffset = function(x, y, z + OffsetSize);

		double xPartial = (valueAtXOffset - valueAtPoint) / OffsetSize;
		double yPartial = (valueAtYOffset - valueAtPoint) / OffsetSize;
		double zPartial = (valueAtZOffset - valueAtPoint) / OffsetSize;

		throw new NotImplementedException();
	}

}
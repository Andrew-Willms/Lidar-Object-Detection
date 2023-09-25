namespace Utilities; 



public static class Power {

	public static double ToThe(this double baseNumber, double exponent) {
		return Math.Pow(baseNumber, exponent);
	}

	public static int ToThe(this int baseNumber, int exponent) {
		return (int)Math.Pow(baseNumber, exponent);
	}

}
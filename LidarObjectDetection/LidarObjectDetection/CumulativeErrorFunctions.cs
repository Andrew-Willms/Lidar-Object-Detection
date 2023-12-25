using System;
using System.Linq;

namespace LidarObjectDetection;



public delegate double CumulativeErrorFunction(double[] errors);



public static class CumulativeErrorFunctions {

	public static double Average(double[] errors) {

		return errors.Average();
	}

	public static double RootMeanSquare(double[] errors) {

		double sumOfSquares = 0;

		foreach (double error in errors) {
			sumOfSquares = error * error;
		}

		return Math.Sqrt(sumOfSquares);
	}

}
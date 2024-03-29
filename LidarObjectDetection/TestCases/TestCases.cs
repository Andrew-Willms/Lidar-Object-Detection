﻿using LidarObjectDetection;
using LinearAlgebra;
using LinearAlgebra.GradientDescent;

namespace TestCases; 



public static class TestCases {

	public static readonly TestCase TestCase1 = new() {

		ShapeToFind = Shapes.SquareHalfMeter,

		World = new(Shapes.SquareHalfMeter.Rotated(-45).Translated(new(-0.3, 1.5))),

		Lidar = LidarScanners.Lidar21Beams1mWide,
		LidarOffset = new(0, 0),
		LidarRotation = 0,

		DetectionParameters = new() {

			StartingPointCount = 10,

			SearchRegion = new() { CornerA = new(-0.3, 0.75, 0), CornerB = new(0.3, 5, 90) },
			
			//StartingPointDistributor = (count, region) => StartingPointDistributors.EvenCubicGridDistributor(150, new() { CornerA = new(-0.5, 0.5, 0), CornerB = new(0.5, 4, 90) }),
			//StartingPointDistributor = (count, region) => StartingPointDistributors.RectangularDistributor(0, new() { CornerA = new(-0.25, 2.5, 0), CornerB = new(0.25, 3.5, 90) }, 2, 4, 30),
			//StartingPointDistributor = (count, region) => StartingPointDistributors.RectangularDistributor(0, new() { CornerA = new(-0.5, 2, 10), CornerB = new(-0.5, 2, 45) }, 1, 1, 20),
			StartingPointDistributor = (count, region) => StartingPointDistributors.DiscretePointDistributor(count, region, new Point3(0.3, 3, 35)),
			//StartingPointDistributor = (count, region) => StartingPointDistributors.DiscretePointDistributor(count, region, new Point3(0.6, 4, 20)),
			//StartingPointDistributor = (count, region) => StartingPointDistributors.DiscretePointDistributor(count, region, new Point3(0.05, 3.5, 70)),
			//StartingPointDistributor = (count, region) => StartingPointDistributors.DiscretePointDistributor(count, region, new Point3(0, 2.92, 34-45)),

			GradientDescentParameters = new() {

				InitialGradientApproximation = (function, point) => 
					GradientApproximations.InitialConstantDifference(function, point, new(0.00000001, 0.00000001, 0.00000001)), // had 0.1

				GradientApproximation = (function, point, previousLocation) => 
					GradientApproximations.ConstantDifference(function, point, previousLocation, new(0.00000001, 0.00000001, 0.00000001)),

				InitialStepCalculator = gradient => InitialStepCalculators.ScaleGradient(gradient, 0.05),

				//StepCalculator = (previousStep, previousGradient, gradient) => StepCalculators.ScaleGradientByPart(previousStep, previousGradient, gradient, new(0.000, 0.000, 3000)),
				StepCalculator = (previousStep, previousGradient, gradient) => 
					StepCalculators.LimitedScaleGradientByPart(previousStep, previousGradient, gradient, new(0.5, 0.5, 1000), new(0.01, 0.01, 1.6)),
					//StepCalculators.LimitedScaleGradientByPart(previousStep, previousGradient, gradient, new(0.5, 0.5, 0.5), new(0.01, 0.01, 1.6)),
					//StepCalculators.LimitedScaleGradientByPart(previousStep, previousGradient, gradient, new(0.001, 0.001, 100), new(0.01, 0.01, 3.6)),

				ConvergenceDeciderFactory = () => new ConsecutiveSmallGradientAndPointChange {
					MaxAllowedIterations = 1000,
					ConsecutiveSmallIterationsRequired = 20,
					//GradientThreshold = 0.001,
					PointChangeThreshold = new(0.00001, 0.00001, 0.00036),
					ErrorChangeThreshold = 0.00001
				},

				DivergenceDeciderFactory = () => new IterationDivergenceDecider { MaxAllowedIterations = 1000 }
			},

			LeastDistanceCalculatorCreator = otherPoints => DumbLeastDistanceCalculator.Create(otherPoints),

			CumulativeErrorFunction = CumulativeErrorFunctions.Average,
			//CumulativeErrorFunction = CumulativeErrorFunctions.RootMeanSquare,

			PreviousPosition = Point3.Origin,

			RobotVelocity = Vector3.Zero
		}
	};
}
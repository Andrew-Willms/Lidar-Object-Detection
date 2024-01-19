using LidarObjectDetection;
using LinearAlgebra;
using LinearAlgebra.GradientDescent;

namespace TestCases; 



public static class TestCases {

	public static readonly TestCase TestCase1 = new() {

		ShapeToFind = new(new Point2(-0.25, -0.25), new(0.25, -0.25), new(0.25, 0.25), new(-0.25, 0.25)),

		World = new(
			new Polygon(new Point2(-0.25, -0.25), new(0.25, -0.25), new(0.25, 0.25), new(-0.25, 0.25))
				.Rotated(34)
				.Translated(new(0, 3))),

		Lidar = new() {
			Beams = new[] {
				new LineSegment(new(-1, 0), new Point2(-1, 5)),
				new LineSegment(new(-0.9, 0), new Point2(-0.9, 5)),
				new LineSegment(new(-0.8, 0), new Point2(-0.8, 5)),
				new LineSegment(new(-0.7, 0), new Point2(-0.7, 5)),
				new LineSegment(new(-0.6, 0), new Point2(-0.6, 5)),
				new LineSegment(new(-0.5, 0), new Point2(-0.5, 5)),
				new LineSegment(new(-0.4, 0), new Point2(-0.4, 5)),
				new LineSegment(new(-0.3, 0), new Point2(-0.3, 5)),
				new LineSegment(new(-0.2, 0), new Point2(-0.2, 5)),
				new LineSegment(new(-0.1, 0), new Point2(-0.1, 5)),
				new LineSegment(new(0, 0), new Point2(0, 5)),
				new LineSegment(new(0.1, 0), new Point2(0.1, 5)),
				new LineSegment(new(0.2, 0), new Point2(0.2, 5)),
				new LineSegment(new(0.3, 0), new Point2(0.3, 5)),
				new LineSegment(new(0.4, 0), new Point2(0.4, 5)),
				new LineSegment(new(0.5, 0), new Point2(0.5, 5)),
				new LineSegment(new(0.6, 0), new Point2(0.6, 5)),
				new LineSegment(new(0.7, 0), new Point2(0.7, 5)),
				new LineSegment(new(0.8, 0), new Point2(0.8, 5)),
				new LineSegment(new(0.9, 0), new Point2(0.9, 5)),
				new LineSegment(new(1, 0), new Point2(1, 5)),
			}
		},

		LidarOffset = new(0, 0),

		LidarRotation = 0,

		DetectionParameters = new() {

			StartingPointCount = 10,

			SearchRegion = new() { CornerA = new(-0.3, 0.75, 0), CornerB = new(0.3, 5, 90) },

			//StartingPointDistributor = (count, region) => StartingPointDistributors.DiscretePointDistributor(count, region, new Point3(0.2, 2, 20)),
			StartingPointDistributor = (count, region) => StartingPointDistributors.DiscretePointDistributor(count, region, new Point3(0.6, 4, 20)),

			GradientDescentParameters = new() {

				InitialGradientApproximation = (function, point) => GradientApproximations.InitialConstantDifference(function, point, new(0.1, 0.1, 0.1)),

				GradientApproximation = (function, point, previousLocation) => GradientApproximations.ConstantDifference(function, point, previousLocation, new(0.1, 0.1, 0.1)),

				InitialStepCalculator = gradient => InitialStepCalculators.ScaleGradient(gradient, 0.05),

				StepCalculator = (previousStep, previousGradient, gradient) => StepCalculators.ScaleGradientByPart(previousStep, previousGradient, gradient, new(0.1, 0.1, 15)),

				ConvergenceDecider = new ConsecutiveSmallGradientAndPointChange {
					MaxAllowedIterations = 1000,
					ConsecutiveSmallIterationsRequired = 5,
					GradientThreshold = 0.001,
					PointChangeThreshold = new(0.001, 0.001, 0.01)
				},

				DivergenceDecider = new IterationDivergenceDecider { MaxAllowedIterations = 1000 }
			},

			LeastDistanceCalculatorCreator = otherPoints => DumbLeastDistanceCalculator.Create(otherPoints),

			CumulativeErrorFunction = CumulativeErrorFunctions.Average,

			PreviousPosition = Point3.Origin,

			RobotVelocity = Vector3.Zero
		}
	};
}
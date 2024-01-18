using LidarObjectDetection;
using LinearAlgebra;
using LinearAlgebra.GradientDescent;

namespace TestCases; 



public static class TestCases {

	public static readonly TestCase TestCase1 = new() {

		ShapeToFind = new(new Point2(-0.5, -0.5), new(0.5, -0.5), new(0.5, 0.5), new(-0.5, 0.5)),

		World = new(
			new Polygon(new Point2(-0.5, -0.5), new(0.5, -0.5), new(0.5, 0.5), new(-0.5, 0.5))
				.Rotated(0)
				.Translated(new(3, 2))),

		Lidar = new() {
			Beams = new[] {
				new LineSegment(new(-1, 0), new Point2(-1, 10)),
				new LineSegment(new(-0.9, 0), new Point2(-0.9, 10)),
				new LineSegment(new(-0.8, 0), new Point2(-0.8, 10)),
				new LineSegment(new(-0.7, 0), new Point2(-0.7, 10)),
				new LineSegment(new(-0.6, 0), new Point2(-0.6, 10)),
				new LineSegment(new(-0.5, 0), new Point2(-0.5, 10)),
				new LineSegment(new(-0.4, 0), new Point2(-0.4, 10)),
				new LineSegment(new(-0.3, 0), new Point2(-0.3, 10)),
				new LineSegment(new(-0.2, 0), new Point2(-0.2, 10)),
				new LineSegment(new(-0.1, 0), new Point2(-0.1, 10)),
				new LineSegment(new(0, 0), new Point2(0, 10)),
				new LineSegment(new(0.1, 0), new Point2(0.1, 10)),
				new LineSegment(new(0.2, 0), new Point2(0.2, 10)),
				new LineSegment(new(0.3, 0), new Point2(0.3, 10)),
				new LineSegment(new(0.4, 0), new Point2(0.4, 10)),
				new LineSegment(new(0.5, 0), new Point2(0.5, 10)),
				new LineSegment(new(0.6, 0), new Point2(0.6, 10)),
				new LineSegment(new(0.7, 0), new Point2(0.7, 10)),
				new LineSegment(new(0.8, 0), new Point2(0.8, 10)),
				new LineSegment(new(0.9, 0), new Point2(0.9, 10)),
				new LineSegment(new(1, 0), new Point2(1, 10)),
			}
		},

		LidarOffset = new(1, 0),

		LidarRotation = -45,

		DetectionParameters = new() {

			StartingPointCount = 10,

			SearchRegion = new() { CornerA = new(-0.3, 0.75, 0), CornerB = new(0.3, 5, 90) },

			StartingPointDistributor = StartingPointDistributors.EvenCubicGridDistributor,

			GradientDescentParameters = new() {

				InitialGradientApproximation = (function, point) => GradientApproximations.InitialConstantDifference(function, point, new(0.1, 0.1, 0.1)),

				GradientApproximation = (function, point, previousLocation) => GradientApproximations.ConstantDifference(function, point, previousLocation, new(0.1, 0.1, 0.1)),

				InitialStepCalculator = gradient => InitialStepCalculators.ConstantStep(gradient, 0.001),

				StepCalculator = (previousStep, previousGradient, gradient) => StepCalculators.ConstantStep(previousStep, previousGradient, gradient, 0.08),

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
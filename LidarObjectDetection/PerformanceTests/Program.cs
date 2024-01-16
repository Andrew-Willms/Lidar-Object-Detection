using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using LidarObjectDetection;
using LinearAlgebra;
using LinearAlgebra.GradientDescent;



Point2[] squarePoints = { new(0, 0), new(1, 0), new(1, 1), new(0, 1) };
Polygon square = new(squarePoints.ToImmutableArray());

World world = new();
world.AddObject(square.Translated(new(1.5, 1.5)));

LidarScanner lidarScanner = new() {
	Beams = new[] { new LineSegment(new(0, 0), new Point2(0, 10)) }
};

ImmutableArray<Point2> lidarPoints = lidarScanner.ScanInLidarCoords(world, new(2, 0), 0);



DetectionParameters detectionParameters = new() {

	StartingPointCount = 20,

	SearchRegion = new() { CornerA = new(-5, 0, 0), CornerB = new(5, 5, 90) },

	StartingPointDistributor = StartingPointDistributors.EvenCubicGridDistributor,

	GradientDescentParameters = new() {

		InitialGradientApproximation = (function, point) => GradientApproximations.InitialConstantDifference(function, point, new(0.1, 0.1, 0.1)),

		GradientApproximation = (function, point, previousLocation) => GradientApproximations.ConstantDifference(function, point, previousLocation, new(0.1, 0.1, 0.1)),
		
		InitialStepCalculator = gradient => InitialStepCalculators.ConstantStep(gradient, 0.001),
		
		StepCalculator = (previousStep, previousGradient, gradient) => StepCalculators.ConstantStep(previousStep, previousGradient, gradient, 0.001),
		
		ConvergenceDecider = new ConsecutiveSmallGradientAndPointChange {
			ConsecutiveSmallIterationsRequired = 5,
			GradientThreshold = 0.00001,
			PointChangeThreshold = new(0.001, 0.001, 0.01)
		},
		
		DivergenceDecider = new IterationDivergenceDecider{ MaxAllowedIterations = 1000 }
	},

	LeastDistanceCalculatorCreator = otherPoints => DumbLeastDistanceCalculator.Create(otherPoints),

	CumulativeErrorFunction = CumulativeErrorFunctions.Average,

	PreviousPosition = Point3.Origin,

	RobotVelocity = Vector3.Zero
};

(Point3? position, List<GradientDescentData> data) = Detection.Detect(lidarPoints, square, lidarScanner, detectionParameters);

Console.WriteLine(position);
Console.WriteLine(data);
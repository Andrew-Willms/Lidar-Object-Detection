using LinearAlgebra;
using System.Diagnostics;
using Utilities;

namespace LidarObjectDetection;



public class LidarArray {

	public int SensorCount { get; init; } = 15;

	public Vector2 SensorSpacing => new Vector2(LeftCorner, RightCorner) / (SensorCount - 1);

	public double LidarPercentErrorStandardDeviation { get; init; } = 1;

	// todo make sure this is non zero
	public double LidarRange { get; init; } = 5;

	public required Point2 LeftCorner { get; init; }

	public required Point2 RightCorner { get; init; }

	public IEnumerable<Point2> SensorPositions {
		get {

			List<Point2> points = new();

			for (int i = 0; i < SensorCount; i++) {

				points.Add(LeftCorner.Translate(SensorSpacing * i));
			}

			return points.ToArray();
		}
	}

	public IEnumerable<LineSegment> LidarBeams {
		get {

			Vector2 beamDirection = new Vector2(LeftCorner, RightCorner).GetCounterClockwisePerpendicularVector();
			Vector2 beamVector = beamDirection.GetUnitVector() * LidarRange;

			return SensorPositions.Select(x => LineSegment.Create(x, beamVector) ?? throw new UnreachableException()).ToArray();
		}
	}

	public Point2[] SimulateLidarReading(Polygon @object) {

		IEnumerable<(LineSegmentIntersection[] intersection, Point2 sensorPosition)> intersectionsAndSensorPositions =
			LidarBeams.Select(x => @object.Intersection(x).AsT0).Zip(SensorPositions);

		List<Point2> lidarPoints = new();

		foreach ((LineSegmentIntersection[] intersection, Point2 sensorPosition) in intersectionsAndSensorPositions) {

			List<Point2> possibleLidarPoints = new();

			foreach (LineSegmentIntersection lineIntersection in intersection) {

				lineIntersection.Match<object>(
					point => {
						possibleLidarPoints.Add(point);
						return null!;
					},
					lineSegment => {
						possibleLidarPoints.Add(lineSegment.Start);
						possibleLidarPoints.Add(lineSegment.End);
						return null!;
					});
			}

			lidarPoints.Add(possibleLidarPoints.SelectMin(x => new Vector2(x, sensorPosition).Magnitude));
		}

		return lidarPoints.ToArray();
	}

}
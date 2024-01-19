using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using LinearAlgebra;
using LinearAlgebra.GradientDescent;
using Microsoft.Maui.Graphics;
using TestCases;
using Polygon = LinearAlgebra.Polygon;

namespace Gui;



public static class UiDetails {

	public static readonly Color FieldBackgroundColor = Colors.Black;

	public static readonly Color LidarScannerColor = Colors.Lime;
	public const float LidarScannerLineThickness = 2;

	public static readonly Color WorldObjectColor = Colors.Aqua;
	public const float WorldObjectLineThickness = 2;

	public static readonly Color LidarDataColor = Colors.Green;
	public const float LidarDataRadius = 3;

	public static readonly Color GuessPositionColor = Colors.Blue;
	public const float GuessPositionLineThickness = 1;

	public static readonly Color FinalPositionColor = Colors.Magenta;
	public const float FinalPositionLineThickness = 1;

}




public class GraphicsManager : IDrawable {

	private (Point3? position, List<GradientDescentData> data)? _TestCaseResults;
	private (Point3? position, List<GradientDescentData> data) TestCaseResults {
		get {
			_TestCaseResults ??= TestCase.Execute();
			return ((Point3? position, List<GradientDescentData> data)) _TestCaseResults;
		}
	}

	public required TestCase TestCase { get; init; }

	public required Point2 FieldTopLeftCorner { get; init; }
	public required Point2 FieldBottomRightCorner { get; init; }

	public required bool ShowLidarBeams { get; init; }
	public required bool ShowObjectsOnField { get; init; }
	public required bool ShowRealLidarPoints { get; init; }
	public required bool ShowTheoreticalLidarPoints { get; init; }

	public required bool ShowShapeToFind { get; init; }
	public required bool ShowStartingPoints { get; init; }
	public required bool ShowStartingBoxBounds { get; init; }
	//public required bool ShowSearchBounds { get; init; }

	public required bool ShowFinalPosition { get; init; }

	public required bool ShowAllRoutes { get; init; }
	public required ImmutableArray<int> RoutesToShow { get; init; }

	public required bool ShowGuessPositions { get; init; }
	public required bool ShowGradient { get; init; }
	public required bool ShowStep { get; init; }



	public void Draw(ICanvas canvas, RectF dirtyRect) {

		FieldCanvas fieldCanvas = new(FieldTopLeftCorner, FieldBottomRightCorner, canvas, dirtyRect);

		DrawBackground();

		if (ShowLidarBeams) {
			DrawLidarBeams(fieldCanvas);
		}

		if (ShowObjectsOnField) {
			DrawObjectsOnField(fieldCanvas);
		}

		if (ShowRealLidarPoints) {

		}

		DrawRoutes(fieldCanvas);

		if (ShowFinalPosition) {
			DrawFinalPosition(fieldCanvas);
		}
	}

	private void DrawBackground() {



	}

	private void DrawLidarBeams(FieldCanvas fieldCanvas) {

		IEnumerable<LineSegment> lidarBeamsInWorldSpace = TestCase.Lidar.Beams
			.Select(beam => beam
				.RotateAround(TestCase.LidarRotation, Point2.Origin)
				.Translate(TestCase.LidarOffset));

		foreach (LineSegment lineSegment in lidarBeamsInWorldSpace) {

			fieldCanvas.DrawLine(lineSegment, UiDetails.LidarScannerColor, UiDetails.LidarScannerLineThickness);
		}
	}

	private void DrawObjectsOnField(FieldCanvas fieldCanvas) {

		foreach (Polygon worldObject in TestCase.World.Objects) {
			
			fieldCanvas.DrawPolygon(worldObject, UiDetails.WorldObjectColor, UiDetails.WorldObjectLineThickness);
		}
	}

	private void DrawRealLidarPoints(FieldCanvas fieldCanvas) {

		IEnumerable<Point2> lidarBeamsInWorldSpace = TestCase.GetLidarData()
			.Select(beam => beam
				.Rotated(TestCase.LidarRotation, Point2.Origin)
				.Translated(TestCase.LidarOffset));

		foreach (Point2 point in lidarBeamsInWorldSpace) {
			
			fieldCanvas.DrawPoint(point, UiDetails.LidarDataColor, UiDetails.LidarDataRadius);
		}
	}

	private void DrawRoutes(FieldCanvas fieldCanvas) {

		List<GradientDescentData> routesToDraw = ShowAllRoutes
			? TestCaseResults.data
			: TestCaseResults.data.Where((_, index) => RoutesToShow.Contains(index)).ToList();

		foreach (GradientDescentData gradientDescentData in routesToDraw) {

			for (int index = 0; index < gradientDescentData.Points.Count; index++) {

				if (ShowGuessPositions) {

					Polygon shape = TestCase.ShapeToFind
						.Rotated(gradientDescentData.Points[index].Z)
						.Translated(new(gradientDescentData.Points[index].X, gradientDescentData.Points[index].Y))
						.Rotated(TestCase.LidarRotation)
						.Translated(TestCase.LidarOffset);

					fieldCanvas.DrawPolygon(shape, UiDetails.GuessPositionColor, UiDetails.GuessPositionLineThickness);
				}

				if (ShowGradient) {
					// todo
				}

				if (ShowStep) {
					// todo
				}
			}
		}
	}

	private void DrawFinalPosition(FieldCanvas fieldCanvas) {

		if (TestCaseResults.position is null) {
			return;
		}

		Point3 finalPosition = (Point3)TestCaseResults.position!;

		Polygon finalGuess = TestCase.ShapeToFind
			.Rotated(finalPosition.Z)
			.Translated(new(finalPosition.X, finalPosition.Y));

		fieldCanvas.DrawPolygon(finalGuess, UiDetails.FinalPositionColor, UiDetails.FinalPositionLineThickness);
	}

}
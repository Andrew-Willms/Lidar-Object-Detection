﻿using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using LinearAlgebra;
using LinearAlgebra.GradientDescent;
using Microsoft.Maui.Graphics;
using TestCases;

namespace Gui;



public static class UiDetails {

	public static readonly Color LidarScannerColor = Colors.Lime;
	public const float LidarScannerLineThickness = 2;

	public static readonly Color LidarDataColor = Colors.Green;
	public const float LidarDataRadius = 3;

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

	public required bool ShowRealLidarPoints { get; init; }

	public required bool ShowTheoreticalLidarPoints { get; init; }

	public required bool ShowShapeToFind { get; init; }

	public required bool ShowStartingPoints { get; init; }

	public required bool ShowStartingBoxBounds { get; init; }

	//public required bool ShowSearchBounds { get; init; }

	public required bool ShowFinalPosition { get; init; }

	public required bool ShowAllRoutes { get; init; }

	public required ImmutableArray<int> RoutesToShow { get; init; }



	public void Draw(ICanvas canvas, RectF dirtyRect) {

		Vector2 fieldSpan = new(FieldTopLeftCorner, FieldBottomRightCorner);
		FieldCanvas fieldCanvas = new((float)fieldSpan.X, (float)fieldSpan.Y, canvas, dirtyRect);

		if (ShowLidarBeams) {
			DrawLidarBeams(fieldCanvas);
		}

		if (ShowRealLidarPoints) {

		}

		List<GradientDescentData> routesToDraw = ShowAllRoutes
			? TestCaseResults.data 
			: TestCaseResults.data.Where((_, index) => RoutesToShow.Contains(index)).ToList();

		DrawRoutes(fieldCanvas, routesToDraw);


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

	private void DrawRealLidarPoints(FieldCanvas fieldCanvas) {

		IEnumerable<Point2> lidarBeamsInWorldSpace = TestCase.GetLidarData()
			.Select(beam => beam
				.Rotated(TestCase.LidarRotation, Point2.Origin)
				.Translated(TestCase.LidarOffset));

		foreach (Point2 point in lidarBeamsInWorldSpace) {
			
			fieldCanvas.DrawPoint(point, UiDetails.LidarDataColor, UiDetails.LidarDataRadius);
		}
	}

	private void DrawRoutes(FieldCanvas fieldCanvas, List<GradientDescentData> routes) {



	}

}
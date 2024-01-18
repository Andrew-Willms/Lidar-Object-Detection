using System;
using System.Diagnostics;
using System.Linq;
using LinearAlgebra;
using Microsoft.Maui.Graphics;

namespace Gui; 



public class FieldCanvas {

	private readonly Point2 TopLeftCorner;
	private readonly Point2 BottomRightCorner;

	private readonly LineSegment TopBorder;
	private readonly LineSegment BottomBorder;
	private readonly LineSegment LeftBorder;
	private readonly LineSegment RightBorder;

	private readonly double XOffset;
	private readonly double YOffset;
	private readonly double DrawScalingFactor;

	private ICanvas BackingCanvas { get; }
	private RectF CanvasDimensions { get; }



	public FieldCanvas(
		Point2 topLeftCorner,
		Point2 bottomRightCorner,
		ICanvas backingCanvas,
		RectF canvasDimensions) {
		TopLeftCorner = topLeftCorner;
		BottomRightCorner = bottomRightCorner;
		Point2 topRightCorner = new(bottomRightCorner.X, topLeftCorner.Y);
		Point2 bottomLeftCorner = new(topLeftCorner.X, bottomRightCorner.Y);

		TopBorder = new(topLeftCorner, topRightCorner);
		BottomBorder = new(bottomLeftCorner, bottomRightCorner);
		LeftBorder = new(topLeftCorner, bottomLeftCorner);
		RightBorder = new(topRightCorner, bottomRightCorner);

		Vector2 fieldSpan = new(TopLeftCorner, BottomRightCorner);
		float width = Math.Abs((float)fieldSpan.X);
		float height = Math.Abs((float)fieldSpan.Y);

		BackingCanvas = backingCanvas;
		CanvasDimensions = canvasDimensions;

		double canvasRatio = CanvasDimensions.Width / CanvasDimensions.Height;
		double fieldRatio = width / height;

		bool canvasHasExtraWidth = canvasRatio > fieldRatio;

		if (canvasHasExtraWidth) {
			DrawScalingFactor = CanvasDimensions.Height / height;
			XOffset = (CanvasDimensions.Width - width * DrawScalingFactor);
			YOffset = 0;

		} else {
			DrawScalingFactor = CanvasDimensions.Width / width;
			XOffset = 0;
			YOffset = (CanvasDimensions.Height - height * DrawScalingFactor);
		}
	}



	public void DrawPoint(Point2 point, Color color, float radius) {

		DrawPoint((float)point.X, (float)point.Y, color, radius);
	}

	public void DrawPoint(float x, float y, Color color, float radius) {

		if (x < TopLeftCorner.X || x > BottomRightCorner.X ||
		    y > TopLeftCorner.Y || y < BottomRightCorner.Y) {

			Debug.WriteLine($"Point ({x}, {y}) skipped because it was out of bounds of the field.");
			return;
		}

		BackingCanvas.FillColor = color;
		BackingCanvas.FillCircle(ToCanvasXPosition(x), ToCanvasYPosition(y), radius);
	}

	public void DrawLine(LineSegment line, Color lineColor, float lineThickness) {

		bool startingPointOutOfBounds = line.Start.X < TopLeftCorner.X || line.Start.X > BottomRightCorner.X ||
		                             line.Start.Y > TopLeftCorner.Y || line.Start.Y < BottomRightCorner.Y;

		bool endingPointOutOfBounds = line.End.X < TopLeftCorner.X || line.End.X > BottomRightCorner.X ||
		                           line.End.Y > TopLeftCorner.Y || line.End.Y < BottomRightCorner.Y;

		LineSegmentIntersection[] intersections = new[] {
			line.Intersection(TopBorder),
			line.Intersection(BottomBorder),
			line.Intersection(LeftBorder),
			line.Intersection(RightBorder)
		}.Where(intersection => intersection is not null).ToArray()!;

		// if any of the intersections are line segments
		if (intersections.Any(intersection => intersection.IsT1)) {
			line = intersections.First(intersection => intersection.IsT1).AsT1;

		} else if (startingPointOutOfBounds && endingPointOutOfBounds && intersections.Length == 2) {
			line = new(intersections.ElementAt(0).AsT0, intersections.ElementAt(1).AsT0);

		} else if (startingPointOutOfBounds && endingPointOutOfBounds && intersections.Length == 0) {
			return;

		} else if (startingPointOutOfBounds && intersections.Length == 1) {
			line = new(intersections.ElementAt(0).AsT0, line.End);

		} else if (endingPointOutOfBounds && intersections.Length == 1) {
			line = new(line.Start, intersections.ElementAt(0).AsT0);
		}

		BackingCanvas.StrokeColor = lineColor;
		BackingCanvas.StrokeSize = lineThickness;

		BackingCanvas.DrawLine(
			ToCanvasXPosition(line.Start.X),
			ToCanvasYPosition(line.Start.Y),
			ToCanvasXPosition(line.End.X),
			ToCanvasYPosition(line.End.Y));
	}

	public void DrawLine(double xStart, double yStart, double xEnd, double yEnd, Color lineColor, float lineThickness) {

		DrawLine(new(new(xStart, yStart), new Point2(xEnd, yEnd)), lineColor, lineThickness);
	}

	public void DrawPolygon(Polygon polygon, Color lineColor, float lineThickness) {

		foreach (LineSegment edge in polygon.Edges) {
			DrawLine(edge.Start.X, edge.Start.Y, edge.End.X, edge.End.Y, lineColor, lineThickness);
		}
	}



	private float ToCanvasXPosition(double xPosition) {

		xPosition -= TopLeftCorner.X;

		return (float)(XOffset + xPosition * DrawScalingFactor);
	}

	private float ToCanvasYPosition(double yPosition) {

		yPosition -= BottomRightCorner.Y;

		return CanvasDimensions.Height - (float)(YOffset + yPosition * DrawScalingFactor);
	}

}
﻿using System;
using LinearAlgebra;
using Microsoft.Maui.Graphics;

namespace Gui; 



public class FieldCanvas {

	private readonly float FieldWidth;
	private readonly float FieldHeight;

	private readonly Point2 FieldTopLeftCorner;
	private readonly Point2 FieldBottomRightCorner;

	private readonly double XOffset;
	private readonly double YOffset;
	private readonly double DrawScalingFactor;

	private ICanvas BackingCanvas { get; }
	private RectF CanvasDimensions { get; }



	public FieldCanvas(
		Point2 fieldTopLeftCorner,
		Point2 fieldBottomRightCorner,
		ICanvas backingCanvas,
		RectF canvasDimensions) {

		FieldTopLeftCorner = fieldTopLeftCorner;
		FieldBottomRightCorner = fieldBottomRightCorner;
		Vector2 fieldSpan = new(FieldTopLeftCorner, FieldBottomRightCorner);

		FieldWidth = Math.Abs((float)fieldSpan.X);
		FieldHeight = Math.Abs((float)fieldSpan.Y);

		BackingCanvas = backingCanvas;
		CanvasDimensions = canvasDimensions;

		double canvasRatio = CanvasDimensions.Width / CanvasDimensions.Height;
		double fieldRatio = FieldWidth / FieldHeight;

		bool canvasHasExtraWidth = canvasRatio > fieldRatio;

		if (canvasHasExtraWidth) {
			DrawScalingFactor = CanvasDimensions.Height / FieldHeight;
			XOffset = (CanvasDimensions.Width - FieldWidth * DrawScalingFactor);
			YOffset = 0;

		} else {
			DrawScalingFactor = CanvasDimensions.Width / FieldWidth;
			XOffset = 0;
			YOffset = (CanvasDimensions.Height - FieldHeight * DrawScalingFactor);
		}
	}



	public void DrawPoint(Point2 point, Color color, float radius) {

		DrawPoint((float)point.X, (float)point.Y, color, radius);
	}

	public void DrawPoint(float x, float y, Color color, float radius) {

		BackingCanvas.FillColor = color;
		BackingCanvas.FillCircle(ToCanvasXPosition(x), ToCanvasYPosition(y), radius);
	}

	public void DrawLine(LineSegment line, Color lineColor, float lineThickness) {

		DrawLine(line.Start.X, line.Start.Y, line.End.X, line.End.Y, lineColor, lineThickness);
	}

	public void DrawLine(double xStart, double yStart, double xEnd, double yEnd, Color lineColor, float lineThickness) {

		//TODO make it cut off the lines when they extend beyond the edge of the field.

		if (xStart < FieldTopLeftCorner.X || xStart > FieldBottomRightCorner.X) {
			throw new ArgumentException($"{nameof(xStart)} must be between {0} and {FieldWidth}, was {xStart}.");
		}

		if (yStart < FieldBottomRightCorner.Y || yStart > FieldTopLeftCorner.Y) {
			throw new ArgumentException($"{nameof(yStart)} must be between {0} and {FieldHeight}, was {yStart}.");
		}

		if (xEnd < FieldTopLeftCorner.X || xEnd > FieldBottomRightCorner.X) {
			throw new ArgumentException($"{nameof(xEnd)} must be between {0} and {FieldWidth}, was {xEnd}.");
		}

		if (yEnd < FieldBottomRightCorner.Y || yEnd > FieldTopLeftCorner.Y) {
			throw new ArgumentException($"{nameof(yEnd)} must be between {0} and {FieldHeight}, was {yEnd}.");
		}

		BackingCanvas.StrokeColor = lineColor;
		BackingCanvas.StrokeSize = lineThickness;

		float test1 = ToCanvasXPosition(xStart);
		float test2 = ToCanvasYPosition(yStart);
		float test3 = ToCanvasXPosition(xEnd);
		float test4 = ToCanvasYPosition(yEnd);

		BackingCanvas.DrawLine(ToCanvasXPosition(xStart), ToCanvasYPosition(yStart), ToCanvasXPosition(xEnd), ToCanvasYPosition(yEnd));
	}

	public void DrawPolygon(Polygon polygon, Color lineColor, float lineThickness) {

		foreach (LineSegment edge in polygon.Edges) {
			DrawLine(edge.Start.X, edge.Start.Y, edge.End.X, edge.End.Y, lineColor, lineThickness);
		}
	}



	private float ToCanvasXPosition(double xPosition) {

		return (float)(XOffset + xPosition * DrawScalingFactor);
	}

	private float ToCanvasYPosition(double yPosition) {

		return CanvasDimensions.Height - (float)(YOffset + yPosition * DrawScalingFactor);
	}

}
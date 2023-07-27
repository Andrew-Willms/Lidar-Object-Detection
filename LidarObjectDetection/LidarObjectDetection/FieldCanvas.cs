using LinearAlgebra;

namespace LidarObjectDetection;



public class FieldCanvas {

	private readonly float FieldWidth;
	private readonly float FieldHeight;

	private ICanvas BackingCanvas { get; }
	private RectF CanvasDimensions { get; }
	private (double Width, double Height) FreeCanvasDimensions { get; }

	private readonly LidarArray LidarArray;

	private readonly double XOffset;
	private readonly double YOffset;
	private readonly double DrawScalingFactor;

	private static readonly Color BackgroundColor = Colors.Black;
	private static readonly Color BorderColor = Colors.White;
	private const float BorderThickness = 5;
	private static readonly Color LidarArrayColor = Colors.Lime;
	private const float LidarArrayLineThickness = 2;
	private const float LidarArrayHeight = 5;

	public FieldCanvas(
		float fieldWidth,
		float fieldHeight,
		ICanvas backingCanvas,
		RectF canvasDimensions,
		LidarArray lidarArray) {

		FieldWidth = fieldWidth;
		FieldHeight = fieldHeight;

		BackingCanvas = backingCanvas;
		CanvasDimensions = canvasDimensions;
		FreeCanvasDimensions = (canvasDimensions.Width - 2 * BorderThickness, canvasDimensions.Height - 2 * BorderThickness - LidarArrayHeight);

		LidarArray = lidarArray;

		double canvasRatio = FreeCanvasDimensions.Width / FreeCanvasDimensions.Height;
		double fieldRatio = fieldWidth / fieldHeight;

		bool canvasHasExtraWidth = canvasRatio > fieldRatio;

		if (canvasHasExtraWidth) {
			DrawScalingFactor = FreeCanvasDimensions.Height / fieldHeight;
			XOffset = (FreeCanvasDimensions.Width - fieldWidth * DrawScalingFactor) / 2 + BorderThickness;
			YOffset = BorderThickness;

		} else {
			DrawScalingFactor = FreeCanvasDimensions.Width / fieldWidth;
			XOffset = BorderThickness;
			YOffset = (FreeCanvasDimensions.Height - fieldHeight * DrawScalingFactor) / 2 + BorderThickness;
		}

		DrawBorderAndBackground();

		DrawLidarArray();
	}



	public void DrawPoint(float x, float y, Color color, float radius) {

		BackingCanvas.FillColor = color;
		BackingCanvas.FillCircle(ToCanvasXPosition(x), ToCanvasYPosition(y), radius);
	}

	public void DrawLine(double xStart, double yStart, double xEnd, double yEnd, Color lineColor, float lineThickness) {

		//TODO make it cut off the lines when they extend beyond the edge of the field.

		if (xStart < 0 || xStart > FieldWidth) {
			throw new ArgumentException($"{nameof(xStart)} must be between {0} and {FieldWidth}, was {xStart}.");
		}

		if (yStart < 0 || xStart > FieldHeight) {
			throw new ArgumentException($"{nameof(yStart)} must be between {0} and {FieldHeight}, was {yStart}.");
		}

		if (xEnd < 0 || xEnd > FieldWidth) {
			throw new ArgumentException($"{nameof(xEnd)} must be between {0} and {FieldWidth}, was {xEnd}.");
		}

		if (yEnd < 0 || yEnd > FieldHeight) {
			throw new ArgumentException($"{nameof(yEnd)} must be between {0} and {FieldHeight}, was {yEnd}.");
		}

		BackingCanvas.StrokeColor = lineColor;
		BackingCanvas.StrokeSize = lineThickness;
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

		return CanvasDimensions.Height - (float)(YOffset + LidarArrayHeight + yPosition * DrawScalingFactor);
	}

	private void DrawBorderAndBackground() {

		float fieldLeftX = ToCanvasXPosition(0);
		float fieldWidth = ToCanvasXPosition(FieldWidth) - ToCanvasXPosition(0);
		float fieldTopY = ToCanvasYPosition(FieldHeight);
		float fieldHeight = ToCanvasYPosition(0) - ToCanvasYPosition(FieldHeight) + LidarArrayHeight;

		float borderLeftX = fieldLeftX - BorderThickness;
		float borderWidth = fieldWidth + 2 * BorderThickness;
		float borderTopY = fieldTopY - BorderThickness;
		float borderHeight = fieldHeight + 2 * BorderThickness;

		BackingCanvas.FillColor = BorderColor;
		BackingCanvas.FillRectangle(borderLeftX, borderTopY, borderWidth, borderHeight);

		BackingCanvas.FillColor = BackgroundColor;
		BackingCanvas.FillRectangle(fieldLeftX, fieldTopY, fieldWidth, fieldHeight);
	}

	private void DrawLidarArray() {

		double lidarArrayStartFieldX = LidarArray.LeftCorner.X;
		float lidarArrayEndX = ToCanvasXPosition(LidarArray.RightCorner.X);

		float lidarArrayTopY = ToCanvasYPosition(0);
		float lidarArrayMiddleY = ToCanvasYPosition(0) + LidarArrayHeight / 2;
		float lidarArrayBottomY = ToCanvasYPosition(0) + LidarArrayHeight;

		BackingCanvas.StrokeColor = LidarArrayColor;
		BackingCanvas.StrokeSize = LidarArrayLineThickness;
		BackingCanvas.DrawLine(ToCanvasXPosition(lidarArrayStartFieldX), lidarArrayMiddleY, lidarArrayEndX, lidarArrayMiddleY);

		for (int i = 0; i < LidarArray.SensorCount; i++) {

			float sensorX = ToCanvasXPosition(lidarArrayStartFieldX + LidarArray.SensorSpacing.Magnitude * i);

			BackingCanvas.DrawLine(sensorX, lidarArrayTopY, sensorX, lidarArrayBottomY);
		}

	}

}
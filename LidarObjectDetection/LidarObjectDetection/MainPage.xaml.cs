using System.Collections.ObjectModel;
using LidarObjectDetection.LinearAlgebra;
using OneOf;
using Point = LidarObjectDetection.LinearAlgebra.Point;

namespace LidarObjectDetection;



public partial class MainPage : ContentPage {

	public float FieldWidth {
		get => Drawable.FieldWidth;
		set {
			Drawable.FieldWidth = value;
			SettingChanged();
		}
	}

	public float FieldHeight {
		get => Drawable.FieldHeight;
		set {
			Drawable.FieldHeight = value;
			SettingChanged();
		}
	}

	public int LidarSensorCount {
		get => Drawable.LidarSensorCount;
		set {
			Drawable.LidarSensorCount = value;
			SettingChanged();
		}
	}

	public float LidarArrayWidth {
		get => Drawable.LidarArrayWidth;
		set {
			Drawable.LidarArrayWidth = value;
			SettingChanged();
		}
	}

	public double LidarPercentErrorStandardDeviation {
		get => Drawable.LidarPercentErrorStandardDeviation;
		set {
			Drawable.LidarPercentErrorStandardDeviation = value;
			SettingChanged();
		}
	}

	public FieldDrawingManager Drawable { get; } = new() {
		FieldWidth = 3,
		FieldHeight = 2,
		LidarArrayWidth = 1,
		LidarSensorCount = 15,
		LidarPercentErrorStandardDeviation = 1
	};

	public MainPage() {

		BindingContext = this;
		InitializeComponent();
	}

	private void SettingChanged() {
		GraphicsView?.Invalidate();
	}

}




public class FieldCanvas {

	private readonly float FieldWidth;
	private readonly float FieldHeight;

	private ICanvas BackingCanvas { get; }
	private (double Width, double Height) FreeCanvasDimensions { get; }

	private readonly int LidarSensorCount;
	private readonly float LidarArrayWidth;

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
		int lidarSensorCount,
		float lidarArrayWidth) {

		FieldWidth = fieldWidth;
		FieldHeight = fieldHeight;

		BackingCanvas = backingCanvas;
		FreeCanvasDimensions = (canvasDimensions.Width - 2 * BorderThickness, canvasDimensions.Height - 2 * BorderThickness - LidarArrayHeight);

		LidarSensorCount = lidarSensorCount;
		LidarArrayWidth = lidarArrayWidth;

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

	private float ToCanvasXPosition(double xPosition) {

		return (float)(XOffset + xPosition * DrawScalingFactor);
	}

	private float ToCanvasYPosition(double yPosition) {

		return (float)(YOffset + yPosition * DrawScalingFactor);
	}

	private void DrawBorderAndBackground() {

		float fieldLeftX = ToCanvasXPosition(0);
		float fieldWidth = ToCanvasXPosition(FieldWidth) - ToCanvasXPosition(0);
		float fieldTopY = ToCanvasYPosition(0);
		float fieldHeight = ToCanvasYPosition(FieldHeight) - ToCanvasYPosition(0) + LidarArrayHeight;

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

		double lidarArrayStartFieldX = FieldWidth / 2 - LidarArrayWidth / 2;
		float lidarArrayEndX = ToCanvasXPosition(FieldWidth / 2 + LidarArrayWidth / 2);

		float lidarArrayTopY = ToCanvasYPosition(FieldHeight);
		float lidarArrayMiddleY = ToCanvasYPosition(FieldHeight) + LidarArrayHeight / 2;
		float lidarArrayBottomY = ToCanvasYPosition(FieldHeight) + LidarArrayHeight;

		BackingCanvas.StrokeColor = LidarArrayColor;
		BackingCanvas.StrokeSize = LidarArrayLineThickness;
		BackingCanvas.DrawLine(ToCanvasXPosition(lidarArrayStartFieldX), lidarArrayMiddleY, lidarArrayEndX, lidarArrayMiddleY);

		double sensorSpacing = LidarArrayWidth / (LidarSensorCount - 1);
		for (int i = 0; i < LidarSensorCount; i++) {

			float sensorX = ToCanvasXPosition(lidarArrayStartFieldX + sensorSpacing * i);

			BackingCanvas.DrawLine(sensorX, lidarArrayTopY, sensorX, lidarArrayBottomY);
		}

	}

}

public class FieldDrawingManager : IDrawable {

	public required float FieldWidth { get; set; }
	public required float FieldHeight { get; set; }
	public required int LidarSensorCount { get; set; }
	public required float LidarArrayWidth { get; set; }
	public required double LidarPercentErrorStandardDeviation { get; set; }

	public void Draw(ICanvas canvas, RectF dirtyRect) {

		FieldCanvas fieldCanvas = new(FieldWidth, FieldHeight, canvas, dirtyRect, LidarSensorCount, LidarArrayWidth);

		fieldCanvas.DrawLine(0.25, 0.5, 0.75, 1.5, Colors.Red, 1);
	}

}



public class Setup {

	public int LidarSensorCount { get; init; } = 15;

	public double LidarArrayWidth { get; init; } = 1.0;

	public double LidarPercentErrorStandardDeviation { get; init; } = 1.0;

	public double FieldWidth { get; init; } = 3.0;

	public double FieldHeight { get; init; } = 3.0;

}

public class CrossSection {

	public Point CenterPoint;

	public ReadOnlyCollection<Point> Vertices { get; init; } = new List<Point>().AsReadOnly();

}

public class Polygon {

	public ReadOnlyCollection<Point> Points { get; init; }

	public Polygon(Point offset, double rotation) {

	}

	public Polygon CopyWithOffset() {
		throw new NotImplementedException();
	}

}




using System.Collections.ObjectModel;

namespace LidarObjectDetection;



public partial class MainPage : ContentPage {

	public FieldDrawingManager Drawable { get; } = new(3, 2);

	public MainPage() {

		BindingContext = this;
		InitializeComponent();
	}

	private void Button_OnClicked(object sender, EventArgs e) {

		TestGraphicsView.Invalidate();
	}

}




public class FieldUnitsCanvas {

	private Color BackgroundColor { get; }
	private Color BorderColor { get; }
	private float BorderThickness { get; }

	private double FieldWidth { get; }
	private double FieldHeight { get; }
	private ICanvas BackingCanvas { get; }
	private RectF CanvasDimensionsMinusBorder { get; }

	private readonly double XOffset;
	private readonly double YOffset;
	private readonly double DrawScalingFactor;

	public FieldUnitsCanvas(
		double fieldWidth,
		double fieldHeight,
		ICanvas backingCanvas,
		RectF canvasDimensions,
		Color backgroundColor,
		Color borderColor,
		float borderThickness = 1) {

		FieldWidth = fieldWidth;
		FieldHeight = fieldHeight;
		BackgroundColor = backgroundColor;
		BorderColor = borderColor;
		BorderThickness = borderThickness;
		BackingCanvas = backingCanvas;
		CanvasDimensionsMinusBorder = new(0, 0, canvasDimensions.Width - 2 * BorderThickness, canvasDimensions.Height - 2 * BorderThickness);

		double canvasRatio = CanvasDimensionsMinusBorder.Width / CanvasDimensionsMinusBorder.Height;
		double fieldRatio = fieldWidth / fieldHeight;

		bool canvasHasExtraWidth = canvasRatio > fieldRatio;

		if (canvasHasExtraWidth) {
			DrawScalingFactor = CanvasDimensionsMinusBorder.Height / fieldHeight;
			XOffset = (CanvasDimensionsMinusBorder.Width - fieldWidth * DrawScalingFactor) / 2 + BorderThickness;
			YOffset = BorderThickness;

		} else {
			DrawScalingFactor = CanvasDimensionsMinusBorder.Width / fieldWidth;
			XOffset = BorderThickness;
			YOffset = (CanvasDimensionsMinusBorder.Height - fieldHeight * DrawScalingFactor) / 2 + BorderThickness;
		}

		DrawBorderAndBackground();
	}

	public void DrawLine(double xStart, double yStart, double xEnd, double yEnd) {

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
		float fieldHeight = ToCanvasYPosition(FieldHeight) - ToCanvasYPosition(0);

		float borderLeftX = fieldLeftX - BorderThickness;
		float borderWidth = fieldWidth + 2 * BorderThickness;
		float borderTopY = fieldTopY - BorderThickness;
		float borderHeight = fieldHeight + 2 * BorderThickness;

		BackingCanvas.FillColor = BorderColor;
		BackingCanvas.FillRectangle(borderLeftX, borderTopY, borderWidth, borderHeight);

		BackingCanvas.FillColor = BackgroundColor;
		BackingCanvas.FillRectangle(fieldLeftX, fieldTopY, fieldWidth, fieldHeight);
	}

}

public class FieldDrawingManager : IDrawable {

	private readonly double FieldWidth;
	private readonly double FieldHeight;

	public FieldDrawingManager(double fieldWidth, double fieldHeight) {

		FieldWidth = fieldWidth;
		FieldHeight = fieldHeight;
	}

	public void Draw(ICanvas canvas, RectF dirtyRect) {

		canvas.StrokeColor = Colors.Red;
		canvas.StrokeSize = 6;

		FieldUnitsCanvas fieldCanvas = new(FieldWidth, FieldHeight, canvas, dirtyRect, Colors.DarkGray, Colors.White, 5);

		fieldCanvas.DrawLine(0.25, 0.5, 0.75, 1.5);
	}

}



public class Setup {

	public int LidarSensorCount { get; init; } = 15;

	public double LidarArrayWidth { get; init; } = 1.0;

	public double FieldWidth { get; init; } = 3.0;

	public double FieldHeight { get; init; } = 3.0;

}

public class CrossSection {

	public Point CenterPoint;

	public ReadOnlyCollection<Point> Vertices { get; init; } = new List<Point>().AsReadOnly();

}
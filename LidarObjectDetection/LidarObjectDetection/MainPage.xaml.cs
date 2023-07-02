using System.Collections.ObjectModel;
using System.Diagnostics;

namespace LidarObjectDetection;



public partial class MainPage : ContentPage {

	public FieldDrawingManager Drawable { get; } = new(1, 3);

	public MainPage() {

		BindingContext = this;
		InitializeComponent();
	}

	private void Button_OnClicked(object sender, EventArgs e) {

		TestGraphicsView.Invalidate();
	}

}




public class FieldUnitsCanvas {

	private Color BackgroundColor { get; init; }
	private Color BorderColor { get; init; }
	private float BorderThickness { get; init; }

	public double FieldWidth { get; }
	public double FieldHeight { get; }
	private ICanvas BackingCanvas { get; }
	private RectF CanvasDimensions { get; }

	private readonly double XOffset;
	private readonly double YOffset;
	private readonly double DrawScalingFactor;

	public FieldUnitsCanvas(
		double fieldWidth,
		double fieldHeight,
		ICanvas backingCanvas,
		RectF canvasDimensions,
		Color backgroundColor,
		Color borderColor = Colors.White,
		float borderThickness = 1) {

		FieldWidth = fieldWidth;
		FieldHeight = fieldHeight;
		BackgroundColor = backgroundColor;
		BorderColor = borderColor;
		BorderThickness = borderThickness;
		BackingCanvas = backingCanvas;
		CanvasDimensions = canvasDimensions;

		double canvasRatio = CanvasDimensions.Width / CanvasDimensions.Height;
		double fieldRatio = fieldWidth / fieldHeight;

		bool canvasHasExtraWidth = canvasRatio > fieldRatio;

		if (canvasHasExtraWidth) {
			DrawScalingFactor = CanvasDimensions.Height / fieldHeight;
			XOffset = (CanvasDimensions.Width - fieldWidth * DrawScalingFactor) / 2;
			YOffset = 0;

		} else {
			DrawScalingFactor = CanvasDimensions.Width / fieldWidth;
			XOffset = 0;
			YOffset = (CanvasDimensions.Height - fieldHeight * DrawScalingFactor) / 2;
		}

		BackingCanvas.DrawRectangle();
	}

	public void DrawLine(double xStart, double yStart, double xEnd, double yEnd) {

		BackingCanvas.DrawLine(ToCanvasXPosition(xStart), ToCanvasYPosition(yStart), ToCanvasXPosition(xEnd), ToCanvasYPosition(yEnd));
	}

	private float ToCanvasXPosition(double xPosition) {

		return (float)(XOffset + xPosition * DrawScalingFactor);
	}

	private float ToCanvasYPosition(double yPosition) {

		return (float)(YOffset + yPosition * DrawScalingFactor);
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

		FieldUnitsCanvas fieldCanvas = new(FieldWidth, FieldHeight, canvas, dirtyRect);

		DrawFieldBorders(fieldCanvas);
	}

	private void DrawFieldBorders(FieldUnitsCanvas fieldCanvas) {

		fieldCanvas.DrawLine(0, 0, FieldWidth, 0);
		fieldCanvas.DrawLine(FieldWidth, 0, FieldWidth, FieldHeight);
		fieldCanvas.DrawLine(FieldWidth, FieldHeight, 0, FieldHeight);
		fieldCanvas.DrawLine(0, FieldHeight, 0, 0);
	}

}

public class TestDrawable : IDrawable {

	public Setup FieldSetup { get; set; } = new();

	public List<CrossSection> CrossSections { get; } = new();

	public void Draw(ICanvas canvas, RectF rectangle) {

		Debug.WriteLine(canvas);
		Debug.WriteLine(rectangle);

		canvas.StrokeColor = Colors.Red;
		canvas.StrokeSize = 6;

		canvas.DrawCircle(new(10, 10), 10.0);

		canvas.DrawLine(10, 10, 90, 100);
		canvas.DrawRectangle(0, 0, 400, 400);
	}

	public void DrawFieldBounds(ICanvas canvas, RectF rectangle) {

		double canvasRatio = rectangle.Width / rectangle.Height;
		double fieldRatio = FieldSetup.FieldWidth / FieldSetup.FieldHeight;

		bool canvasHasExtraWidth = canvasRatio > fieldRatio;

		double drawRatio = canvasHasExtraWidth
			? rectangle.Height / FieldSetup.FieldHeight
			: rectangle.Width / FieldSetup.FieldWidth;
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
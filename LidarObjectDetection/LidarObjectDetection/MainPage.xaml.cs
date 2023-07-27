using System.Diagnostics;
using LinearAlgebra;

namespace LidarObjectDetection;



public partial class MainPage : ContentPage {

	//public float FieldWidth {
	//	get => Drawable.FieldWidth;
	//	set {
	//		Drawable.FieldWidth = value;
	//		SettingChanged();
	//	}
	//}

	//public float FieldHeight {
	//	get => Drawable.FieldHeight;
	//	set {
	//		Drawable.FieldHeight = value;
	//		SettingChanged();
	//	}
	//}

	//public int LidarSensorCount {
	//	get => Drawable.LidarSensorCount;
	//	set {
	//		Drawable.LidarSensorCount = value;
	//		SettingChanged();
	//	}
	//}

	//public float LidarArrayWidth {
	//	get => Drawable.LidarArrayWidth;
	//	set {
	//		Drawable.LidarArrayWidth = value;
	//		SettingChanged();
	//	}
	//}

	//public double LidarPercentErrorStandardDeviation {
	//	get => Drawable.LidarPercentErrorStandardDeviation;
	//	set {
	//		Drawable.LidarPercentErrorStandardDeviation = value;
	//		SettingChanged();
	//	}
	//}

	public FieldDrawingManager Drawable { get; } = new() {
		FieldWidth = 3,
		FieldHeight = 2,
		LidarArray = new() {
			SensorCount = 15,
			LidarPercentErrorStandardDeviation = 1,
			LidarRange = 5,
			LeftCorner = new() { X = 1.2, Y = 0 },
			RightCorner = new() { X = 1.8, Y = 0 },
		}
	};



	public MainPage() {

		BindingContext = this;
		InitializeComponent();
	}

	//private void SettingChanged() {
	//	GraphicsView?.Invalidate();
	//}

}



public class FieldDrawingManager : IDrawable {

	public required float FieldWidth { get; init; }
	public required float FieldHeight { get; init; }
	public required LidarArray LidarArray { get; init; }

	public void Draw(ICanvas canvas, RectF dirtyRect) {

		FieldCanvas fieldCanvas = new(FieldWidth, FieldHeight, canvas, dirtyRect, LidarArray);

		Polygon square = Polygon.Create(new Point2[] { new(0, 0), new(0.1, 0), new(0.1, 0.1), new(0, 0.1) }) ?? throw new UnreachableException();
		Polygon transformedSquare = square.Rotate(30).Translate(new(1.5, 0.5));

		Point2[] points = LidarArray.SimulateLidarReading(transformedSquare);

		foreach (Point2 point in points) {
			fieldCanvas.DrawPoint((float)point.X, (float)point.Y, Colors.Orange, 2);
		}

		fieldCanvas.DrawPolygon(transformedSquare, Colors.Red, 1);
	}

}
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace LidarObjectDetection;



public partial class MainPage : ContentPage {

	public TestDrawable Drawable { get; set; } = new();

	public MainPage() {

		BindingContext = this;
		InitializeComponent();
	}

}


public class TestDrawable : IDrawable {

	public void Draw(ICanvas canvas, RectF dirtyRect) {

		Debug.WriteLine(canvas);
		Debug.WriteLine(dirtyRect);

		canvas.StrokeColor = Colors.Red;
		canvas.StrokeSize = 6;

		canvas.DrawLine(10, 10, 90, 100);
		canvas.DrawRectangle(0, 0, 400, 400);
	}

	public void DrawShape(ICanvas canvas, RectF dirtyRect) {

	}

}



public class CrossSection {

	public ReadOnlyCollection<Point> Vertices { get; init; } = new List<Point>().AsReadOnly();

}
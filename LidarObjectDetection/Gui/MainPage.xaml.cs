using Microsoft.Maui.Controls;

namespace Gui;



public partial class MainPage : ContentPage {

	public GraphicsManager GraphicsManager { get; } = new() {
		TestCase = TestCases.TestCases.TestCase1,
		FieldTopLeftCorner = new(-5, 3),
		FieldBottomRightCorner = new(5, -3),
		ShowLidarBeams = true,
		ShowRealLidarPoints = true,
		ShowTheoreticalLidarPoints = true,
		ShowShapeToFind = true,
		ShowStartingPoints = true,
		ShowStartingBoxBounds = true,
		ShowFinalPosition = true,
		ShowAllRoutes = true,
		RoutesToShow = new()
	};

	public MainPage() {

		BindingContext = this;
		InitializeComponent();



	}

}
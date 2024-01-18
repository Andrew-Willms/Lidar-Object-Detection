using Microsoft.Maui.Controls;

namespace Gui;



public partial class MainPage : ContentPage {

	public GraphicsManager GraphicsManager { get; } = new() {
		TestCase = TestCases.TestCases.TestCase1,
		FieldTopLeftCorner = new(-15, 11),
		FieldBottomRightCorner = new(15, -1),
		ShowLidarBeams = true,
		ShowObjectsOnField = true,
		ShowRealLidarPoints = true,
		ShowTheoreticalLidarPoints = true,
		ShowShapeToFind = true,
		ShowStartingPoints = true,
		ShowStartingBoxBounds = true,
		ShowFinalPosition = true,
		ShowAllRoutes = true,
		RoutesToShow = default,
		ShowGuessPositions = true,
		ShowGradient = true,
		ShowStep = true
	};

	public MainPage() {

		BindingContext = this;
		InitializeComponent();

		GraphicsView?.Invalidate();
	}

}
using Microsoft.Maui.Controls;

namespace Gui;



public partial class MainPage : ContentPage {

	public GraphicsManager GraphicsManager { get; } = new() {
		FieldTopRightCorner = default,
		FieldBottomLeftCorner = default,
		ShowLidarBeams = false,
		ShowTheoreticalLidarPoints = false,
		ShowShapeToFind = false,
		ShowStartingPoints = false,
		ShowStartingBoxBounds = false,
		ShowFinalPosition = false,
		StartingPointRoutesToShow = default
	};

	public MainPage() {

		BindingContext = this;
		InitializeComponent();
	}

}
﻿using System.Collections.Immutable;
using Microsoft.Maui.Controls;

namespace Gui;



public partial class MainPage : ContentPage {

	public GraphicsManager GraphicsManager { get; } = new() {
		TestCase = TestCases.TestCases.TestCase1,
		FieldTopLeftCorner = new(-3, 4),
		FieldBottomRightCorner = new(3, -0.1),
		ShowLidarBeams = true,
		ShowObjectsOnField = true,
		ShowRealLidarPoints = true,
		//ShowTheoreticalLidarPoints = true,
		ShowShapeToFind = true,
		ShowStartingPoints = true,
		//ShowStartingBoxBounds = true,
		ShowFinalPosition = true,
		ShowAllRoutes = true,
		RoutesToShow = ImmutableArray<int>.Empty,
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
using System.Collections.Immutable;
using LinearAlgebra;
using Microsoft.Maui.Graphics;

namespace Gui; 



public class GraphicsManager : IDrawable {

	public required Point2 FieldTopRightCorner { get; init; }

	public required Point2 FieldBottomLeftCorner { get; init; }

	public required bool ShowLidarBeams { get; init; }

	public bool ShowRealLidarPoints { get; init; }

	public required bool ShowTheoreticalLidarPoints { get; init; }

	public required bool ShowShapeToFind { get; init; }

	public required bool ShowStartingPoints { get; init; }

	public required bool ShowStartingBoxBounds { get; init; }

	//public required bool ShowSearchBounds { get; init; }

	public required bool ShowFinalPosition { get; init; }

	public required ImmutableArray<int> StartingPointRoutesToShow { get; init; }

	public void Draw(ICanvas canvas, RectF dirtyRect) {
		





	}

}
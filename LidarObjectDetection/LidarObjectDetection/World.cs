using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using LinearAlgebra;

namespace LidarObjectDetection; 



public readonly struct World {

	public required ImmutableArray<Polygon> Objects { get; init; }

	public World() { }

	[SetsRequiredMembers]
	public World(params Polygon[] objects) {
		Objects = objects.ToImmutableArray();

	}

}
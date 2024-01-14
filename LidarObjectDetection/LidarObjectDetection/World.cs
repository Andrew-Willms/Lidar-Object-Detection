using System.Collections.Generic;
using LinearAlgebra;

namespace LidarObjectDetection; 



public readonly struct World {

	private readonly List<Polygon> _Objects = new();
	public IReadOnlyCollection<Polygon> Objects => _Objects;

	public World() { }

	public void AddObject(Polygon polygon) {
		_Objects.Add(polygon);
	}

}
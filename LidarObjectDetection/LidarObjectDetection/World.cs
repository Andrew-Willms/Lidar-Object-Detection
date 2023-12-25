using System;
using System.Collections.Generic;
using LinearAlgebra;

namespace LidarObjectDetection; 



public class World {

	private readonly List<Polygon> _Objects = new();
	public IReadOnlyCollection<Polygon> Objects => _Objects;

	public LidarScanner Lidar { get; set; }

	public Vector2 LidarScannerForwardsDirection { get; set; }

	public void AddObject(Polygon polygon) {
		_Objects.Add(polygon);
	}

	public Point3[] GetLidarPoints() {

		throw new NotImplementedException();
	}

	public Point3[] GetLidarPointsWorldSpace() {

		throw new NotImplementedException();
	}

	public LineSegment[] GetLidarBeamsInWorldSpace() {

		throw new NotImplementedException();
	}

}
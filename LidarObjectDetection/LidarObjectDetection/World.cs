using System;
using System.Collections.Generic;
using System.Linq;
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

	public Point2[] GetLidarPoints() {

		throw new NotImplementedException();
	}

	public Point2[] GetLidarPointsWorldSpace() {

		double lidarAngle = Lidar.ForwardsDirection.AngleTo();

		return GetLidarPoints()
			.Select(point => point.Rotate(lidarAngle, Lidar.Center))
			.ToArray();
	}

	public LineSegment[] GetLidarBeamsInWorldSpace() {

		throw new NotImplementedException();
	}

}
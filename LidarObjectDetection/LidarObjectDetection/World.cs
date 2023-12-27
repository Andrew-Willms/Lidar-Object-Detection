using System;
using System.Collections.Generic;
using System.Linq;
using LinearAlgebra;

namespace LidarObjectDetection; 



public class World {

	private readonly List<Polygon> _Objects = new();
	public IReadOnlyCollection<Polygon> Objects => _Objects;

	public LidarScanner Lidar { get; set; }

	public Vector2 LidarDirection { get; set; }

	public void AddObject(Polygon polygon) {
		_Objects.Add(polygon);
	}

	public Point2[] GetLidarPoints() {

		throw new NotImplementedException();
	}

	public Point2[] GetLidarPointsInWorldSpace() {

		double lidarAngle = Lidar.ForwardsDirection.AngleTo(LidarDirection);

		return GetLidarPoints()
			.Select(point => point.Rotated(lidarAngle, Lidar.Center))
			.ToArray();
	}

	public LineSegment[] GetLidarBeamsInWorldSpace() {

		throw new NotImplementedException();
	}

}
﻿using System;
using LinearAlgebra;

namespace LidarObjectDetection;



public delegate Point3[] StartingPointDistributor(int startingPointCount, RectangularRegion searchRegion);



public static class StartingPointDistributors {

	public static Point3[] EvenCubicGridDistributor(int startingPointCount, RectangularRegion searchRegion) {

		double volumePerPoint = searchRegion.Volume / startingPointCount;

		double approximateCellSize = Math.Pow(volumePerPoint, 1 / 3D);

		int cellsWide = (int)Math.Ceiling(searchRegion.Width / approximateCellSize);
		int cellsHigh = (int)Math.Ceiling(searchRegion.Height / approximateCellSize);
		int cellsDeep = (int)Math.Ceiling(searchRegion.Depth / approximateCellSize);

		double cellWidth = searchRegion.Width / cellsWide;
		double cellHeight = searchRegion.Height / cellsHigh;
		double cellDepth = searchRegion.Width / cellsDeep;

		Point3[] points = new Point3[cellsWide * cellsHigh * cellsDeep];

		int cellIndex = 0;
		for (int widthIndex = 0; widthIndex < cellsWide; widthIndex++) {
			for (int heightIndex = 0; heightIndex < cellsHigh; heightIndex++) {
				for (int depthIndex = 0; depthIndex < cellsDeep; depthIndex++) {

					points[cellIndex] = new() {
						X = cellWidth * (0.5 + widthIndex),
						Y = cellHeight * (0.5 + heightIndex),
						Z = cellDepth * (0.5 + depthIndex)
					};

					cellIndex++;
				}
			}
		}

		return points;
	}

}
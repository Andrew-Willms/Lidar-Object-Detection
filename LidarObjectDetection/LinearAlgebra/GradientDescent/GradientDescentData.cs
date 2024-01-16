using System.Collections.Generic;

namespace LinearAlgebra.GradientDescent; 



public class GradientDescentData {
	
	public required GradientDescentParameters Parameters { get; init; }

	public List<Point3> Points { get; } = new();

	public List<Vector3> Gradients { get; } = new();

	public List<Vector3> Steps { get; } = new();

	//public List<List<Point2>> TheoreticalLidarPoints { get; } = new(); // todo

}
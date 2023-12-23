using System.Collections.Generic;

namespace LinearAlgebra.GradientDescent; 



public class GradientDescentData {
	
	public required GradientDescentParameters Parameters { get; init; }

	public List<Point3> Points { get; set; } = new();

	public List<Vector3> Gradients { get; set; } = new();

	public List<Vector3> Steps { get; set; } = new();

}
using System.Collections.Generic;

namespace LinearAlgebra.GradientDescent; 



public class GradientDescentData {
	
	public required GradientDescentParameters Parameters { get; init; }

	public required List<Point3> Points { get; init; }

	public required List<Vector3> Gradients { get; init; }

	public required List<Vector3> Steps { get; init; }

}
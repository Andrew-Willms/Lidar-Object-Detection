namespace LinearAlgebra; 



public class RectangularRegion {

	public required Point3 CornerA { get; set; }

	public required Point3 CornerB { get; set; }

	public double Width => CornerB.X - CornerA.X;

	public double Height => CornerB.Y - CornerA.Y;

	public double Depth => CornerB.Z - CornerA.Z;

	public double Volume => Width * Height * Depth;

}
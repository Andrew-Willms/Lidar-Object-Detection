namespace LidarObjectDetection.LinearAlgebra;



//public class Line {
	
//	public required Point Point { get; init; }

//	public required Vector Direction { get; init; }

//	private Line() { }

//	public static Line? Create(Point point, Vector direction) {

//		if (direction == Vector.Zero) {
//			return null;
//		}

//		return new() { Point = point, Direction = direction };

//	}

//	public static Line? Create(Point point1, Point point2) {

//		if (point1 == point2) {
//			return null;
//		}

//		return new() { Point = point1, Direction = new(point2) };
//	}

//}
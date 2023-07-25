using GradientDescentTest;
using LinearAlgebra;


double Function1(Point3 point) => point.X * point.X + point.Y * point.Y + point.Z * point.Z;
double Function2(Point3 point) => point.X * point.X * (point.X * point.X - point.X - 2);
double Function3(Point3 point) => Math.Sin(point.X * 5) + Math.Sin(point.Y * 5) + Math.Sin(point.Z * 5) - 0.01 * point.X - 0.01 * point.Y - 0.01 * point.Z;

Point3 startingPoint1 = new() { X = 105.23423d, Y = 98.2343, Z = -546.333 };
Point3 startingPoint2 = new() { X = -2.5, Y = 4.5, Z = 1 };

//Point3 minimum1 = GradientUtilities.GradientDescent(Function1, startingPoint1);
//Point3 minimum2 = GradientUtilities.GradientDescent(Function2, startingPoint2);
//Point3 minimum3 = GradientUtilities.MultiStartGradientDescent(Function2, new(-5, 4.5, 1), new(5, 4.5, 1), 3, 3, 3);
Point3 minimum4 = GradientUtilities.MultiStartGradientDescent(Function3, new(-5, -5, -5), new(5, 5, 5), 5, 5, 5);

//Console.WriteLine($"{Function1(minimum1)} {{{minimum1.X}, {minimum1.Y}, {minimum1.Z}}}");
//Console.WriteLine($"{Function2(minimum2)} {{{minimum2.X}, {minimum2.Y}, {minimum2.Z}}}");
//Console.WriteLine($"{Function2(minimum3)} {{{minimum3.X}, {minimum3.Y}, {minimum3.Z}}}");
Console.WriteLine($"{Function3(minimum4)} {minimum4}");
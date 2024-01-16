using System;
using System.Collections.Generic;
using LinearAlgebra;
using LinearAlgebra.GradientDescent;


(Point3? position, List<GradientDescentData> data) = TestCases.TestCases.TestCase1.Execute();

Console.WriteLine(position);
Console.WriteLine(data);
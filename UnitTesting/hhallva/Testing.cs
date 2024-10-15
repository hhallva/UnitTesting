using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingLib.Math;

namespace UnitTesting.hhallva
{
    public class Testing
    {
        private readonly BasicCalc _calculator;

        public Testing()
        {
            _calculator = new BasicCalc();
        }

        [Fact]
        public void TestSqrt()
        {
            double result = _calculator.Sqrt(4);
            Assert.Equal(2, result);
        }

        [Fact]
        public void TestSolveQuadraticEquation()
        {
            (double?, double?) result = _calculator.SolveQuadraticEquation(-1d, 7d, 8d);
            Assert.Equal((-1d, 8d), result);
        }

        [Theory]
        [InlineData(4, 2)]
        public void InlineTest_Sqrt(double n, double expected)
        {
            double result = _calculator.Sqrt(n);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(-1d, 7d, 8d, -1d, 8d)]
        public void InlineTest_SolveQuadraticEquation(double a, double b, double c, double? x1, double? x2 )
        {
            (double?, double?) result = _calculator.SolveQuadraticEquation(a, b, c);

            Assert.Equal(x1, result.Item1);
            Assert.Equal(x2, result.Item2);
        }

        [Fact]
        public void ThrowTest_Sqrt()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _calculator.Sqrt(-1));
        }

        [Fact]
        public void ThrowTest_SolveQuadraticEquation()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _calculator.SolveQuadraticEquation(0, 9, 3));
        }
    }
}

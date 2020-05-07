using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scratch.Regression
{
	/// <summary>
	///		Purposely implementing things from scratch for clearest explanation.
	/// </summary>
	public static class Formulas
	{
		public static decimal Mean(
			params decimal[] values)
		{
			return values.Sum() / values.Length;
		}

		public static decimal Variance(
			params decimal[] values)
		{
			decimal mean = Formulas.Mean(values);

			return values.Sum(value => 
				Formulas.Square(value - mean));
		}

		public static decimal Covariance(
			params (decimal x, decimal y)[] values)
		{
			decimal meanX = Formulas.Mean(
				values.Select(valueSet => valueSet.x).ToArray());
			decimal meanY = Formulas.Mean(
				values.Select(valueSet => valueSet.y).ToArray());

			return values.Sum(valueSet =>
				(valueSet.x - meanX) * (valueSet.y - meanY));
		}

		public static decimal Square(
			decimal value)
		{
			// Math.Pow does not work for decimals without casting to double
			return value * value;
		}

		public static decimal Sqrt(decimal x, decimal epsilon = 0.0M)
		{
			// https://stackoverflow.com/a/6755197

			// x - a number, from which we need to calculate the square root
			// epsilon - an accuracy of calculation of the root from our number.
			// The result of the calculations will differ from an actual value
			// of the root on less than epslion.

			if (x < 0) throw new OverflowException("Cannot calculate square root from a negative number");

			decimal current = (decimal)Math.Sqrt((double)x), previous;
			do
			{
				previous = current;
				if (previous == 0.0M) return 0;
				current = (previous + x / previous) / 2;
			}
			while (Math.Abs(previous - current) > epsilon);
			return current;
		}
	}
}

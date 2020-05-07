using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scratch.Regression
{
	public static class Regression
	{
		public static (decimal b1, decimal b0) Solve(
			params (decimal x, decimal y)[] values)
		{
			decimal[] xArray = values
				.Select(valueSet => valueSet.x).ToArray();
			decimal[] yArray = values
				.Select(valueSet => valueSet.y).ToArray();

			decimal meanX = Formulas.Mean(xArray);
			decimal meanY = Formulas.Mean(yArray);

			decimal b1 =
				Formulas.Covariance(values) /
				Formulas.Variance(xArray);

			decimal b0 = meanY - b1 * meanX;

			return (b1, b0);
		}

		public static (decimal[] b, decimal b0) Solve(
			params (decimal[] x, decimal y)[] values)
		{
			// https://rpubs.com/aaronsc32/qr-decomposition-householder

			// QR decomposition

			// by the Householder transformation

			// R is the upper triangular matrix found from recursively multiplying
			// the found Householder matrix by the original matrix

			// the Q matrix can be found by taking the dot product of each
			// successively formed Householder matrix

			// https://en.wikipedia.org/wiki/QR_decomposition#Using_Householder_reflections

			// works for rows >= columns

			// loop over how many columns



			return (b: new decimal[] { }, b0: 0);
		}

		public static decimal Predict(
			decimal x,
			decimal b1,
			decimal b0)
		{
			return b1 * x + b0;
		}

		public static decimal Predict(
			decimal[] x,
			decimal[] b,
			decimal b0)
		{
			return 0;
		}
	}
}

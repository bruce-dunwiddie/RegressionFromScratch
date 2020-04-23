using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scratch.Regression
{
	public class ModelAnalysis
	{
		/// <summary>
		///		Root Mean Square Error
		/// </summary>
		/// <returns></returns>
		public static decimal RMSE((decimal predicted, decimal actual)[] results)
		{
			// https://machinelearningmastery.com/implement-simple-linear-regression-scratch-python/

			decimal sumOfErrorSquared = results.Sum(
				result => {
					decimal error = result.predicted - result.actual;
					decimal errorSquared = Formulas.Square(error);

					return errorSquared;
				});

			decimal meanErrorSquared = sumOfErrorSquared / results.Length;

			decimal meanError = (decimal)Math.Sqrt(
				(double)meanErrorSquared);

			return meanError;
		}

		public static decimal R2((decimal predicted, decimal actual)[] results)
		{
			// The Formula for R - Squared Is:
			//
			// R² = 1 - (Unexplained Variation) / (Total Variation)

			// To calculate the total variance, you would subtract the average actual value from
			// each of the actual values, square the results and sum them. From there, divide the
			// first sum of errors(explained variance) by the second sum(total variance), subtract
			// the result from one, and you have the R - squared.

			// https://www.edureka.co/blog/least-square-regression/

			decimal unexplainedVariation = 0;
			decimal totalVariation = 0;

			decimal meanActual = Formulas.Mean(
				results.Select(valueSet => valueSet.actual).ToArray());

			results.ToList().ForEach(
				result =>
				{
					unexplainedVariation += Formulas.Square(
						result.actual - result.predicted);

					totalVariation += Formulas.Square(
						result.actual - meanActual);
				});

			return 1 - (unexplainedVariation / totalVariation);
		}

		public static decimal AdjustedR2()
		{
			// https://github.com/hamzanasirr/Adjusted-R-Squared-Coefficient-code-in-Python

			return 0;
		}
	}
}

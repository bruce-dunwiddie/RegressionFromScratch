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

			// once the QR matrix is found, back substitution can be used to find the
			// coefficients

			// TODO: other method = inverse matrix method

			// TODO: other method = MLE (maximum likelihood estimation)

			// convert array of equations to coefficient matrix

			int height = values.Length;
			int width = values[0].x.Length;

			decimal[,] x = new decimal[height, width + 1];

			for (int rowIndex = 0; rowIndex < height; rowIndex++)
			{
				// intercept

				// Typically the first column of X is a column containing 1’s that correspond to the intercept.
				// Then the first β calculated according to the formula will be the estimated intercept.
				x[rowIndex, 0] = 1;

				for (int columnIndex = 0; columnIndex < width; columnIndex++)
				{
					x[rowIndex, columnIndex + 1] = values[rowIndex].x[columnIndex];
				}
			}

			int iteration = 1;

			Matrix A = new Matrix(x);

			Matrix H = A.GetHouseholder();

			Matrix Q = H;

			Matrix R = Matrices.Multiply(
				H,
				A);

			if (iteration < A.Width)
			{
				// save storage space for the expanded versions of H
				Matrix HFinal = H;

				while (iteration < A.Width)
				{
					A = R.GetMinor(
						startingRowIndex: iteration,
						height: R.Height - iteration,
						startingColumnIndex: iteration,
						width: R.Width - iteration);

					H = A.GetHouseholder();

					// expand H to add 1's on diagonal for dropped rows/columns
					for (int rowIndex = 0; rowIndex < iteration; rowIndex++)
					{
						for (int columnIndex = 0; columnIndex < HFinal.Width; columnIndex++)
						{
							if (rowIndex == columnIndex)
							{
								HFinal[rowIndex, columnIndex] = 1;
							}
							else
							{
								HFinal[rowIndex, columnIndex] = 0;
							}
						}
					}

					for (int rowIndex = iteration; rowIndex < HFinal.Height; rowIndex++)
					{
						for (int columnIndex = 0; columnIndex < iteration; columnIndex++)
						{
							HFinal[rowIndex, columnIndex] = 0;
						}
					}

					for (int rowIndex = 0; rowIndex < H.Width; rowIndex++)
					{
						for (int columnIndex = 0; columnIndex < H.Height; columnIndex++)
						{
							HFinal[rowIndex + iteration, columnIndex + iteration] = H[rowIndex, columnIndex];
						}
					}

					Q = Matrices.Multiply(
						Q,
						HFinal);

					R = Matrices.Multiply(
						HFinal,
						R);

					iteration++;
				}
			}

			Matrix QR = Matrices.Multiply(
				Q,
				R);

			decimal[] b = new decimal[width];
			decimal b0 = 0;

			// need to loop including intercept
			for (int variableIndex = width; variableIndex >= 0; variableIndex--)
			{
				// use back substitution to drop out solved variables
				decimal substitutionSum = 0;

				for (int substituteIndex = b.Length - 1; substituteIndex > variableIndex - 1; substituteIndex--)
				{
					substitutionSum += b[substituteIndex] * QR[variableIndex, substituteIndex + 1];
				}

				// solve for next variable coefficient
				decimal coefficient = (values[variableIndex].y - substitutionSum) / QR[variableIndex, variableIndex + 1];

				if (variableIndex > 0)
				{
					b[variableIndex] = coefficient;
				}
				else
				{
					b0 = coefficient;
				}
			}

			return (b: b, b0: b0);
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

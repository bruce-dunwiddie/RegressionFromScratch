using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Scratch.Regression;

namespace Tests
{
	public class RegressionTests
	{
		[Test]
		public void Regression_Solve_Simple()
		{
			// https://machinelearningmastery.com/implement-simple-linear-regression-scratch-python/

			var solution = Regression.Solve(
				(1m, 1m),
				(2m, 3m),
				(4m, 3m),
				(3m, 2m),
				(5m, 5m));

			Assert.AreEqual(0.8, solution.b1);
			Assert.AreEqual(0.4, solution.b0);
		}

		[Test]
		public void Regression_Solve_OLS_Partials()
		{
			// Boston dataset downloaded from http://lib.stat.cmu.edu/datasets/boston

			// validating results against values from https://towardsdatascience.com/simple-and-multiple-linear-regression-in-python-c928425168f9

			Matrix A = new Matrix(
				new decimal[,]
				{
					{12, -51, 4},
					{6, 167, -68},
					{-4, 24, -41}
				});

			// don't alter a1 or it will alter A
			ColumnVector a1 = A.GetColumnVector(
				0,
				0,
				A.Height);

			decimal norm = a1.GetNorm();

			// create copy of vector that can be edited
			Matrix u = a1.AsMatrix();

			Assert.AreEqual(
				new decimal[,]
				{
					{12},
					{6},
					{-4}
				},
				a1.AsMatrix().Array);

			// subtract norm * e vector
			// the sign is selected so it has the opposite sign of u1
			decimal u1 = u[0, 0];
			u[0, 0] -= Math.Sign(u1) * norm;

			u.DivideBy(2);

			Matrix uT = u.GetTranspose();

			Matrix uuT = Matrices.Multiply(
				u,
				uT);

			Matrix I = Matrices.GetIdentity(
				uuT.Width);

			uuT.MultiplyBy(
				2 / norm);

			Matrix H = Matrices.Subtract(
				I,
				uuT);

			Matrix HA = Matrices.Multiply(
				H,
				A);

			decimal[,] expected = new decimal[,]
				{
					{6/7m, 3/7m, -2/7m},
					{3/7m, -2/7m, 6/7m},
					{-2/7m, 6/7m, 3/7m}
				};

			Assert.AreEqual(
				expected,
				H.Array);

			A = new Matrix(
				new decimal[,]
				{
					{4, 1, -2, 2},
					{1, 2, 0, 1},
					{-2, 0, 3, -2},
					{2, 1, -2, -1}
				});

			//Q1 = A.GetHouseholderMatrix();

			//expected = new decimal[,]
			//	{
			//		{1, 0, 0, 0},
			//		{0, -1/3m, 2/3m, -2/3m},
			//		{0, 2/3m, 2/3m, 1/3m},
			//		{0, -2/3m, 1/3m, 2/3m}
			//	};
		}

		[Test]
		public void Regression_Solve_OLS()
		{
			Matrix A = new Matrix(
				new decimal[,]
				{
					{12, -51, 4},
					{6, 167, -68},
					{-4, 24, -41}
				});

			//Matrix A = new Matrix(
			//	new decimal[,]
			//	{
			//		{2, -2, 18},
			//		{2, 1, 0},
			//		{1, 2, 0}
			//	});

			int iteration = 1;

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

			//decimal[] b = new decimal[width];
			//decimal b0 = 0;

			//// need to loop including intercept
			//for (int variableIndex = width; variableIndex >= 0; variableIndex--)
			//{
			//	// use back substitution to drop out solved variables
			//	decimal substitutionSum = 0;

			//	for (int substituteIndex = b.Length - 1; substituteIndex > variableIndex - 1; substituteIndex--)
			//	{
			//		substitutionSum += b[substituteIndex] * QR[variableIndex, substituteIndex + 1];
			//	}

			//	// solve for next variable coefficient
			//	decimal coefficient = (values[variableIndex].y - substitutionSum) / QR[variableIndex, variableIndex + 1];

			//	if (variableIndex > 0)
			//	{
			//		b[variableIndex] = coefficient;
			//	}
			//	else
			//	{
			//		b0 = coefficient;
			//	}
			//}


		}

		[Test]
		public void Regression_Predict()
		{
			decimal[] dataset = new decimal[]
			{
				1,
				2,
				4,
				3,
				5
			};

			var predictions = dataset.Select(d => Regression.Predict(
				d,
				0.8m,
				0.4m)).ToArray();

			Assert.AreEqual(
				new decimal[]
				{
					1.2m,
					2m,
					3.6m,
					2.8m,
					4.4m
				},
				predictions);
		}
	}
}

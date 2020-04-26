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
		public void Regression_Solve_OLS()
		{
			// Boston dataset downloaded from http://lib.stat.cmu.edu/datasets/boston

			// validating results against values from https://towardsdatascience.com/simple-and-multiple-linear-regression-in-python-c928425168f9

			decimal[,] A = new decimal[,]
			{
				{12, -51, 4},
				{6, 167, -68},
				{-4, 24, -41}
			};

			int height = A.GetLength(0);
			int width = A.GetLength(1);

			decimal[,] a1 = Matrices.GetColumnVector(
				A,
				0);

			//decimal[,] a1 = Matrices.SliceVertical(
			//	A,
			//	0,
			//	0,
			//	height);

			Assert.AreEqual(
				new decimal[,]
				{
					{12},
					{6},
					{-4}
				},
				a1);

			decimal[,] a1T = Matrices.GetTranspose(
				a1);

			//decimal sumA1 = 0;

			//for (int rowIndex = 0; rowIndex < a1.GetLength(0); rowIndex++)
			//{
			//	sumA1 += a1[rowIndex, 0];
			//}

			//Assert.AreEqual(14, sumA1);

			//a1[0, /* will always be 0 */ 0] -= sumA1;

			decimal[,] Q1 = Matrices.GetHouseholderMatrix(
				A);

			decimal[,] expected = new decimal[,]
				{
					{6/7m, 3/7m, -2/7m},
					{3/7m, -2/7m, 6/7m},
					{-2/7m, 6/7m, 3/7m}
				};

			//Assert.AreEqual(
			//	new decimal[,]
			//	{
			//		{6/7m, 3/7m, -2/7m},
			//		{3/7m, -2/7m, 6/7m},
			//		{-2/7m, 6/7m, 3/7m}
			//	},
			//	Q1);

			A = new decimal[,]
				{
					{4, 1, -2, 2},
					{1, 2, 0, 1},
					{-2, 0, 3, -2},
					{2, 1, -2, -1}
				};

			Q1 = Matrices.GetHouseholderMatrix(
				A);

			expected = new decimal[,]
				{
					{1, 0, 0, 0},
					{0, -1/3m, 2/3m, -2/3m},
					{0, 2/3m, 2/3m, 1/3m},
					{0, -2/3m, 1/3m, 2/3m}
				};
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

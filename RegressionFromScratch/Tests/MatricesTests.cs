using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

using Scratch.Regression;

namespace Tests
{
	public class MatricesTests
	{
		[Test]
		public void Matrices_Multiply_1x3times3x1()
		{
			Matrix left = new Matrix(
				new decimal[,]
				{
					{1, 2, 3}
				});

			Matrix right = new Matrix(
				new decimal[,]
				{
					{4},
					{5},
					{6}
				});

			Matrix result = Matrices.Multiply(
				left,
				right);

			Assert.AreEqual(
				new decimal[,]
				{
					{32}
				},
				result.Array);
		}

		[Test]
		public void Matrices_Multiply_3x1times1x3()
		{
			Matrix left = new Matrix(
				new decimal[,]
				{
					{4},
					{5},
					{6}
				});

			Matrix right = new Matrix(
				new decimal[,]
				{
					{1, 2, 3}
				});

			Matrix result = Matrices.Multiply(
				left,
				right);

			Assert.AreEqual(
				new decimal[,]
				{
					{4, 8, 12},
					{5, 10, 15},
					{6, 12, 18}
				},
				result.Array);
		}

		[Test]
		public void Matrices_Multiply_2x2()
		{
			Matrix left = new Matrix(
				new decimal[,]
				{
					{1, 2},
					{3, 4}
				});

			Matrix right = new Matrix(
				new decimal[,]
				{
					{2, 0},
					{1, 2}
				});

			Matrix result = Matrices.Multiply(
				left,
				right);

			Assert.AreEqual(
				new decimal[,]
				{
					{4, 4},
					{10, 8}
				},
				result.Array);
		}

		[Test]
		public void Matrices_Multiply_2x2Flipped()
		{
			Matrix left = new Matrix(
				new decimal[,]
				{
					{2, 0},
					{1, 2}
				});

			Matrix right = new Matrix(
				new decimal[,]
				{
					{1, 2},
					{3, 4}
				});

			Matrix result = Matrices.Multiply(
				left,
				right);

			Assert.AreEqual(
				new decimal[,]
				{
					{2, 4},
					{7, 10}
				},
				result.Array);
		}

		[Test]
		public void Matrices_Multiply_AnotherExample()
		{
			Matrix left = new Matrix(
				new decimal[,]
				{
					{-1},
					{3},
					{-2}
				});

			Matrix right = new Matrix(
				new decimal[,]
				{
					{-1, 3, -2}
				});

			Matrix result = Matrices.Multiply(
				left,
				right);

			Assert.AreEqual(
				new decimal[,]
				{
					{1, -3, 2},
					{-3, 9, -6},
					{2, -6, 4}
				},
				result.Array);
		}
	}
}

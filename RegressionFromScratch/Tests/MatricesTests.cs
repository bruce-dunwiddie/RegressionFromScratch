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
			decimal[,] left = new decimal[,]
			{
				{1, 2, 3}
			};

			decimal[,] right = new decimal[,]
			{
				{4},
				{5},
				{6}
			};

			decimal[,] result = Matrices.Multiply(
				left,
				right);

			Assert.AreEqual(
				new decimal[,]
				{
					{32}
				},
				result);
		}

		[Test]
		public void Matrices_Multiply_3x1times1x3()
		{
			decimal[,] left = new decimal[,]
			{
				{4},
				{5},
				{6}
			};

			decimal[,] right = new decimal[,]
			{
				{1, 2, 3}
			};

			decimal[,] result = Matrices.Multiply(
				left,
				right);

			Assert.AreEqual(
				new decimal[,]
				{
					{4, 8, 12},
					{5, 10, 15},
					{6, 12, 18}
				},
				result);
		}

		[Test]
		public void Matrices_Multiply_2x2()
		{
			decimal[,] left = new decimal[,]
			{
				{1, 2},
				{3, 4}
			};

			decimal[,] right = new decimal[,]
			{
				{2, 0},
				{1, 2}
			};

			decimal[,] result = Matrices.Multiply(
				left,
				right);

			Assert.AreEqual(
				new decimal[,]
				{
					{4, 4},
					{10, 8}
				},
				result);
		}

		[Test]
		public void Matrices_Multiply_2x2Flipped()
		{
			decimal[,] left = new decimal[,]
			{
				{2, 0},
				{1, 2}
			};

			decimal[,] right = new decimal[,]
			{
				{1, 2},
				{3, 4}
			};

			decimal[,] result = Matrices.Multiply(
				left,
				right);

			Assert.AreEqual(
				new decimal[,]
				{
					{2, 4},
					{7, 10}
				},
				result);
		}

		[Test]
		public void Matrices_Multiply_AnotherExample()
		{
			decimal[,] left = new decimal[,]
			{
				{-1},
				{3},
				{-2}
			};

			decimal[,] right = new decimal[,]
			{
				{-1, 3, -2}
			};

			decimal[,] result = Matrices.Multiply(
				left,
				right);

			Assert.AreEqual(
				new decimal[,]
				{
					{1, -3, 2},
					{-3, 9, -6},
					{2, -6, 4}
				},
				result);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

using Scratch.Regression;

namespace Tests
{
	public class MatrixTests
	{
		[Test]
		public void Matrix_GetInverse()
		{
			// https://www.mathwords.com/i/inverse_of_a_matrix.htm
			Matrix A = new Matrix(
				new decimal[,]
				{
					{1, 2, 3},
					{0, 4, 5},
					{1, 0, 6}
				});

			Matrix InvA = A.GetInverse();

			Assert.AreEqual(
				new decimal[,]
				{
					{12/11m, -6/11m, -1/11m},
					{5/22m, 3/22m, -5/22m},
					{-2/11m, 1/11m, 2/11m}
				},
				InvA.Array);
		}

		[Test]
		public void Matrix_GetCofactor()
		{
			// https://www.mathwords.com/i/inverse_of_a_matrix.htm
			Matrix A = new Matrix(
				new decimal[,]
				{
					{1, 2, 3},
					{0, 4, 5},
					{1, 0, 6}
				});

			Matrix cofactor = A.GetCofactor();

			Assert.AreEqual(
				new decimal[,]
				{
					{24, 5, -4},
					{-12, 3, 2},
					{-2, -5, 4}
				},
				cofactor.Array);
		}

		[Test]
		public void Matrix_GetAdjugate()
		{
			// https://www.mathwords.com/i/inverse_of_a_matrix.htm
			Matrix A = new Matrix(
				new decimal[,]
				{
					{1, 2, 3},
					{0, 4, 5},
					{1, 0, 6}
				});

			Matrix adjA = A.GetAdjugate();

			Assert.AreEqual(
				new decimal[,]
				{
					{24, -12, -2},
					{5, 3, -5},
					{-4, 2, 4}
				},
				adjA.Array);
		}

		[Test]
		public void Matrix_GetDeterminant()
		{
			// https://www.mathwords.com/i/inverse_of_a_matrix.htm
			Matrix A = new Matrix(
				new decimal[,]
				{
					{1, 2, 3},
					{0, 4, 5},
					{1, 0, 6}
				});

			decimal detA = A.GetDeterminant();

			Assert.AreEqual(
				22,
				detA);
		}

		[Test]
		public void Matrix_GetDeterminant2x2()
		{
			// https://www.mathsisfun.com/algebra/matrix-determinant.html
			Matrix A = new Matrix(
				new decimal[,]
				{
					{3, 8},
					{4, 6}
				});

			decimal detA = A.GetDeterminant();

			Assert.AreEqual(
				-14,
				detA);
		}

		[Test]
		public void Matrix_GetDeterminant3x3()
		{
			// https://www.mathsisfun.com/algebra/matrix-determinant.html
			Matrix A = new Matrix(
				new decimal[,]
				{
					{6, 1, 1},
					{4, -2, 5},
					{2, 8, 7}
				});

			decimal detA = A.GetDeterminant();

			Assert.AreEqual(
				-306,
				detA);
		}
	}
}

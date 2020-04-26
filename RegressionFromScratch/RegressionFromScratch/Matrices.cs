using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scratch.Regression
{
	public static class Matrices
	{
		public static decimal[,] GetColumnVector(
			decimal[,] matrix,
			int columnIndex)
		{
			int height = matrix.GetLength(0);

			decimal[,] vector = new decimal[height, 1];

			for (int rowIndex = 0; rowIndex < height; rowIndex++)
			{
				vector[rowIndex, 0] = matrix[rowIndex, columnIndex];
			}

			return vector;
		}

		public static decimal[,] GetMinor(
			decimal[,] matrix,
			int startingColumnIndex,
			int width,
			int startingRowIndex,
			int height)
		{
			decimal[,] minor = new decimal[height, width];

			for (int rowIndex = 0; rowIndex < height; rowIndex++)
			{
				for (int columnIndex = 0; columnIndex < width; columnIndex++)
				{
					minor[rowIndex, columnIndex] =
						matrix[startingRowIndex + rowIndex, startingColumnIndex + columnIndex];
				}
			}

			return minor;
		}

		public static decimal[,] Multiply(
			decimal[,] left,
			decimal[,] right)
		{
			int leftHeight = left.GetLength(0);
			int leftWidth = left.GetLength(1);

			int rightWidth = right.GetLength(1);

			// https://www.mathsisfun.com/algebra/matrix-multiplying.html

			// number of columns in left matrix must match number of rows in right matrix

			int resultHeight = leftHeight;
			int resultWidth = rightWidth;

			decimal[,] result = new decimal[resultHeight, resultWidth];

			for (int rowIndex = 0; rowIndex < resultHeight; rowIndex++)
			{
				for (int columnIndex = 0; columnIndex < resultWidth; columnIndex++)
				{
					decimal sum = 0;

					for (int leftColumnIndex = 0; leftColumnIndex < leftWidth; leftColumnIndex++)
					{
						sum +=
							left[rowIndex, leftColumnIndex] *
							right[leftColumnIndex, columnIndex];
					}

					result[rowIndex, columnIndex] = sum;
				}
			}

			return result;
		}

		public static decimal[,] Multiply(
			decimal left,
			decimal[,] right)
		{
			int height = right.GetLength(0);
			int width = right.GetLength(1);

			decimal[,] result = new decimal[height, width];

			for (int rowIndex = 0; rowIndex < height; rowIndex++)
			{
				for (int columnIndex = 0; columnIndex < width; columnIndex++)
				{
					result[rowIndex, columnIndex] = right[rowIndex, columnIndex] * left;
				}
			}

			return result;
		}

		public static decimal[,] Divide(
			decimal[,] left,
			decimal right)
		{
			int height = left.GetLength(0);
			int width = left.GetLength(1);

			decimal[,] result = new decimal[height, width];

			for (int rowIndex = 0; rowIndex < height; rowIndex++)
			{
				for (int columnIndex = 0; columnIndex < width; columnIndex++)
				{
					result[rowIndex, columnIndex] = left[rowIndex, columnIndex] / right;
				}
			}

			return result;
		}

		public static decimal[,] Sum(
			decimal[,] matrix)
		{
			int height = matrix.GetLength(0);
			int width = matrix.GetLength(1);

			decimal[,] result = new decimal[1, width];

			for (int columnIndex = 0; columnIndex < width; columnIndex++)
			{
				decimal sum = 0;

				for (int rowIndex = 0; rowIndex < height; rowIndex++)
				{
					sum += matrix[rowIndex, columnIndex];
				}

				result[0, columnIndex] = sum;
			}

			return result;
		}

		public static decimal[,] GetTranspose(
			decimal[,] matrix)
		{
			// https://en.wikipedia.org/wiki/Transpose

			// the transpose of a matrix is an operator which flips a matrix over its diagonal,
			// that is it switches the row and column indices of the matrix.

			int height = matrix.GetLength(0);
			int width = matrix.GetLength(1);

			decimal[,] transpose = new decimal[width, height];

			for (int rowIndex = 0; rowIndex < height; rowIndex++)
			{
				for (int columnIndex = 0; columnIndex < width; columnIndex++)
				{
					transpose[columnIndex, rowIndex] = matrix[rowIndex, columnIndex];
				}
			}

			return transpose;
		}

		public static decimal[,] GetIdentity(
			int size)
		{
			// https://en.wikipedia.org/wiki/Identity_matrix

			// In linear algebra, the identity matrix, or sometimes ambiguously called a unit matrix,
			// of size n is the n × n square matrix with ones on the main diagonal and zeros elsewhere.

			decimal[,] identity = new decimal[size, size];

			for (int rowIndex = 0; rowIndex < size; rowIndex++)
			{
				for (int columnIndex = 0; columnIndex < size; columnIndex++)
				{
					if (rowIndex == columnIndex)
					{
						identity[rowIndex, columnIndex] = 1;
					}
					else
					{
						identity[rowIndex, columnIndex] = 0;
					}
				}
			}

			return identity;
		}

		public static decimal[,] Subtract(
			decimal[,] left,
			decimal[,] right)
		{
			int height = left.GetLength(0);
			int width = left.GetLength(1);

			// matrices must be the same size

			decimal[,] result = new decimal[height, width];

			for (int rowIndex = 0; rowIndex < height; rowIndex++)
			{
				for (int columnIndex = 0; columnIndex < width; columnIndex++)
				{
					result[rowIndex, columnIndex] =
						left[rowIndex, columnIndex] -
						right[rowIndex, columnIndex];
				}
			}

			return result;
		}

		public static decimal[,] GetHouseholderMatrix(
			decimal[,] matrix)
		{
			// creating a new alias to match commonly used algorithm variables
			decimal[,] A = matrix;

			int m = A.GetLength(0);
			int n = A.GetLength(1);

			decimal[,] a1 = Matrices.GetColumnVector(
				A,
				0);

			decimal sumA1 = Matrices.Sum(
				a1)[0, 0];

			a1[0, 0] -= sumA1;

			a1 = Matrices.Divide(
				a1,
				2);

			decimal[,] a1T = Matrices.GetTranspose(
				a1);

			decimal[,] I = Matrices.GetIdentity(
				n);

			//decimal[,] householder = Matrices.Subtract(
			//	I,
			//	Matrices.Multiply(
			//		2,
			//		Matrices.Divide(
			//			Matrices.Multiply(
			//				a1,
			//				a1T),
			//			Matrices.Multiply(
			//				a1T,
			//				a1))));

			// this gets the "right" answer compared to one online example for a Householder transform
			// but I'm not sure it's "correct"

			decimal[,] householder = Matrices.Subtract(
				I,
				Matrices.Multiply(
					2 / sumA1,
					Matrices.Multiply(
						a1,
						a1T)));

			return householder;
		}
	}
}

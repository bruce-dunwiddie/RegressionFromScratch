using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scratch.Regression
{
	public static class Matrices
	{
		public static Matrix Subtract(
			Matrix left,
			Matrix right)
		{
			int height = left.Height;
			int width = left.Width;

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

			return new Matrix(result);
		}

		public static Matrix Multiply(
			Matrix left,
			Matrix right)
		{
			// https://www.mathsisfun.com/algebra/matrix-multiplying.html

			// number of columns in left matrix must match number of rows in right matrix

			int resultHeight = left.Height;
			int resultWidth = right.Width;

			decimal[,] result = new decimal[resultHeight, resultWidth];

			for (int rowIndex = 0; rowIndex < resultHeight; rowIndex++)
			{
				for (int columnIndex = 0; columnIndex < resultWidth; columnIndex++)
				{
					decimal sum = 0;

					for (int leftColumnIndex = 0; leftColumnIndex < left.Width; leftColumnIndex++)
					{
						sum +=
							left[rowIndex, leftColumnIndex] *
							right[leftColumnIndex, columnIndex];
					}

					result[rowIndex, columnIndex] = sum;
				}
			}

			return new Matrix(result);
		}

		public static Matrix Multiply(
			decimal left,
			Matrix right)
		{
			int height = right.Height;
			int width = right.Width;

			decimal[,] result = new decimal[height, width];

			for (int rowIndex = 0; rowIndex < height; rowIndex++)
			{
				for (int columnIndex = 0; columnIndex < width; columnIndex++)
				{
					result[rowIndex, columnIndex] = right[rowIndex, columnIndex] * left;
				}
			}

			return new Matrix(result);
		}

		public static Matrix Divide(
			Matrix left,
			decimal right)
		{
			int height = left.Height;
			int width = left.Width;

			decimal[,] result = new decimal[height, width];

			for (int rowIndex = 0; rowIndex < height; rowIndex++)
			{
				for (int columnIndex = 0; columnIndex < width; columnIndex++)
				{
					result[rowIndex, columnIndex] = left[rowIndex, columnIndex] / right;
				}
			}

			return new Matrix(result);
		}

		public static Matrix GetIdentity(
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

			return new Matrix(identity);
		}
	}
}

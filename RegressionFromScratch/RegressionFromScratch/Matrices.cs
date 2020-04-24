using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scratch.Regression
{
	public static class Matrices
	{
		public static decimal[,] SliceVertical(
			decimal[,] matrix, 
			int columnIndex,
			int startingRowIndex,
			int length)
		{
			decimal[,] vector = new decimal[length, 1];

			for (int rowIndex = 0; rowIndex < length; rowIndex++)
			{
				vector[rowIndex, 0] = matrix[startingRowIndex + rowIndex, columnIndex];
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

		public static decimal [,] Multiply(
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
	}
}

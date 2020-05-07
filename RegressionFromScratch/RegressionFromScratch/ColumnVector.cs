using System;
using System.Collections.Generic;
using System.Text;

namespace Scratch.Regression
{
	public class ColumnVector
	{
		private Matrix matrix;
		private int columnIndex;
		private int startingRowIndex;

		public ColumnVector(
			Matrix matrix,
			int columnIndex,
			int startingRowIndex,
			int height)
		{
			this.matrix = matrix;
			this.columnIndex = columnIndex;
			this.startingRowIndex = startingRowIndex;
			Height = height;
		}

		public int Height { get; }

		public decimal this[int index]
		{
			get
			{
				return matrix[startingRowIndex + index, columnIndex];
			}
		}

		public ColumnVector GetSubVector(
			int startingRowIndex,
			int height)
		{
			return new ColumnVector(
				matrix,
				columnIndex,
				startingRowIndex + this.startingRowIndex,
				height);
		}

		public decimal GetNorm()
		{
			decimal sum = 0;

			for (int rowIndex = 0; rowIndex < Height; rowIndex++)
			{
				sum += Formulas.Square(this[rowIndex]);
			}

			return Formulas.Sqrt(sum);
		}

		public Matrix AsMatrix()
		{
			decimal[,] vector = new decimal[Height, 1];

			for (int rowIndex = 0; rowIndex < Height; rowIndex++)
			{
				vector[rowIndex, 0] = this[rowIndex];
			}

			return new Matrix(vector);
		}
	}
}

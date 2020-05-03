using System;
using System.Collections.Generic;
using System.Text;

namespace Scratch.Regression
{
	public class Matrix
	{
		private decimal[,] matrix;

		public Matrix(
			decimal[,] matrix)
		{
			this.matrix = matrix;

			Height = matrix.GetLength(0);
			Width = matrix.GetLength(1);
		}

		public decimal[,] Array
		{
			get
			{
				return matrix;
			}
		}

		public virtual int Height { get; }

		public virtual int Width { get; }

		public virtual decimal this[int row, int column]
		{
			get
			{
				return matrix[row, column];
			}

			set
			{
				matrix[row, column] = value;
			}
		}

		public ColumnVector GetColumnVector(
			int columnIndex,
			int startingRowIndex,
			int height)
		{
			return new ColumnVector(
				this,
				columnIndex,
				startingRowIndex,
				height);
		}

		public Matrix GetMinor(
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
						this[startingRowIndex + rowIndex, startingColumnIndex + columnIndex];
				}
			}

			return new Matrix(minor);
		}

		public Matrix GetTranspose()
		{
			// https://en.wikipedia.org/wiki/Transpose

			// the transpose of a matrix is an operator which flips a matrix over its diagonal,
			// that is it switches the row and column indices of the matrix.

			decimal[,] transpose = new decimal[Width, Height];

			for (int rowIndex = 0; rowIndex < Height; rowIndex++)
			{
				for (int columnIndex = 0; columnIndex < Width; columnIndex++)
				{
					transpose[columnIndex, rowIndex] = this[rowIndex, columnIndex];
				}
			}

			return new Matrix(transpose);
		}

		public Matrix GetSum()
		{
			decimal[,] result = new decimal[1, Width];

			for (int columnIndex = 0; columnIndex < Width; columnIndex++)
			{
				decimal sum = 0;

				for (int rowIndex = 0; rowIndex < Height; rowIndex++)
				{
					sum += this[rowIndex, columnIndex];
				}

				result[0, columnIndex] = sum;
			}

			return new Matrix(result);
		}

		public Matrix GetInverse()
		{
			// using Adjoint method

			// https://www.mathwords.com/i/inverse_of_a_matrix.htm

			return Matrices.Divide(
				GetAdjugate(),
				GetDeterminant());
		}

		public Matrix GetAdjugate()
		{
			// https://www.mathwords.com/a/adjoint.htm

			// The matrix formed by taking the transpose of the cofactor matrix of a given original matrix.

			return GetCofactor()
				.GetTranspose();
		}

		public Matrix GetCofactor()
		{
			// https://www.mathwords.com/c/cofactor_matrix.htm

			decimal[,] cofactor = new decimal[Height, Width];

			for (int rowIndex = 0; rowIndex < Height; rowIndex++)
			{
				for (int columnIndex = 0; columnIndex < Width; columnIndex++)
				{
					cofactor[rowIndex, columnIndex] = new Cofactor(
						this,
						rowIndex,
						columnIndex).GetDeterminant();
				}
			}

			return new Matrix(cofactor);
		}

		public virtual decimal GetDeterminant()
		{
			// https://www.mathsisfun.com/algebra/matrix-determinant.html

			// you can only get a determinant from a square matrix

			// using expansion by cofactors
			// https://www.mathwords.com/e/expansion_by_cofactors.htm

			return GetDeterminant(
				this);
		}

		private static decimal GetDeterminant(
			Matrix matrix)
		{
			// look for 2x2 matrix
			if (matrix.Height == 2)
			{
				return GetDeterminant(
					matrix[0, 0],
					matrix[1, 0],
					matrix[0, 1],
					matrix[1, 1]);
			}
			else
			{
				decimal determinant = 0;

				for (int columnIndex = 0; columnIndex < matrix.Width; columnIndex++)
				{
					Cofactor cofactor = new Cofactor(
						matrix,
						0,
						columnIndex);

					determinant +=
						matrix[columnIndex, 0] *
						GetDeterminant(
							cofactor);
				}

				return determinant;
			}
		}

		private static decimal GetDeterminant(
			decimal a11,
			decimal a21,
			decimal a12,
			decimal a22)
		{
			// [ a11, a12 ]
			// [ a21, a22 ]

			return a11 * a22 - a12 * a21;
		}

		public Matrix GetHouseholderMatrix()
		{
			// creating a new alias to match commonly used algorithm variables
			Matrix A = this;

			int m = A.Height;
			int n = A.Width;

			Matrix a1 = A.GetColumnVector(
				columnIndex: 0,
				startingRowIndex: 0,
				height: A.Height).AsMatrix();

			decimal sumA1 = a1.GetSum()[0, 0];

			a1[0, 0] -= sumA1;

			a1 = Matrices.Divide(
				a1,
				2);

			Matrix a1T = a1.GetTranspose();

			Matrix I = Matrices.GetIdentity(
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

			Matrix householder = Matrices.Subtract(
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

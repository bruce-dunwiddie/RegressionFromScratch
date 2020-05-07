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

		public void MultiplyBy(
			decimal value)
		{
			for (int rowIndex = 0; rowIndex < Height; rowIndex++)
			{
				for (int columnIndex = 0; columnIndex < Width; columnIndex++)
				{
					this[rowIndex, columnIndex] *= value;
				}
			}
		}

		public void DivideBy(
			decimal value)
		{
			for (int rowIndex = 0; rowIndex < Height; rowIndex++)
			{
				for (int columnIndex = 0; columnIndex < Width; columnIndex++)
				{
					this[rowIndex, columnIndex] /= value;
				}
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

			return GetDeterminant(
				this);
		}

		private static decimal GetDeterminant(
			Matrix matrix)
		{
			// you can only get a determinant from a square matrix

			// using expansion by cofactors
			// https://www.mathwords.com/e/expansion_by_cofactors.htm

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
						matrix[0, columnIndex] *
						cofactor.GetDeterminant();
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
			// https://en.wikipedia.org/wiki/QR_decomposition#Using_Householder_reflections

			// creating a new alias to match commonly used algorithm variables
			Matrix A = this;

			// don't alter a1 or it will alter A
			ColumnVector a1 = A.GetColumnVector(
				0,
				0,
				A.Height);

			decimal norm = a1.GetNorm();

			// create copy of vector that can be edited
			Matrix u = a1.AsMatrix();

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

			return H;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Scratch.Regression
{
	/// <summary>
	///		The determinant obtained by deleting the row and column of a given element of a matrix or determinant.
	///		The cofactor is preceded by a + or – sign depending whether the element is in a + or – position.
	/// </summary>
	public class Cofactor : Matrix
	{
		private int deletedRow;
		private int deletedColumn;

		public Cofactor(
			Matrix matrix,
			int row,
			int column) :
			base(
				matrix.Array)
		{
			deletedRow = row;
			deletedColumn = column;
		}

		public override int Height => base.Height - 1;

		public override int Width => base.Width - 1;

		public override decimal this[int row, int column]
		{
			get
			{
				int adjustedRow = row;
				int adjustedColumn = column;

				if (adjustedRow >= deletedRow)
				{
					adjustedRow--;
				}

				if (adjustedColumn >= deletedColumn)
				{
					adjustedColumn--;
				}

				return base[adjustedRow, adjustedColumn];
			}

			set
			{
				// probably never used, but overriding it for good measure

				int adjustedRow = row;
				int adjustedColumn = column;

				if (adjustedRow >= deletedRow)
				{
					adjustedRow--;
				}

				if (adjustedColumn >= deletedColumn)
				{
					adjustedColumn--;
				}

				base[adjustedRow, adjustedColumn] = value;
			}
		}

		public override decimal GetDeterminant()
		{
			// https://www.mathwords.com/c/cofactor.htm

			decimal sign = 1;

			if ((deletedRow + deletedColumn) % 2 == 1)
			{
				sign = -1;
			}

			return sign * base.GetDeterminant();
		}
	}
}

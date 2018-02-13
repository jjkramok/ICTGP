using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CG
{
	class Matrix
	{
		public float[,] Values;
		public int Rows { get; private set; }
		public int Columns { get; private set; }

		public Matrix(int squareSize)
		{
			Values = new float[squareSize, squareSize];
			Rows = squareSize;
			Columns = squareSize;
		}

		public Matrix(int rows, int columns)
		{
			Values = new float[rows, columns];
			Rows = rows;
			Columns = columns;
		}

		public Matrix(int rows, int columns, float[] values)
		{
			Values = new float[rows, columns];
			Rows = rows;
			Columns = columns;

			int index = 0;
			for(int row = 0; row < Rows; row++)
			{
				for(int col = 0; col < Columns; col++)
				{
					if (index >= values.Length)
					{
						return;
					}
					Values[row, col] = values[index];
					index++;
				}
			}
		}

		public static Matrix operator +(Matrix m1, Matrix m2)
		{
			if (m1.Rows != m2.Rows || m1.Columns != m2.Columns)
			{
				throw new Exception("Matrixes are not the same size");
			}
			var result = new Matrix(m1.Rows, m1.Columns);
			for (int row = 0; row < m1.Rows; row++)
			{
				for (int col = 0; col < m1.Columns; col++)
				{
					result.Values[row, col] = m1.Values[row, col] + m2.Values[row, col];
				}
			}

			return result;
		}

		public static Matrix operator -(Matrix m1, Matrix m2)
		{
			if (m1.Rows != m2.Rows || m1.Columns != m2.Columns)
			{
				throw new Exception("Matrixes are not the same size");
			}
			var result = new Matrix(m1.Rows, m1.Columns);
			for (int row = 0; row < m1.Rows; row++)
			{
				for (int col = 0; col < m1.Columns; col++)
				{
					result.Values[row, col] = m1.Values[row, col] - m2.Values[row, col];
				}
			}

			return result;
		}

		public static Matrix operator *(float value, Matrix matrix)
		{
			var result = new Matrix(matrix.Rows, matrix.Columns);
			for (int row = 0; row < matrix.Rows; row++)
			{
				for (int col = 0; col < matrix.Columns; col++)
				{
					result.Values[row, col] = matrix.Values[row, col] * value;
				}
			}

			return result;
		}

		public static Matrix operator *(Matrix matrix, float value)
		{
			return value * matrix;
		}

		public static Matrix operator *(Matrix m1, Matrix m2)
		{
			if (m1.Columns != m2.Rows)
			{
				throw new Exception("Matrix sizes do not match.");
			}
			var result = new Matrix(m1.Rows, m2.Columns);

			for (int row = 0; row < m1.Rows; row++)
			{
				for (int col = 0; col < m2.Columns; col++)
				{
					float value = 0f;
					for (int i = 0; i < m1.Columns; i++)
					{
						value += m1.Values[row, i] * m2.Values[i, col];
					}
					result.Values[row, col] = value;
				}
			}

			return result;

		}
		public Vector ColumnToVector(int column)
		{
			if (column >= Columns)
			{
				throw new Exception("Column not available");
			}
			var vector = new Vector(Rows);
			for (int i = 0; i < Rows; i++)
			{
				vector.Values[i] = Values[i, column];
			}
			return vector;
		}

		public Vector RowToVector(int row)
		{
			if (row >= Rows)
			{
				throw new Exception("Row not available");
			}
			var vector = new Vector(Columns);
			for (int i = 0; i < Columns; i++)
			{
				vector.Values[i] = Values[row, i];
			}
			return vector;
		}

		public static Matrix IdentityMatrix(int size)
		{
			var result = new Matrix(size);
			for (int i = 0; i < size; i++)
			{
				result.Values[i, i] = 1;
			}
			return result;
		}

		public override string ToString()
		{
			string result = $"Matrix size: {Rows} x {Columns}\n";
			for (int row = 0; row < Rows; row++)
			{
				for (int col = 0; col < Columns; col++)
				{
					result += $"{Values[row, col]}\t";
				}
				result += "\n";
			}
			return result;
		}
	}
}

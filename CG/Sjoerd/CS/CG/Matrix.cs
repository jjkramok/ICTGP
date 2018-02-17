using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CG
{
	public class Matrix
	{
		public float[,] Values;
		public int Rows { get; private set; }
		public int Columns { get; private set; }
		#region constructors
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
			for (int row = 0; row < Rows; row++)
			{
				for (int col = 0; col < Columns; col++)
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
		#endregion

        public static Matrix Translate(Vector t)
        {
            Matrix idMatrix = IdentityMatrix(t.Count);
            // Add the translation vector to the last column of the matrix
            for (int row = 0; row < t.Count - 1; row++)
            {
                idMatrix.Values[row, t.Count - 1] = t.Values[row];
            }
            return idMatrix;
        }
		#region operators

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

		public static Vector operator *(Matrix m, Vector v)
		{
			if (m.Columns != v.Count)
			{
				throw new Exception("Matrix and vector size do not match.");
			}
			var result = new Vector(v.Count);

			for (int row = 0; row < m.Rows; row++)
			{
				float value = 0f;
				for (int i = 0; i < m.Columns; i++)
				{
					value += m.Values[row, i] * v.Values[i];
				}
				result.Values[row] = value;
			}
			return result;
		}

		public static Vector operator *(Vector v, Matrix m)
		{
			return m * v;
		}
		#endregion

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
		#region DefaultMatrices
		public static Matrix IdentityMatrix(int size)
		{
			var result = new Matrix(size);
			for (int i = 0; i < size; i++)
			{
				result.Values[i, i] = 1;
			}
			return result;
		}

		public static Matrix ScalingMatrix(int size, float scale)
		{
			var matrix = new Matrix(size);
			for (int i = 0; i < size - 1; i++)
			{
				matrix.Values[i, i] = scale;
			}
			matrix.Values[size - 1, size - 1] = 1;
			return matrix;
		}
		public static Matrix ScalingMatrix(float scaleX, float scaleY)
		{
			var matrix = new Matrix(3);

			matrix.Values[0, 0] = scaleX;
			matrix.Values[1, 1] = scaleY;
			matrix.Values[2, 2] = 1;

			return matrix;
		}
		public static Matrix ScalingMatrix(float scaleX, float scaleY, float scaleZ)
		{
			var matrix = new Matrix(4);

			matrix.Values[0, 0] = scaleX;
			matrix.Values[1, 1] = scaleY;
			matrix.Values[2, 2] = scaleZ;
			matrix.Values[3, 3] = 1;

			return matrix;
		}
		// todo: implement these
		public static Matrix RotationMatrix2D(float amount)
		{
			var matrix = IdentityMatrix(3);

			return matrix;
		}

		public static Matrix RotationMatrix3D()
		{
			return null;
		}

		public static Matrix TranslationMatrix2D(float x, float y)
		{
			var matrix = IdentityMatrix(3);
			matrix.Values[0, 2] = x;
			matrix.Values[1, 2] = y;
			return matrix;
		}

		public static Matrix TranslationMatrix3D(float x, float y, float z)
		{
			var matrix = IdentityMatrix(4);
			matrix.Values[0, 3] = x;
			matrix.Values[1, 3] = y;
			matrix.Values[2, 3] = z;
			return matrix;
		}
		#endregion
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

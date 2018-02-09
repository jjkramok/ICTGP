using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CG
{
	class Vector
	{
		public float[] Values;
		public int Count { get { return Values.Length; } }

		public Vector(int size)
		{
			Values = new float[size];
		}

		public Vector(float[] values)
		{
			Values = values;
		}

		public static Vector operator +(Vector v1, Vector v2)
		{
			var newVector = new Vector(v1.Count);
			for (int i = 0; i < v1.Count; i++)
			{
				newVector.Values[i] = v1.Values[i] + v2.Values[i];
			}
			return newVector;
		}

		public static Vector operator *(Vector vector, float value)
		{
			var newVector = new Vector(vector.Count);
			for (int i = 0; i < vector.Count; i++)
			{
				newVector.Values[i] = vector.Values[i] * value;
			}
			return newVector;
		}

		public override string ToString()
		{
			// todo fix
			return $"X:";
		}
	}
}

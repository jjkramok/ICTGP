using CG.G2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CG
{
	class Program
	{
		static void Main(string[] args)
		{
			Test2D();
			TestVector();
			Console.WriteLine();
			TestMatrix();
			Console.ReadKey();
		}

		static void TestVector()
		{
			Vector v1 = new Vector(new float[] { 1, 2, 3 });
			Vector v2 = new Vector(new float[] { 4, 5, 6 });

			Console.WriteLine("1,2,3 + 4,5,6=");
			Console.WriteLine(v1 + v2);

			Console.WriteLine("1,2,3 * 4=");
			Console.WriteLine(v1 * 4);
		}

		static void TestMatrix()
		{
			Matrix mId = Matrix.IdentityMatrix(3);
			Console.WriteLine("identity: 3");
			Console.WriteLine(mId);

			Matrix m1 = new Matrix(1, 2, new float[] { 4, -1 });
			Matrix m2 = new Matrix(2, 2, new float[] { 2, 4, -1, 3 });
			Console.WriteLine("multiply matrix");
			Console.WriteLine(m1 * m2);

		}

		static void Test2D()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form2D());
		}
	}
}

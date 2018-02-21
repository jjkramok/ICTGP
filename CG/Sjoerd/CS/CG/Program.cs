using CG.G2D;
using CG.G3D;
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
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			Test3D();
			Test2D();

			//TestVector();
			//Console.WriteLine();
			//TestMatrix();

			Application.Run();
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

			Vector TwoDTranslationVector = new Vector(new float[] { 4, -1, 1 });
			Vector ThreeDTranslationVector = new Vector(new float[] { 4, -1, 8, 1 });
			Matrix tm1 = Matrix.Translate(TwoDTranslationVector);
			Matrix tm2 = Matrix.Translate(ThreeDTranslationVector);
			Console.WriteLine("Translation matrix based on 2d vector");
			Console.WriteLine(TwoDTranslationVector);
			Console.WriteLine(tm1);
			Console.WriteLine("Translation matrix based on 3d vector");
			Console.WriteLine(ThreeDTranslationVector);
			Console.WriteLine(tm2);
		}

		static void Test2D()
		{
			Form form = new Form2D();
			form.Show();
		}
		static void Test3D()
		{
			Form form = new Form3D();
			form.Show();
		}
	}
}

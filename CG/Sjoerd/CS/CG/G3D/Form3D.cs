using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CG.G3D
{
	public partial class Form3D : Form
	{
		private Cube cube;

		private int animationPhase = 0;
		private float animationStatus = 0f;

		private float d;
		private float r;
		private float theta;
		private float phi;

		private Matrix rotation = Matrix.IdentityMatrix(4);
		private Matrix translation = Matrix.IdentityMatrix(4);
		private Matrix scaling = Matrix.IdentityMatrix(4);

		private List<Vector> helperLineVerteces;

		public Form3D()
		{
			cube = new Cube(Color.Green);
			var matrix = Matrix.ScalingMatrix3D(0.1f);
			ApplyMatrixToCube(matrix);

			helperLineVerteces = new List<Vector>
			{
				new Vector(new float[] { 0, 0, 0, 1}),
				new Vector(new float[] { 0.2f, 0, 0, 1}),
				new Vector(new float[] { 0, 0.2f, 0, 1}),
				new Vector(new float[] { 0, 0, 0.2f, 1}),
			};


			ResetCube();

			InitializeComponent();
		}

		private void UpdateInfoLabel()
		{
			var rotateX = Math.Round(Math.Atan2(rotation.Values[2, 1], rotation.Values[2, 2]) * 180 / Math.PI);
			var rotateY = Math.Round(Math.Atan2(-rotation.Values[2, 0], Math.Sqrt(Math.Pow(rotation.Values[2, 1], 2) + Math.Pow(rotation.Values[2, 2], 2))) * 180 / Math.PI);
			var rotateZ = Math.Round(Math.Atan2(rotation.Values[1, 0], rotation.Values[0, 0]) * 180 / Math.PI);


			//Console.WriteLine(rotation);
			string text =
				$"Scale: {Math.Round(scaling.Values[0, 0], 2)}\r\n" +
				$"Translate: ({Math.Round(translation.Values[0, 3], 2)}, {Math.Round(translation.Values[1, 3], 2)}, {Math.Round(translation.Values[2, 3], 2)})\r\n" +
				$"RotateX: {rotateX}\r\n" +
				$"RotateY: {rotateY}\r\n" +
				$"RotateZ: {rotateZ}\r\n" +
				$"\r\n" +
				$"r: {Math.Round(r, 1)}\r\n" +
				$"d: {Math.Round(d, 1)}\r\n" +
				$"phi: {Math.Round(phi * 180 / Math.PI)}\r\n" +
				$"theta{Math.Round(theta * 180 / Math.PI)}\r\n" +
				$"\r\n" +
				$"animation phase: {animationPhase}";


			infoLabel.Text = text;
		}

		private void drawPanel_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			UpdateScreen(g);
		}

		private void UpdateScreen(Graphics g)
		{
			// translate origin to center
			g.TranslateTransform(drawPanel.Width / 2, drawPanel.Height / 2);

			var viewMatrix = Matrix.ViewMatrix3D(theta, phi, r);

			// draw cube
			var drawableCube = new List<Vector>();
			foreach (var original in cube.vertexbuffer)
			{
				var changed = original * rotation * translation * scaling * viewMatrix;
				var translateMatrices = Matrix.ProjectionMatrix(d, changed.Values[3]);
				drawableCube.Add(changed * translateMatrices);
			}
			cube.Draw(g, drawableCube);

			var dh = new List<Vector>();
			foreach (var original in helperLineVerteces)
			{
				var changed = original * viewMatrix;
				var translateMatrices = Matrix.ProjectionMatrix(d, changed.Values[3]);
				dh.Add(changed * translateMatrices);
			}
			// draw helper lines
			Pen penX = new Pen(Color.Red, 3f);
			Pen penY = new Pen(Color.Green, 3f);
			Pen penZ = new Pen(Color.Blue, 3f);
			g.DrawLine(penX, dh[0].Values[0], dh[0].Values[1], dh[1].Values[0], dh[1].Values[1]);
			g.DrawLine(penY, dh[0].Values[0], dh[0].Values[1], dh[2].Values[0], dh[2].Values[1]);
			g.DrawLine(penZ, dh[0].Values[0], dh[0].Values[1], dh[3].Values[0], dh[3].Values[1]); 

			UpdateInfoLabel();
		}

		private void Form3D_KeyDown(object sender, KeyEventArgs e)
		{
			bool inverse = e.Shift;
			switch (e.KeyCode)
			{
				case Keys.Up: // change x/z
					translation *= Matrix.TranslationMatrix3D(0f, 0f, -0.01f);
					break;
				case Keys.Down: // change x/z
					translation *= Matrix.TranslationMatrix3D(0f, 0f, 0.01f);
					break;
				case Keys.Left: // change x/z
					translation *= Matrix.TranslationMatrix3D(0.01f, 0f, 0f);
					break;
				case Keys.Right: // change x/z
					translation *= Matrix.TranslationMatrix3D(-0.01f, 0f, 0f);
					break;
				case Keys.PageUp: // change y
					translation *= Matrix.TranslationMatrix3D(0f, 0.01f, 0f);
					break;
				case Keys.PageDown: // change y
					translation *= Matrix.TranslationMatrix3D(0f, -0.01f, 0f);
					break;
				case Keys.X: // rotate X
					if (inverse)
						rotation *= Matrix.RotationMatrix3Dx(-(float) Math.PI / 180f);
					else
						rotation *= Matrix.RotationMatrix3Dx((float) Math.PI / 180f);
					break;
				case Keys.Y: // rotate Y
					if (inverse)
						rotation *= Matrix.RotationMatrix3Dy(-(float) Math.PI / 180f);
					else
						rotation *= Matrix.RotationMatrix3Dy((float) Math.PI / 180f);
					break;
				case Keys.Z: // rotate Z
					if (inverse)
						rotation *= Matrix.RotationMatrix3Dz(-(float) Math.PI / 180f);
					else
						rotation *= Matrix.RotationMatrix3Dz((float) Math.PI / 180f);
					break;
				case Keys.S: // scale
					if (inverse)
						scaling *= Matrix.ScalingMatrix3D(0.99f);
					else
						scaling *= Matrix.ScalingMatrix3D(1.01f);
					break;
				case Keys.A: // animate
					AnimationStart();
					break;
				case Keys.C: // reset
					ResetCube();
					break;
				case Keys.N:
					if (inverse)
						theta -= 0.01f;
					else
						theta += 0.01f;
					break;
				case Keys.M:
					if (inverse)
						phi -= 0.01f;
					else
						phi += 0.01f;
					break;

			}
			// redraw
			drawPanel.Refresh();
		}

		private void ResetCube()
		{
			d = 800f;
			r = 10f;
			phi = (float)Math.PI * -0.5f;
			theta = (float)Math.PI * -0.5f;

			rotation = Matrix.IdentityMatrix(4);
			translation = Matrix.IdentityMatrix(4);
			scaling = Matrix.IdentityMatrix(4);
		}

		private void AnimationStart()
		{
			animationPhase = 1;
			animationStatus = 0f;
			while (animationPhase != 0)
			{
				Animate();
				Thread.Sleep(50);
			}
		}

		private void Animate()
		{
			switch (animationPhase)
			{
				case 1:
					// 1*1.01 ^ 40 = 1.503 
					if (animationStatus < 0.4f)
						scaling *= Matrix.ScalingMatrix3D(1.01f);
					else
						scaling *= Matrix.ScalingMatrix3D(0.99f);
					theta -= (float) Math.PI / 180f;
					animationStatus += 0.01f;

					if (animationStatus >= 0.8f)
					{
						animationStatus = 0f;
						animationPhase = 2;
					}
					break;
				case 2:
					if (animationStatus < 45)
						rotation *= Matrix.RotationMatrix3Dx((float) Math.PI / 180);
					else
						rotation *= Matrix.RotationMatrix3Dx(-(float) Math.PI / 180);
					theta -= (float) Math.PI / 180f;
					animationStatus += 1f;
					if (animationStatus >= 90f)
					{
						animationStatus = 0f;
						animationPhase = 3;
					}
					break;
				case 3:
					if (animationStatus < 45)
						rotation *= Matrix.RotationMatrix3Dy((float) Math.PI / 180);
					else
						rotation *= Matrix.RotationMatrix3Dy(-(float) Math.PI / 180);
					phi += (float) Math.PI / 180f;
					animationStatus += 1f;
					if (animationStatus >= 90f)
					{
						animationStatus = 0f;
						animationPhase = 4;
					}
					break;
				case 4:
					if (phi > (float) Math.PI * -0.5f)
						phi -= (float) Math.PI / 180f;

					if (theta < (float) Math.PI * -0.5f)
						theta += (float) Math.PI / 180f;

					if (theta >= (float) Math.PI * -0.5f && phi <= (float) Math.PI * -0.5f)
						animationPhase = 0;
					break;
				default:
					break;
			}
			drawPanel.Refresh();
		}

		private void ApplyMatrixToCube(Matrix matrix)
		{
			for (int i = 0; i < cube.vertexbuffer.Count; i++)
			{
				cube.vertexbuffer[i] *= matrix;
			}
		}

		private void Form3D_Resize(object sender, EventArgs e)
		{
			drawPanel.Update();
		}
	}
}
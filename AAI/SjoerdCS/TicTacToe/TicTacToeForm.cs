using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SjoerdCS.TicTacToe
{
	public partial class TicTacToeForm : Form
	{
		TicTacToe ttt = new TicTacToe();
		Random random = new Random();
		TicTacToe.Marble turn = TicTacToe.Marble.CROSS;
		int p1 = 0;


		public TicTacToeForm()
		{
			InitializeComponent();
			InitGame();
		}

		public void InitGame()
		{
			ttt = new TicTacToe();
			turn = TicTacToe.Marble.CROSS;
			TurnLabel.Text = "Turn:";
			GameStateLabel.Text = "Place new marble";
			this.BackColor = Color.White;
			Invalidate(true);
		}

		private void pictureBox1_Paint(object sender, PaintEventArgs e)
		{
			Pen pen = Pens.Black;
			Brush brushCross = Brushes.Red;
			Brush brushZero = Brushes.Blue;
			Graphics g = e.Graphics;

			int dim = 100;
			for (int x = 0; x < 3; x++)
				for (int y = 0; y < 3; y++)
				{
					g.DrawRectangle(pen, x * dim, y * dim, dim, dim);
					switch (ttt.Get(y * 3 + x + 1))
					{
						case TicTacToe.Marble.EMPTY:
							break;
						case TicTacToe.Marble.CROSS:
							g.FillEllipse(brushCross, x * dim + 20, y * dim + 20, dim - 40, dim - 40);
							break;
						case TicTacToe.Marble.ZERO:
							g.FillEllipse(brushZero, x * dim + 20, y * dim + 20, dim - 40, dim - 40);
							break;
					}
				}
		}


		private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
		{
			int p = PixelToPosition(e.X, e.Y);

			if (ttt.IsEmpty(p) && p1 == 0)
			{
				if (ttt.DoMove(turn, p))
					testGameOver();
				this.Invalidate(true);
				return;
			}

			if (ttt.IsEmpty(p) && p1 != 0)
			{
				if (ttt.DoMove(p1, p))
					testGameOver();
				p1 = 0;
				this.Invalidate(true);
				return;
			}

			p1 = p;
		}

		int PixelToPosition(int x, int y)
		{
			int px = x / 100;
			int py = y / 100;
			return py * 3 + px + 1;
		}

		private void testGameOver()
		{
			if (ttt.IsWinner(turn))
			{
				GameStateLabel.Text = "Game Over";
				TurnLabel.Text = "Winner:";
				this.BackColor = Color.Yellow;
				return;
			}

			if (ttt.InInsertingState())
				GameStateLabel.Text = "Place new marble";
			else
				GameStateLabel.Text = "Move a marble to an empty place";

			turn = ttt.Reverse(turn);
		}

		private void pictureBox2_Paint(object sender, PaintEventArgs e)
		{

			Pen pen = Pens.Black;
			Brush brushCross = Brushes.Red;
			Brush brushZero = Brushes.Blue;
			Graphics g = e.Graphics;

			int dim = 100;
			g.DrawRectangle(pen, 0, 0, dim, dim);
			switch (turn)
			{
				case TicTacToe.Marble.EMPTY:
					break;
				case TicTacToe.Marble.CROSS:
					g.FillEllipse(brushCross, 20, 20, dim - 40, dim - 40);
					break;
				case TicTacToe.Marble.ZERO:
					g.FillEllipse(brushZero, 20, 20, dim - 40, dim - 40);
					break;
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			InitGame();
		}

		private void ComputerMoveButton_Click(object sender, EventArgs e)
		{
			if (ttt.FindAndDoBestComputerMove(turn))
			{
				testGameOver();
				Invalidate(true);
			}
			else
			{
				MessageBox.Show("Something went wrong");
			}
		}
	}
}

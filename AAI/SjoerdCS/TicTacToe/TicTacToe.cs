using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SjoerdCS.TicTacToe
{
	class TicTacToe
	{
		public enum Marble { EMPTY, CROSS, ZERO };

		/**
         * 1 2 3
         * 4 5 6
         * 7 8 9
         * */

		int[] history;
		Marble[] board;
		int count;          //  number of marbles on the board

		public TicTacToe()
		{
			history = new int[9];
			board = new Marble[10];
			count = 0;
		}

		public bool InInsertingState()
		{
			return count < 9;
		}

		public bool DoMove(Marble marble, int position)
		{
			if (count < 9)
			{
				board[position] = marble;
				history[count] = position;
				count++;
				return true;
			}

			return false;
		}

		public bool DoMove(int positionFrom, int positionTo)
		{
			board[positionTo] = board[positionFrom];
			board[positionFrom] = Marble.EMPTY;
			return true;
		}

		public void UndoMove()
		{
			if (count == 0)
			{
				return;
			}
			count--;
			board[history[count]] = Marble.EMPTY;
		}

		public bool IsEmpty(int position)
		{
			return board[position] == Marble.EMPTY;
		}

		public Marble Get(int position)
		{
			return board[position];
		}

		public Marble Reverse(Marble marble)
		{
			switch (marble)
			{
				case Marble.CROSS:
					return Marble.ZERO;
				case Marble.ZERO:
					return Marble.CROSS;
				default:
					return marble;
			}
		}

		public bool IsWinner(Marble m)
		{
			if (B(m, 1) && B(m, 2) && B(m, 3))
				return true;
			if (B(m, 4) && B(m, 5) && B(m, 6))
				return true;
			if (B(m, 7) && B(m, 8) && B(m, 9))
				return true;

			if (B(m, 1) && B(m, 4) && B(m, 7))
				return true;
			if (B(m, 2) && B(m, 5) && B(m, 8))
				return true;
			if (B(m, 3) && B(m, 6) && B(m, 9))
				return true;

			if (B(m, 1) && B(m, 5) && B(m, 9))
				return true;
			if (B(m, 3) && B(m, 5) && B(m, 7))
				return true;

			return false;
		}

		private bool B(Marble marble, int position)
		{
			return board[position] == marble;
		}



		/***
         * methods to be implemented for finding the best move
         * */

		public bool FindAndDoBestComputerMove(Marble m)
		{
			int bestMoveValue = -100;
			int bestMove = 0;
			for (int i = 1; i <= 9; i++)
			{
				if (IsEmpty(i))
				{
					DoMove(m, i);
					int value = ComputerMove(Reverse(m), m, false, 0);
					Console.WriteLine(value);
					if (value > bestMoveValue)
					{
						bestMove = i;
						bestMoveValue = value;
					}
					UndoMove();
				}
			}
			if (bestMove == 0)
			{
				return false;
			}

			board[bestMove] = m;

			return true;
		}

		private int ComputerMove(Marble player, Marble originalPlayer, bool maximize, int depth)
		{
			if (IsWinner(originalPlayer))
			{
				return 1;
			}
			if (IsWinner(Reverse(originalPlayer)))
			{
				return -1;
			}
			if (count == 9)
			{
				return 0;
			}
			int bestValue = maximize ? -100 : 100;
			for (int i = 1; i <= 9; i++)
			{
				if (IsEmpty(i))
				{
					DoMove(player, i);
					int value = ComputerMove(Reverse(player), originalPlayer, !maximize, depth + 1);
					if (bestValue < value == maximize)
					{
						bestValue = value;
					}
					UndoMove();
				}
			}
			return bestValue;
		}

		public string Str { get { return ToString(); } }

		public override string ToString()
		{
			var result = "";
			for (int i = 1; i <= 9; i++)
			{
				if (i % 3 == 1)
				{
					result += "\n";
				}
				switch (board[i])
				{
					case Marble.CROSS:
						result += "X";
						break;
					case Marble.ZERO:
						result += "O";
						break;
					case Marble.EMPTY:
						result += ".";
						break;
				}
			}
			return result.Trim();
		}
	}
}

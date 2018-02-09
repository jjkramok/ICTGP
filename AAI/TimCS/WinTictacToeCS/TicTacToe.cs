using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinTicTacToeCS
{
    public class TicTacToe
    {
        public enum Marble { EMPTY, CROSS, ZERO };

        /**
         * 1 2 3
         * 4 5 6
         * 7 8 9
         * */

        public Marble[] board;
        int count;          //  number of marbles on the board

        public TicTacToe()
        {
            board = new Marble[10];
            count = 0;
        }

        public bool InInsertingState()
        {
            return count < 6;
        }

        public bool DoMove(Marble marble, int position)
        {
            if (count < 6)
            {
                board[position] = marble;
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

        public bool UndoMove()
        {
            return true;            //  to be implemented   //
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

        public List<Move> GetPossibleMoves(Marble player)
        {
            List<Move> res = new List<Move>();
            for (int i = 1; i < board.Length; i++)
            {
                if (board[i] == Marble.EMPTY)
                    res.Add(new Move(i, player));
            }
            return res;
        }

        public Marble GetOppositePlayer(Marble player)
        {
            return player == Marble.CROSS ? Marble.ZERO : Marble.CROSS;
        }

        // TODO improve score calculation
        /// <summary>
        /// Evaluates the score of the current board by only checking for the win condition
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public int eval(Marble player)
        {
            Marble other = GetOppositePlayer(player);
            int MyScore = 0;
            int OtherScore = 0;
            
            // First check on end-game states, those should heavily impact the score.
            if (IsWinner(player))
            {
                return 999;
            } 
            if (IsWinner(other))
            {
                return -999;
            }
            
            /*
            for (int i = 1; i < board.Length; i++)
            {
                if (B(player, i))
                {
                        
                    
                }
                else if (B(other, i))
                {
                    OtherScore++;
                }
            }*/
            return MyScore - OtherScore;
        }


        /***
         * methods to be implemented for finding the best move
         * */

        public bool FindAndDoBestComputerMove(Marble m)
        {
            
            return false;
        }
    }
}

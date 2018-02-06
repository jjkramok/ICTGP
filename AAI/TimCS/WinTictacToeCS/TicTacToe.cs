using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinTicTacToeCS
{
    class TicTacToe
    {
        public enum Marble { EMPTY, CROSS, ZERO };

        /**
         * 1 2 3
         * 4 5 6
         * 7 8 9
         * */

        Marble[] board;
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



        /***
         * methods to be implemented for finding the best move
         * */

        public bool FindAndDoBestComputerMove(Marble m)
        {
            return false;
        }
    }
}

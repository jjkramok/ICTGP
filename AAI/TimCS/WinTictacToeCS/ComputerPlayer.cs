using System;

namespace WinTicTacToeCS
{
    public class ComputerPlayer
    {
        private TicTacToe game;

        public ComputerPlayer(TicTacToe game)
        {
            this.game = game;
        }
        
        public Move Minimax(TicTacToe.Marble player, int depth)
        {
            foreach (var move in game.GetPossibleMoves(player))
            {
                TicTacToe cpyOfBoard = new TicTacToe(game); 
            }
            
            throw new NotImplementedException();
        }

    }
}
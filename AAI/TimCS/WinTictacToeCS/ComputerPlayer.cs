using System;

namespace WinTicTacToeCS
{
    public class ComputerPlayer
    {
        private TicTacToe game;

        ComputerPlayer(TicTacToe game)
        {
            this.game = game;
        }
        
        Move Minimax(TicTacToe.Marble player, int depth)
        {
            foreach (var move in game.GetPossibleMoves(player))
            {
                

            }
            
            throw new NotImplementedException();
        }

    }
}
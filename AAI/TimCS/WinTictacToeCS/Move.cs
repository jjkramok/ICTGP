namespace WinTicTacToeCS
{
    public class Move
    {
        public int Location { get; set; }
        public TicTacToe.Marble Player { get; set; }

        public Move(int location, TicTacToe.Marble player)
        {
            Location = location;
            Player = player;
        } 
    }
}
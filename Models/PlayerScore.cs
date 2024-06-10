namespace TicTacToe.DataAccess.Models
{
    public class PlayerScore
    {
        public string PlayerName { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }
    }
}

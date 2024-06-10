namespace TicTacToe.Models
{
    public class Rating
    {
        public int PlayerId { get; set; }
        public int TotalGames { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }
    }
}

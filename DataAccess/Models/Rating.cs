namespace TicTacToe.DataAccess.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }
    }
}

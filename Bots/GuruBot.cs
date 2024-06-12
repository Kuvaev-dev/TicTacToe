namespace TicTacToe.Bots
{
    public class GuruBot : IBot
    {
        public (int row, int col) GetNextMove(char[,] board)
        {
            // Advanced strategy combining both offensive and defensive strategies
            var defensiveBot = new DefensiveBot();
            var offensiveBot = new OffensiveBot();

            // First try to win
            var move = offensiveBot.GetNextMove(board);
            if (move != (-1, -1))
            {
                return move;
            }

            // Then try to block
            move = defensiveBot.GetNextMove(board);
            if (move != (-1, -1))
            {
                return move;
            }

            // If neither, make a random move
            return new SimpleBot().GetNextMove(board);
        }
    }
}

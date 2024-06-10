namespace TicTacToe.Strategies
{
    public class BotStrategyEasy : IBotStrategy
    {
        public (int, int) GetNextMove(char[,] board)
        {
            // Simple strategy: take the first available spot
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == ' ')
                    {
                        return (i, j);
                    }
                }
            }
            return (-1, -1); // No move possible (shouldn't happen in a valid game)
        }
    }
}

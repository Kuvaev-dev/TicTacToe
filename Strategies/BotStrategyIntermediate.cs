namespace TicTacToe.Strategies
{
    public class BotStrategyIntermediate : IBotStrategy
    {
        public (int, int) GetNextMove(char[,] board)
        {
            // Intermediate strategy: prioritize winning moves, then blocking moves, then random
            // Check for a winning move
            var winningMove = FindWinningMove(board, 'O');
            if (winningMove != (-1, -1)) return winningMove;

            // Check for a blocking move
            var blockingMove = FindWinningMove(board, 'X');
            if (blockingMove != (-1, -1)) return blockingMove;

            // Otherwise, take the first available spot
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

        private (int, int) FindWinningMove(char[,] board, char player)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == ' ')
                    {
                        board[i, j] = player;
                        if (CheckWin(board, player))
                        {
                            board[i, j] = ' '; // Reset the spot
                            return (i, j);
                        }
                        board[i, j] = ' '; // Reset the spot
                    }
                }
            }
            return (-1, -1);
        }

        private bool CheckWin(char[,] board, char player)
        {
            // Check rows, columns, and diagonals for a win
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == player && board[i, 1] == player && board[i, 2] == player)
                    return true;
                if (board[0, i] == player && board[1, i] == player && board[2, i] == player)
                    return true;
            }
            if (board[0, 0] == player && board[1, 1] == player && board[2, 2] == player)
                return true;
            if (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player)
                return true;
            return false;
        }
    }
}

namespace TicTacToe.Bots
{
    public class DefensiveBot : IBot
    {
        public (int row, int col) GetNextMove(char[,] board)
        {
            // Logic to block player's winning move
            // This is a simple example, can be extended
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (board[row, col] == '\0')
                    {
                        board[row, col] = 'O';
                        if (IsWinningMove(board, 'O'))
                        {
                            board[row, col] = '\0';
                            return (row, col);
                        }
                        board[row, col] = '\0';
                    }
                }
            }

            return new SimpleBot().GetNextMove(board);
        }

        private bool IsWinningMove(char[,] board, char player)
        {
            // Check rows, columns and diagonals for a winning move
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == player && board[i, 1] == player && board[i, 2] == player) return true;
                if (board[0, i] == player && board[1, i] == player && board[2, i] == player) return true;
            }
            if (board[0, 0] == player && board[1, 1] == player && board[2, 2] == player) return true;
            if (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player) return true;
            return false;
        }
    }
}

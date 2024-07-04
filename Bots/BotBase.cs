namespace TicTacToe.Bots
{
    /// <summary>
    /// Базовий клас для всіх ботів.
    /// </summary>
    public abstract class BotBase : IBot
    {
        protected const char Player = 'X';
        protected const char Bot = 'O';

        public abstract (int row, int col) GetNextMove(char[,] board);

        /// <summary>
        /// Перевіряє, чи є поточний гравець переможцем.
        /// </summary>
        /// <param name="board">Поточний стан ігрового поля.</param>
        /// <param name="player">Поточний гравець ('X' або 'O').</param>
        /// <returns>True, якщо гравець виграв, інакше False.</returns>
        protected static bool CheckWinner(char[,] board, char player)
        {
            // Перевірка рядків, стовпців і діагоналей на переможний хід
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == player && board[i, 1] == player && board[i, 2] == player) return true;
                if (board[0, i] == player && board[1, i] == player && board[2, i] == player) return true;
            }
            if (board[0, 0] == player && board[1, 1] == player && board[2, 2] == player) return true;
            if (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player) return true;
            return false;
        }

        /// <summary>
        /// Перевіряє, чи повністю заповнене ігрове поле.
        /// </summary>
        /// <param name="board">Поточний стан ігрового поля.</param>
        /// <returns>True, якщо поле повністю заповнене, інакше False.</returns>
        protected static bool IsBoardFull(char[,] board)
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (board[row, col] == '\0')
                        return false;
                }
            }
            return true;
        }
    }
}
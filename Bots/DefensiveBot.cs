namespace TicTacToe.Bots
{
    /// <summary>
    /// Представляє бота, який грає в хрестики-нулики, орієнтуючись на захист.
    /// </summary>
    public class DefensiveBot : IBot
    {
        /// <summary>
        /// Отримує наступний хід для бота, намагаючись заблокувати виграшний хід гравця.
        /// </summary>
        /// <param name="board">Поточний стан ігрового поля.</param>
        /// <returns>Кортеж, що містить номер рядка та стовпця наступного ходу.</returns>
        public (int row, int col) GetNextMove(char[,] board)
        {
            // Логіка блокування виграшного ходу гравця
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (board[row, col] == '\0') // Перевіряємо, чи пуста клітинка
                    {
                        board[row, col] = 'O'; // Пробуємо зробити хід за бота
                        if (IsWinningMove(board, 'O')) // Перевіряємо, чи є цей хід виграшним
                        {
                            board[row, col] = '\0'; // Скасовуємо хід
                            return (row, col); // Повертаємо координати ходу
                        }
                        board[row, col] = '\0'; // Скасовуємо хід
                    }
                }
            }

            // Якщо немає виграшного ходу, вибираємо випадковий хід
            return new SimpleBot().GetNextMove(board);
        }

        /// <summary>
        /// Перевіряє, чи є поточний хід виграшним для заданого гравця.
        /// </summary>
        /// <param name="board">Поточний стан ігрового поля.</param>
        /// <param name="player">Поточний гравець ('X' або 'O').</param>
        /// <returns>True, якщо хід є виграшним, інакше False.</returns>
        private bool IsWinningMove(char[,] board, char player)
        {
            // Перевірка рядків, стовпців і діагоналей на виграшний хід
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
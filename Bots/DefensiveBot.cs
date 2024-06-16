namespace TicTacToe.Bots
{
    /// <summary>
    /// Представляет бота, который играет в крестики-нолики, ориентируясь на защиту.
    /// </summary>
    public class DefensiveBot : IBot
    {
        /// <summary>
        /// Получает следующий ход для бота, пытаясь заблокировать выигрышный ход игрока.
        /// </summary>
        /// <param name="board">Текущее состояние игрового поля.</param>
        /// <returns>Кортеж, содержащий номер строки и столбца следующего хода.</returns>
        public (int row, int col) GetNextMove(char[,] board)
        {
            // Логика блокировки выигрышного хода игрока
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (board[row, col] == '\0') // Проверяем, пуста ли клетка
                    {
                        board[row, col] = 'O'; // Пробуем сделать ход за бота
                        if (IsWinningMove(board, 'O')) // Проверяем, является ли этот ход выигрышным
                        {
                            board[row, col] = '\0'; // Отменяем ход
                            return (row, col); // Возвращаем координаты хода
                        }
                        board[row, col] = '\0'; // Отменяем ход
                    }
                }
            }

            // Если нет выигрышного хода, выбираем случайный ход
            return new SimpleBot().GetNextMove(board);
        }

        /// <summary>
        /// Проверяет, является ли текущий ход выигрышным для заданного игрока.
        /// </summary>
        /// <param name="board">Текущее состояние игрового поля.</param>
        /// <param name="player">Текущий игрок ('X' или 'O').</param>
        /// <returns>True, если ход является выигрышным, иначе False.</returns>
        private bool IsWinningMove(char[,] board, char player)
        {
            // Проверка строк, столбцов и диагоналей на выигрышный ход
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

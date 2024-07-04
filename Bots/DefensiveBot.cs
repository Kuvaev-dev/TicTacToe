namespace TicTacToe.Bots
{
    /// <summary>
    /// Представляє бота, який грає в хрестики-нулики, орієнтуючись на захист.
    /// </summary>
    public class DefensiveBot : BotBase
    {
        /// <summary>
        /// Отримує наступний хід для бота, намагаючись заблокувати виграшний хід гравця.
        /// </summary>
        /// <param name="board">Поточний стан ігрового поля.</param>
        /// <returns>Кортеж, що містить номер рядка та стовпця наступного ходу.</returns>
        public override (int row, int col) GetNextMove(char[,] board)
        {
            // Логіка блокування виграшного ходу гравця
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (board[row, col] == '\0') // Перевіряємо, чи пуста клітинка
                    {
                        board[row, col] = 'O'; // Пробуємо зробити хід за бота
                        if (CheckWinner(board, 'O')) // Перевіряємо, чи є цей хід виграшним
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
    }
}
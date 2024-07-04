namespace TicTacToe.Bots
{
    /// <summary>
    /// Представляє бота з агресивною стратегією в грі хрестики-нулики.
    /// </summary>
    public class OffensiveBot : BotBase
    {
        /// <summary>
        /// Отримує наступний хід для бота.
        /// </summary>
        /// <param name="board">Поточний стан ігрового поля.</param>
        /// <returns>Кортеж, що містить номер рядка та стовпця наступного ходу.</returns>
        public override (int row, int col) GetNextMove(char[,] board)
        {
            // Логіка вибору виграшного ходу, якщо доступний
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (board[row, col] == '\0')
                    {
                        board[row, col] = Bot; // Спроба зробити хід
                        if (CheckWinner(board, Bot)) // Перевірка, чи є цей хід виграшним
                        {
                            board[row, col] = '\0'; // Скасування ходу
                            return (row, col); // Повернення виграшного ходу
                        }
                        board[row, col] = '\0'; // Скасування ходу
                    }
                }
            }

            // Якщо виграшний хід недоступний, використовуємо Minimax
            return new AIBot().GetNextMove(board);
        }
    }
}
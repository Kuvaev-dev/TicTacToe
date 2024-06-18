namespace TicTacToe.Bots
{
    /// <summary>
    /// Представляє бота-гуру, що використовує комбіновану стратегію атаки та захисту.
    /// </summary>
    public class GuruBot : IBot
    {
        /// <summary>
        /// Отримує наступний хід для бота.
        /// </summary>
        /// <param name="board">Поточний стан ігрового поля.</param>
        /// <returns>Кортеж, що містить номер рядка та стовпця наступного ходу.</returns>
        public (int row, int col) GetNextMove(char[,] board)
        {
            // Створюємо екземпляри ботів для захисту та атаки
            var defensiveBot = new DefensiveBot();
            var offensiveBot = new OffensiveBot();

            // Спочатку намагаємося виграти
            var move = offensiveBot.GetNextMove(board);
            if (move != (-1, -1) && board[move.row, move.col] == '\0')
            {
                return move;
            }

            // Потім намагаємося заблокувати противника
            move = defensiveBot.GetNextMove(board);
            if (move != (-1, -1) && board[move.row, move.col] == '\0')
            {
                return move;
            }

            // Якщо ні те, ні інше, робимо випадковий хід
            return new SimpleBot().GetNextMove(board);
        }
    }
}
namespace TicTacToe.Bots
{
    public class GuruBot : BotBase
    {
        /// <summary>
        /// Отримує наступний хід для бота.
        /// </summary>
        /// <param name="board">Поточний стан ігрового поля.</param>
        /// <returns>Кортеж, що містить номер рядка та стовпця наступного ходу.</returns>
        public override (int row, int col) GetNextMove(char[,] board)
        {
            // Створюємо екземпляри ботів для захисту та атаки
            var defensiveBot = new DefensiveBot();
            var offensiveBot = new OffensiveBot();

            // Спочатку намагаємося виграти
            var move = offensiveBot.GetNextMove(board);
            if (move != (-1, -1) && board[move.row, move.col] == '\0') // Правильна перевірка на порожню клітинку
            {
                return move;
            }

            // Потім намагаємося заблокувати противника
            move = defensiveBot.GetNextMove(board);
            if (move != (-1, -1) && board[move.row, move.col] == '\0') // Правильна перевірка на порожню клітинку
            {
                return move;
            }

            // Якщо ні те, ні інше, робимо випадковий хід
            return new SimpleBot().GetNextMove(board);
        }
    }
}
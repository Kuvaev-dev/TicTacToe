namespace TicTacToe.Bots
{
    /// <summary>
    /// Представляет бота-гуру, использующего комбинированную стратегию атаки и защиты.
    /// </summary>
    public class GuruBot : IBot
    {
        /// <summary>
        /// Получает следующий ход для бота.
        /// </summary>
        /// <param name="board">Текущее состояние игрового поля.</param>
        /// <returns>Кортеж, содержащий номер строки и столбца следующего хода.</returns>
        public (int row, int col) GetNextMove(char[,] board)
        {
            // Создаем экземпляры ботов для защиты и атаки
            var defensiveBot = new DefensiveBot();
            var offensiveBot = new OffensiveBot();

            // Сначала пытаемся выиграть
            var move = offensiveBot.GetNextMove(board);
            if (move != (-1, -1))
            {
                return move;
            }

            // Затем пытаемся заблокировать противника
            move = defensiveBot.GetNextMove(board);
            if (move != (-1, -1))
            {
                return move;
            }

            // Если ни то, ни другое, делаем случайный ход
            return new SimpleBot().GetNextMove(board);
        }
    }
}
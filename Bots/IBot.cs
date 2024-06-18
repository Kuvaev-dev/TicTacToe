namespace TicTacToe.Bots
{
    /// <summary>
    /// Інтерфейс, що визначає метод для отримання наступного ходу бота.
    /// </summary>
    public interface IBot
    {
        /// <summary>
        /// Отримує наступний хід бота на основі поточного стану ігрового поля.
        /// </summary>
        /// <param name="board">Поточний стан ігрового поля.</param>
        /// <returns>Кортеж з координатами наступного ходу (номер рядка і стовпця).</returns>
        (int row, int col) GetNextMove(char[,] board);
    }
}
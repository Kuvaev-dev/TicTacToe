namespace TicTacToe.Bots
{
    /// <summary>
    /// Интерфейс, определяющий метод для получения следующего хода бота.
    /// </summary>
    public interface IBot
    {
        /// <summary>
        /// Получает следующий ход бота на основе текущего состояния игрового поля.
        /// </summary>
        /// <param name="board">Текущее состояние игрового поля.</param>
        /// <returns>Кортеж с координатами следующего хода (номер строки и столбца).</returns>
        (int row, int col) GetNextMove(char[,] board);
    }
}
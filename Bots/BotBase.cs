using TicTacToe.Helpers;

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
        protected static bool CheckWinner(char[,] board, char player) =>
            BoardHelper.CheckWinner(board, player);

        /// <summary>
        /// Перевіряє, чи повністю заповнене ігрове поле.
        /// </summary>
        /// <param name="board">Поточний стан ігрового поля.</param>
        /// <returns>True, якщо поле повністю заповнене, інакше False.</returns>
        protected static bool IsBoardFull(char[,] board) => 
            BoardHelper.IsBoardFull(board);
    }
}
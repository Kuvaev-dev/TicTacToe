using System;
using System.Collections.Generic;

namespace TicTacToe.Bots
{
    /// <summary>
    /// Представляет простого бота, который делает случайные ходы.
    /// </summary>
    public class SimpleBot : IBot
    {
        private Random _random = new Random();

        /// <summary>
        /// Получает следующий ход для бота.
        /// </summary>
        /// <param name="board">Текущее состояние игрового поля.</param>
        /// <returns>Кортеж, содержащий номер строки и столбца следующего хода.</returns>
        public (int row, int col) GetNextMove(char[,] board)
        {
            List<(int row, int col)> availableMoves = new List<(int row, int col)>();

            // Определяем доступные ходы на пустых клетках
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (board[row, col] == '\0') // Проверяем, пуста ли клетка
                    {
                        availableMoves.Add((row, col)); // Добавляем координаты клетки в список доступных ходов
                    }
                }
            }

            if (availableMoves.Count > 0) // Если есть доступные ходы
            {
                int index = _random.Next(availableMoves.Count); // Выбираем случайный индекс из списка доступных ходов
                return availableMoves[index]; // Возвращаем выбранные координаты хода
            }

            return (-1, -1); // Если нет доступных ходов
        }
    }
}
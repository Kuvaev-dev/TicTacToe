using System;
using System.Collections.Generic;

namespace TicTacToe.Bots
{
    /// <summary>
    /// Представляє простого бота, який робить випадкові ходи.
    /// </summary>
    public class SimpleBot : IBot
    {
        private readonly Random _random = new();

        /// <summary>
        /// Отримує наступний хід для бота.
        /// </summary>
        /// <param name="board">Поточний стан ігрового поля.</param>
        /// <returns>Кортеж, що містить номер рядка та стовпця наступного ходу.</returns>
        public (int row, int col) GetNextMove(char[,] board)
        {
            List<(int row, int col)> availableMoves = new List<(int row, int col)>();

            // Визначаємо доступні ходи на пустих клітинках
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (board[row, col] == '\0') // Перевіряємо, чи пуста клітинка
                    {
                        availableMoves.Add((row, col)); // Додаємо координати клітинки до списку доступних ходів
                    }
                }
            }

            if (availableMoves.Count > 0) // Якщо є доступні ходи
            {
                int index = _random.Next(availableMoves.Count); // Обираємо випадковий індекс зі списку доступних ходів
                return availableMoves[index]; // Повертаємо обрані координати ходу
            }

            return (-1, -1); // Якщо немає доступних ходів
        }
    }
}
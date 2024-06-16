using System;
using System.ComponentModel;

namespace TicTacToe.Models
{
    /// <summary>
    /// Модель игры в крестики-нолики.
    /// </summary>
    public class Game : IDataErrorInfo
    {
        /// <summary>
        /// Уникальный идентификатор игры.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор игрока, который сыграл эту игру.
        /// </summary>
        public int PlayerId { get; set; }

        /// <summary>
        /// Результат игры. 0 - поражение, 1 - ничья, 2 - победа.
        /// </summary>
        public int Result { get; set; }

        /// <summary>
        /// Дата и время окончания игры.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Ходы, сделанные в игре (возможно, в виде строки или другого формата).
        /// </summary>
        public string Moves { get; set; }

        // Реализация IDataErrorInfo для валидации

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                string error = null;

                switch (columnName)
                {
                    case nameof(PlayerId):
                        if (PlayerId <= 0)
                            error = "Идентификатор игрока должен быть больше нуля.";
                        break;
                    case nameof(Result):
                        if (Result < 0 || Result > 2)
                            error = "Результат игры должен быть 0 (поражение), 1 (ничья) или 2 (победа).";
                        break;
                    case nameof(Date):
                        if (Date == DateTime.MinValue)
                            error = "Необходимо указать дату и время окончания игры.";
                        break;
                }

                return error;
            }
        }
    }
}
using System;
using System.ComponentModel;

namespace TicTacToe.Models
{
    /// <summary>
    /// Модель гри в хрестики-нулики.
    /// </summary>
    public class Game : IDataErrorInfo
    {
        /// <summary>
        /// Унікальний ідентифікатор гри.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Ідентифікатор гравця, який зіграв цю гру.
        /// </summary>
        public int PlayerId { get; set; }

        /// <summary>
        /// Результат гри. 0 - поразка, 1 - нічия, 2 - перемога.
        /// </summary>
        public int Result { get; set; }

        /// <summary>
        /// Дата та час завершення гри.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Ходи, зроблені в грі (можливо, у вигляді рядка або іншого формату).
        /// </summary>
        public string Moves { get; set; }

        // Реалізація IDataErrorInfo для валідації

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
                            error = "Ідентифікатор гравця повинен бути більше нуля.";
                        break;
                    case nameof(Result):
                        if (Result < 0 || Result > 2)
                            error = "Результат гри повинен бути 0 (поразка), 1 (нічия) або 2 (перемога).";
                        break;
                    case nameof(Date):
                        if (Date == DateTime.MinValue)
                            error = "Необхідно вказати дату та час завершення гри.";
                        break;
                }

                return error;
            }
        }
    }
}
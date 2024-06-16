using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace TicTacToe.Models
{
    /// <summary>
    /// Модель игрока в игре крестики-нолики.
    /// </summary>
    public class Player : IDataErrorInfo
    {
        private const string UsernameRegexPattern = @"^[a-zA-Z0-9_]+$";

        /// <summary>
        /// Уникальный идентификатор игрока.
        /// </summary>
        public int Id { get; set; }

        private string _username;
        /// <summary>
        /// Имя пользователя игрока.
        /// </summary>
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                ValidateUsername();
            }
        }

        /// <summary>
        /// Пароль игрока.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Флаг, указывающий, удален ли игрок (логическое удаление).
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Количество сыгранных игр игроком.
        /// </summary>
        public int GamesPlayed { get; set; }

        /// <summary>
        /// Количество побед игрока.
        /// </summary>
        public int Wins { get; set; }

        /// <summary>
        /// Количество поражений игрока.
        /// </summary>
        public int Losses { get; set; }

        /// <summary>
        /// Количество ничьих игрока.
        /// </summary>
        public int Draws { get; set; }

        /// <summary>
        /// Дата и время последнего входа в систему игрока.
        /// </summary>
        public DateTime? LastLogin { get; set; }

        // Реализация IDataErrorInfo для валидации

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                string error = null;

                switch (columnName)
                {
                    case nameof(Username):
                        if (string.IsNullOrEmpty(Username))
                            error = "Имя пользователя не может быть пустым.";
                        break;
                    case nameof(Password):
                        if (string.IsNullOrEmpty(Password))
                            error = "Пароль не может быть пустым.";
                        break;
                    case nameof(Wins):
                        if (Wins < 0)
                            error = "Количество побед не может быть меньше нуля.";
                        break;
                    case nameof(Losses):
                        if (Losses < 0)
                            error = "Количество поражений не может быть меньше нуля.";
                        break;
                    case nameof(Draws):
                        if (Draws < 0)
                            error = "Количество ничьих не может быть меньше нуля.";
                        break;
                }

                return error;
            }
        }

        private void ValidateUsername()
        {
            if (string.IsNullOrEmpty(Username))
                return;

            if (!Regex.IsMatch(Username, UsernameRegexPattern))
            {
                throw new ArgumentException("Имя пользователя содержит недопустимые символы.");
            }
        }
    }
}
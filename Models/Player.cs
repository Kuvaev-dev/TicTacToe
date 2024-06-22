using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;

namespace TicTacToe.Models
{
    /// <summary>
    /// Модель гравця в грі хрестики-нулики.
    /// </summary>
    public class Player : IDataErrorInfo
    {
        /// <summary>
        /// Regex для валідації імені користувача
        /// </summary>
        private const string UsernameRegexPattern = @"^[a-zA-Z0-9_]+$";

        /// <summary>
        /// Унікальний ідентифікатор гравця.
        /// </summary>
        public int Id { get; set; }

        private string _username;
        /// <summary>
        /// Ім'я користувача гравця.
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
        /// Пароль гравця.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Прапор, що вказує, чи видалено гравця (логічне видалення).
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Кількість зіграних ігор гравцем.
        /// </summary>
        public int GamesPlayed { get; set; }

        /// <summary>
        /// Кількість перемог гравця.
        /// </summary>
        public int Wins { get; set; }

        /// <summary>
        /// Кількість поразок гравця.
        /// </summary>
        public int Losses { get; set; }

        /// <summary>
        /// Кількість нічиїх гравця.
        /// </summary>
        public int Draws { get; set; }

        /// <summary>
        /// Дата та час останнього входу в систему гравця.
        /// </summary>
        public DateTime? LastLogin { get; set; }

        // Реалізація IDataErrorInfo для валідації

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                string error = null;

                switch (columnName)
                {
                    case nameof(Wins):
                        if (Wins < 0)
                            error = (string)Application.Current.FindResource("StringPlayerWinsError");
                        break;
                    case nameof(Losses):
                        if (Losses < 0)
                            error = (string)Application.Current.FindResource("StringPlayerLossesError");
                        break;
                    case nameof(Draws):
                        if (Draws < 0)
                            error = (string)Application.Current.FindResource("StringPlayerDrawsError");
                        break;
                }

                return error;
            }
        }

        /// <summary>
        /// Метод валідації імені користувача.
        /// </summary>
        private void ValidateUsername()
        {
            if (string.IsNullOrEmpty(Username))
                return;

            if (!Regex.IsMatch(Username, UsernameRegexPattern))
            {
                throw new ArgumentException((string)Application.Current.FindResource("StringPlayerValidateUsernameError"));
            }
        }
    }
}

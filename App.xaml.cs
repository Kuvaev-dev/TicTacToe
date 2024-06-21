using System;
using System.Collections.Generic;
using System.Windows;

namespace TicTacToe
{
    /// <summary>
    /// Клас додатку TicTacToe, який реалізує основні функціональність додатку.
    /// </summary>
    public partial class App : Application
    {
        private string _selectedLanguage;

        /// <summary>
        /// Обрана мова додатку.
        /// </summary>
        public string SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                if (_selectedLanguage != value)
                {
                    _selectedLanguage = value;
                    OnLanguageChanged();
                }
            }
        }

        /// <summary>
        /// Подія, яка викликається при зміні мови додатку.
        /// </summary>
        public event Action LanguageChanged;

        /// <summary>
        /// Метод, який викликає подію зміни мови.
        /// </summary>
        private void OnLanguageChanged()
        {
            LanguageChanged?.Invoke();
        }

        /// <summary>
        /// Доступні мови, які підтримуються додатком.
        /// </summary>
        public List<string> AvailableLanguages { get; } = new List<string> { "Українська", "English" };
    }
}
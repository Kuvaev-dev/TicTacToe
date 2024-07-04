using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using TicTacToe.Views.CustomControls;

namespace TicTacToe.Views.Utils
{
    /// <summary>
    /// Утиліти для роботи з ComboBox у контексті зміни мови додатку.
    /// </summary>
    public static class ComboBoxUtils
    {
        private static readonly Dictionary<string, string> LanguageResourcePaths = new()
        {
            { "Українська", "/Views/Localization/Українська.xaml" },
            { "English", "/Views/Localization/English.xaml" }
        };

        /// <summary>
        /// Обробник зміни вибору елемента в ComboBox.
        /// </summary>
        /// <param name="sender">Об'єкт, що викликав подію.</param>
        /// <param name="e">Аргументи події зміни вибору.</param>
        public static void HandleSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as CustomComboBox;
            if (comboBox == null || comboBox.SelectedItem == null)
                return;

            var selectedLanguage = comboBox.SelectedItem.ToString();

            if (!LanguageResourcePaths.TryGetValue(selectedLanguage, out var resourcePath))
                return;

            var resourceDictionary = new ResourceDictionary
            {
                Source = new Uri(resourcePath, UriKind.Relative)
            };

            ((App)Application.Current).SelectedLanguage = selectedLanguage;

            LanguageResourceUtils.RemoveLanguageDictionaries();
            LanguageResourceUtils.AddLanguageDictionary(resourceDictionary);
        }

        /// <summary>
        /// Ініціалізація вибраної мови в ComboBox.
        /// </summary>
        /// <param name="sender">Об'єкт, що викликав подію.</param>
        /// <param name="e">Аргументи події завантаження.</param>
        public static void LoadSelectedLanguage(object sender, RoutedEventArgs e)
        {
            var comboBox = sender as CustomComboBox;
            if (comboBox == null)
                return;

            var app = (App)Application.Current;
            if (string.IsNullOrEmpty(app.SelectedLanguage))
            {
                comboBox.SelectedItem = comboBox.Items[0];
                app.SelectedLanguage = comboBox.SelectedItem.ToString();
            }
            else
            {
                comboBox.SelectedItem = app.SelectedLanguage;
            }

            app.LanguageChanged += () =>
            {
                if (comboBox.SelectedItem?.ToString() != app.SelectedLanguage)
                {
                    comboBox.SelectedItem = app.SelectedLanguage;
                }
            };
        }
    }
}
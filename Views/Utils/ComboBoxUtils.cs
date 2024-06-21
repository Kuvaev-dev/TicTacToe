using System;
using System.Windows;
using System.Windows.Controls;
using TicTacToe.Views.CustomControls;

namespace TicTacToe.Views.Utils
{
    /// <summary>
    /// Утилітарний клас для роботи з ComboBox у контексті зміни мови додатку.
    /// </summary>
    public static class ComboBoxUtils
    {
        /// <summary>
        /// Обробник зміни вибору елемента в ComboBox.
        /// </summary>
        /// <param name="sender">Об'єкт, що викликав подію.</param>
        /// <param name="e">Аргументи події зміни вибору.</param>
        /// <param name="resourcePath">Шлях до ресурсів мови.</param>
        public static void HandleSelectionChanged(object sender, SelectionChangedEventArgs e, string resourcePath)
        {
            var comboBox = sender as CustomComboBox;
            if (comboBox == null || comboBox.SelectedItem == null)
                return;

            var selectedLanguage = comboBox.SelectedItem.ToString();
            var resourceDictionary = new ResourceDictionary();

            switch (selectedLanguage)
            {
                case "Українська":
                    resourceDictionary.Source = new Uri(resourcePath + "Українська.xaml", UriKind.Relative);
                    break;
                case "English":
                    resourceDictionary.Source = new Uri(resourcePath + "English.xaml", UriKind.Relative);
                    break;
                    // Додайте кейси для інших мов, якщо необхідно
            }

            // Оновлення обраної мови в класі App
            ((App)Application.Current).SelectedLanguage = selectedLanguage;

            // Видалення існуючих мовних словників та додавання нового
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

            // Ініціалізація вибраного елемента до поточної обраної мови додатку
            comboBox.SelectedItem = ((App)Application.Current).SelectedLanguage;

            // Підписка на подію зміни мови для оновлення ComboBox при її зміні
            ((App)Application.Current).LanguageChanged += () =>
            {
                if (comboBox.SelectedItem?.ToString() != ((App)Application.Current).SelectedLanguage)
                {
                    comboBox.SelectedItem = ((App)Application.Current).SelectedLanguage;
                }
            };
        }
    }
}

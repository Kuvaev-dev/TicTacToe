using System.Linq;
using System.Windows;

namespace TicTacToe.Views.Utils
{
    /// <summary>
    /// Утиліти для роботи з мовними словниками ресурсів додатку.
    /// </summary>
    public static class LanguageResourceUtils
    {
        private const string LanguageTag = "LanguageDictionary";

        /// <summary>
        /// Додає новий словник ресурсів мови до загального словника ресурсів додатку.
        /// </summary>
        /// <param name="resourceDictionary">Словник ресурсів мови для додавання.</param>
        public static void AddLanguageDictionary(ResourceDictionary resourceDictionary)
        {
            resourceDictionary[LanguageTag] = true;
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
        }

        /// <summary>
        /// Видаляє всі мовні словники з загального словника ресурсів додатку.
        /// </summary>
        public static void RemoveLanguageDictionaries()
        {
            // Знаходимо всі мовні словники за тегом і видаляємо їх з загального словника ресурсів
            var languageDictionaries = Application.Current.Resources.MergedDictionaries
                .Where(d => d.Contains(LanguageTag) && (bool)d[LanguageTag])
                .ToList();

            foreach (var dictionary in languageDictionaries)
            {
                Application.Current.Resources.MergedDictionaries.Remove(dictionary);
            }
        }
    }
}
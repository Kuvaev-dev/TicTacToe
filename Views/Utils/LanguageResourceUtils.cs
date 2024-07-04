using System.Linq;
using System.Windows;

namespace TicTacToe.Views.Utils
{
    /// <summary>
    /// Утиліти для керування мовними словниками ресурсів у додатку.
    /// </summary>
    public static class LanguageResourceUtils
    {
        private const string LanguageTag = "LanguageDictionary";

        /// <summary>
        /// Додає новий мовний словник ресурсів до злитих словників додатку.
        /// </summary>
        /// <param name="resourceDictionary">Словник ресурсів для додавання.</param>
        public static void AddLanguageDictionary(ResourceDictionary resourceDictionary)
        {
            resourceDictionary[LanguageTag] = true;
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
        }

        /// <summary>
        /// Видаляє всі мовні словники ресурсів із злитих словників додатку.
        /// </summary>
        public static void RemoveLanguageDictionaries()
        {
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
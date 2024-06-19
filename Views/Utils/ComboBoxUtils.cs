using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TicTacToe.Views.CustomControls;

namespace TicTacToe.Views.Utils
{
    public static class ComboBoxUtils
    {
        public static void HandleSelectionChanged(object sender, SelectionChangedEventArgs e, string languagesResourcePath)
        {
            var comboBox = sender as CustomComboBox;
            if (comboBox?.SelectedItem is ComboBoxItem selectedItem)
            {
                var selectedLanguage = selectedItem.Content.ToString();

                var resources = Application.Current.Resources;

                if (!string.IsNullOrEmpty(languagesResourcePath))
                {
                    // Remove existing language resources
                    var existingLanguageResources = resources.MergedDictionaries
                        .Where(d => d.Source != null && d.Source.ToString().Contains(languagesResourcePath))
                        .ToList();
                    foreach (var existingLanguageResource in existingLanguageResources)
                    {
                        resources.MergedDictionaries.Remove(existingLanguageResource);
                    }

                    // Determine the culture and new resource path
                    var cultureName = selectedLanguage == "Українська" ? "uk-UA" : "en-US";

                    var newLanguageResourcePath = $"{languagesResourcePath}{selectedLanguage}.xaml";

                    // Load new language resources
                    var newLanguageResource = new ResourceDictionary
                    {
                        Source = new Uri(newLanguageResourcePath, UriKind.RelativeOrAbsolute)
                    };
                    resources.MergedDictionaries.Add(newLanguageResource);
                }
            }
        }
    }
}

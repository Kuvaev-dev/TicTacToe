namespace TicTacToe.ViewModels.Base
{
    /// <summary>
    /// Локатор ViewModel, який надає доступ до екземплярів ViewModel у додатку.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Головна ViewModel, що управляє головним вікном додатка.
        /// </summary>
        public MainViewModel Main { get; }

        /// <summary>
        /// Ініціалізує новий екземпляр ViewModelLocator з головною ViewModel.
        /// </summary>
        public ViewModelLocator()
        {
            Main = new MainViewModel();
        }
    }
}
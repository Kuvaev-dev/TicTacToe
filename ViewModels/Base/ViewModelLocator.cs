namespace TicTacToe.ViewModels.Base
{
    /// <summary>
    /// Локатор ViewModel, который предоставляет доступ к экземплярам ViewModel в приложении.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Главная ViewModel, управляющая главным окном приложения.
        /// </summary>
        public MainViewModel Main { get; }

        /// <summary>
        /// Создает новый экземпляр ViewModelLocator с главной ViewModel.
        /// </summary>
        public ViewModelLocator()
        {
            Main = new MainViewModel();
        }
    }
}
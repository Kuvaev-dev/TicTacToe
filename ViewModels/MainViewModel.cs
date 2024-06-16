using System.Windows.Controls;
using TicTacToe.ViewModels.Base;
using TicTacToe.Views;

namespace TicTacToe.ViewModels
{
    /// <summary>
    /// ViewModel, управляющая главным окном приложения.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private Page _currentPage;

        /// <summary>
        /// Текущая страница в главном окне приложения.
        /// </summary>
        public Page CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Создает экземпляр MainViewModel с начальной страницей входа.
        /// </summary>
        public MainViewModel()
        {
            CurrentPage = new ProfilePage(this);
        }

        /// <summary>
        /// Перенаправляет на указанную страницу.
        /// </summary>
        /// <param name="page">Страница для навигации.</param>
        public void NavigateTo(Page page)
        {
            CurrentPage = page;
        }
    }
}

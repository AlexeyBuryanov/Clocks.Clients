using Prism.Mvvm;
using Prism.Navigation;

namespace Clocks.Clients.Core.ViewModels.Base
{
    /// <summary>
    /// Базовая модель представления
    /// </summary>
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible
    {
        /// <summary>
        /// Занят или нет
        /// </summary>
        private bool _isBusy;
        public bool IsBusy {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        /// <summary>
        /// Заголовок
        /// </summary>
        private string _title;
        public string Title {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters) { }
        public virtual void OnNavigatedTo(INavigationParameters parameters) { }
        public virtual void OnNavigatingTo(INavigationParameters parameters) { }
        public virtual void Destroy() { }
    }
}

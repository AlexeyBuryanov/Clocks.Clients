using Prism.Mvvm;
using System;
using System.Threading.Tasks;

namespace Clocks.Clients.Core.Models
{
    /// <inheritdoc />
    /// <summary>
    /// Элемент меню
    /// </summary>
    public class MenuItem : BindableBase
    {
        private string _title;
        private MenuItemType _menuItemType;
        private Type _viewType;
        private bool _isEnabled;

        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        /// <summary>
        /// Тип меню
        /// </summary>
        public MenuItemType MenuItemType
        {
            get => _menuItemType;
            set => SetProperty(ref _menuItemType, value);
        }

        /// <summary>
        /// Тип модели представления
        /// </summary>
        public Type ViewType
        {
            get => _viewType;
            set => SetProperty(ref _viewType, value);
        }

        /// <summary>
        /// Включен ли
        /// </summary>
        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        /// <summary>
        /// Действие после навигации
        /// </summary>
        public Func<Task> AfterNavigationAction { get; set; }
    }
}
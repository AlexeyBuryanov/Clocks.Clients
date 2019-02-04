using Clocks.Clients.Core.Models;
using Clocks.Clients.Core.Services.Analytic;
using Clocks.Clients.Core.Services.Authentication;
using Clocks.Clients.Core.Services.OpenUri;
using Clocks.Clients.Core.ViewModels.Base;
using Clocks.Clients.Core.Views;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using MenuItem = Clocks.Clients.Core.Models.MenuItem;

namespace Clocks.Clients.Core.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
		/// <summary>
		/// Служба навигации
		/// </summary>
		private readonly INavigationService _navigationService;
		/// <summary>
		/// Служба аналитики
		/// </summary>
		private readonly IAnalyticService _analyticService;
		/// <summary>
		/// Служба аутентификации
		/// </summary>
		private readonly IAuthenticationService _authenticationService;
		/// <summary>
		/// Служба открытия URI
		/// </summary>
		private readonly IOpenUriService _openUrlService;
		/// <summary>
		/// Элементы меню
		/// </summary>
		private ObservableCollection<MenuItem> _menuItems;

		public MainViewModel(
			INavigationService navigationService,
			IAnalyticService analyticService,
			IAuthenticationService authenticationService,
			IOpenUriService openUriService)
		{
			_navigationService = navigationService;
			_analyticService = analyticService;
			_authenticationService = authenticationService;
			_openUrlService = openUriService;

			Title = "Главная";

			MenuItems = new ObservableCollection<MenuItem>();

			// Инициализация элементов
			InitMenuItems();
		}

		/// <summary>
		/// Имя пользователя
		/// </summary>
		public string UserName => AppSettings.User?.Name;
		/// <summary>
		/// Аватар, ссылка
		/// </summary>
		public string UserAvatar => AppSettings.User?.AvatarUrl;

		/// <summary>
		/// Элементы меню св-во
		/// </summary>
		public ObservableCollection<MenuItem> MenuItems {
			get => _menuItems;
			set => SetProperty(ref _menuItems, value);
		}

		public ICommand MenuItemSelectedCommand => new DelegateCommand<MenuItem>(async item => await OnSelectMenuItem(item));

		/// <summary>
		/// Инициализация меню
		/// </summary>
		private void InitMenuItems()
		{
			MenuItems.Add(new MenuItem
			{
				Title = "Часы",
				MenuItemType = MenuItemType.Home,
				ViewType = typeof(HomeView),
				IsEnabled = true
			});

			MenuItems.Add(new MenuItem
			{
				Title = "Выйти",
				MenuItemType = MenuItemType.Logout,
				ViewType = typeof(LoginView),
				IsEnabled = true,
				AfterNavigationAction = RemoveUserCredentials
			});

			MenuItems.Add(new MenuItem
			{
				Title = "Автор",
				MenuItemType = MenuItemType.Author,
				IsEnabled = true
			});
		}

		/// <summary>
		/// При выборе меню
		/// </summary>
		private async Task OnSelectMenuItem(MenuItem item)
		{
			if (item.MenuItemType == MenuItemType.Author)
			{
				var props = new Dictionary<string, string>
				{
					{"AvatarUrl", _authenticationService.AuthenticatedUser.AvatarUrl},
					{"Email", _authenticationService.AuthenticatedUser.Email},
					{"Device", Device.RuntimePlatform}
				};
				_analyticService.TrackEvent("Someone is interested in you =)", props);

				_openUrlService.OpenUri(@"https://github.com/AlexeyBuryanov");
			}

			if (item.IsEnabled && item.ViewType != null)
			{
				// Вызываем действие после навигации
				item.AfterNavigationAction?.Invoke();

				// Переместится к представлению по типу модели представления
				if (item.ViewType == typeof(LoginView))
				{
					await _navigationService.NavigateAsync("/LoginView");
				}
				else
				{
					await _navigationService.NavigateAsync($"/MainView/NavigationPage/{item.ViewType.Name}");
				}
			}
		}

		/// <summary>
		/// Удаление учетных данных пользователя
		/// </summary>
		private Task RemoveUserCredentials()
		{
			var props = new Dictionary<string, string>
			{
				{"AvatarUrl", _authenticationService.AuthenticatedUser.AvatarUrl},
				{"Email", _authenticationService.AuthenticatedUser.Email},
				{"Device", Device.RuntimePlatform}
			};
			_analyticService.TrackEvent("SignOut", props);

			// Выходим
			return _authenticationService.LogoutAsync();
		}

		/// <summary>
		/// Изменить статус элемента бокового меню
		/// </summary>
		private void SetMenuItemStatus(MenuItemType type, bool enabled)
		{
			var menuItem = MenuItems.FirstOrDefault(m => m.MenuItemType == type);

			if (menuItem != null)
			{
				menuItem.IsEnabled = enabled;
			}
		}
	}
}

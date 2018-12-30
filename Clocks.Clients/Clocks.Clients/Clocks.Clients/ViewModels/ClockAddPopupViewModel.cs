using Amporis.Xamarin.Forms.ColorPicker;
using Clocks.Clients.Core.Models.Database;
using Clocks.Clients.Core.Services.Dialog;
using Clocks.Clients.Core.ViewModels.Base;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Clocks.Clients.Core.ViewModels
{
	public class ClockAddPopupViewModel : ViewModelBase
	{
		/// <summary>
		/// Служба навигации
		/// </summary>
		private readonly INavigationService _navigationService;
		/// <summary>
		/// Список стран для отображения
		/// </summary>
		private ObservableCollection<string> _cityList;
		private List<City> _cities;
		/// <summary>
		/// Служба диалогов
		/// </summary>
		private readonly IDialogService _dialogService;
		/// <summary>
		/// Выбранный город
		/// </summary>
		private string _selectedCity;
		/// <summary>
		/// Выбранная страна Id (для добавления)
		/// </summary>
		private int _selectedCityId;
		/// <summary>
		/// Часы для добавления
		/// </summary>
		private Clock _clock;
		/// <summary>
		/// Цвет отметок времени
		/// </summary>
		private Color _tickMarksColor;
		/// <summary>
		/// Цвет секундной стрелки
		/// </summary>
		private Color _secondArrowColor;
		/// <summary>
		/// Цвет минутной стрелки
		/// </summary>
		private Color _minuteArrowColor;
		/// <summary>
		/// Цвет часовой стрелки
		/// </summary>
		private Color _hourArrowColor;

		private readonly UnitOfWork _unitOfWork;

		public ClockAddPopupViewModel(
			INavigationService navigationService,
			IDialogService dialogService)
		{
			_navigationService = navigationService;
			_dialogService = dialogService;

			Title = "Добавление часов";

			_cityList = new ObservableCollection<string>();
			_cities = new List<City>();
			_selectedCity = "Киев";
			_selectedCityId = 0;

			_tickMarksColor = Color.Black;
			_secondArrowColor = Color.DarkRed;
			_minuteArrowColor = Color.Gray;
			_hourArrowColor = Color.Black;

			_unitOfWork = new UnitOfWork();

			GetCitiesFromDb();
		}

		private async void GetCitiesFromDb()
		{
			try
			{
				_cities = await _unitOfWork.CityRepository.GetAllAsync();
				_cities.ForEach(city =>
				{
					CityList.Add(city.Name);
				});
			}
			catch (Exception e)
			{
				await _dialogService.ShowAlertAsync($"Ошибка при получении городов из БД: {e.Message}",
					"Ой, что-то пошло не так", "ОК =(");
				Console.WriteLine(e);
			}
		}

		public ObservableCollection<string> CityList {
			get => _cityList;
			set => SetProperty(ref _cityList, value);
		}
		public string SelectedCity {
			get => _selectedCity;
			set => SetProperty(ref _selectedCity, value);
		}
		public Color TickMarksColor {
			get => _tickMarksColor;
			set => SetProperty(ref _tickMarksColor, value);
		}
		public Color SecondArrowColor {
			get => _secondArrowColor;
			set => SetProperty(ref _secondArrowColor, value);
		}
		public Color MinuteArrowColor {
			get => _minuteArrowColor;
			set => SetProperty(ref _minuteArrowColor, value);
		}
		public Color HourArrowColor {
			get => _hourArrowColor;
			set => SetProperty(ref _hourArrowColor, value);
		}

		public ICommand AddClockPopupCommand => new DelegateCommand(async () => await AddClockPopup());
		public ICommand ClosePopupCommand => new DelegateCommand(async () => await ClosePopup());

		private async Task ClosePopup()
		{
			await _navigationService.GoBackAsync();
		}

		private async Task AddClockPopup()
		{
			// Получаем ID выбранного города
			_cities.ForEach(city =>
			{
				if (city.Name == _selectedCity)
				{
					_selectedCityId = city.CityId;
				}
			});
			// Создаём новый экземпляр часов
			_clock = new Clock
			{
				ClockId = 0,
				CityId = _selectedCityId,
				ClockTypeId = 1,
				Description = "",
				TickMarksColor = _tickMarksColor,
				TickMarksColorHex = _tickMarksColor.ToHex(),
				SecondArrowColor = _secondArrowColor,
				SecondArrowColorHex = _secondArrowColor.ToHex(),
				MinuteArrowColor = _minuteArrowColor,
				MinuteArrowColorHex = _minuteArrowColor.ToHex(),
				HourArrowColor = _hourArrowColor,
				HourArrowColorHex = _hourArrowColor.ToHex()
			};

			await _navigationService.GoBackAsync(new NavigationParameters
			{
				{ "newClock", _clock }
			});
		}
	}
}

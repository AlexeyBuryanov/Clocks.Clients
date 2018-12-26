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
    public class ClockEditPopupViewModel : ViewModelBase
    {
        /// <summary>
        /// Служба навигации
        /// </summary>
        private readonly INavigationService _navigationService;
        /// <summary>
        /// Служба диалогов
        /// </summary>
        private readonly IDialogService _dialogService;

        /// <summary>
        /// Список стран
        /// </summary>
        private ObservableCollection<string> _cityList;
        private List<City> _cities;

        /// <summary>
        /// Выбранный город
        /// </summary>
        private string _selectedCity;

        /// <summary>
        /// Часы для редактирования
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

        private Clock _selectedClock;

        private double _offset;

        public ClockEditPopupViewModel(
            INavigationService navigationService,
            IDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;

            Title = "Редактирование часов";

            _cityList = new ObservableCollection<string>();
            _cities = new List<City>();
            _selectedCity = "Киев";

            _tickMarksColor = Color.Black;
            _secondArrowColor = Color.DarkRed;
            _minuteArrowColor = Color.Gray;
            _hourArrowColor = Color.Black;

            _selectedClock = new Clock();
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
        public string SelectedCity
        {
            get => _selectedCity;
            set
            {
                SetProperty(ref _selectedCity, value);
                // TODO: с циклом плохое решение
                _cities.ForEach(city =>
                {
                    if (_selectedCity == city.Name)
                    {
                        Offset = city.Offset;
                    }
                });
            }
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
        public double Offset {
            get => _offset;
            set => SetProperty(ref _offset, value);
        }

        public ICommand EditClockPopupCommand => new DelegateCommand(async () => await EditClockPopup());
        public ICommand ClosePopupCommand => new DelegateCommand(async () => await ClosePopup());

        private async Task ClosePopup()
        {
            await _navigationService.GoBackAsync();
        }

        private async Task EditClockPopup()
        {
            // TODO: оптимизация/рефакторинг
            _clock = _selectedClock;
            _clock.TickMarksColor = _tickMarksColor;
            _clock.TickMarksColorHex = _tickMarksColor.ToHex();
            _clock.SecondArrowColor = _secondArrowColor;
            _clock.SecondArrowColorHex = _secondArrowColor.ToHex();
            _clock.MinuteArrowColor = _minuteArrowColor;
            _clock.MinuteArrowColorHex = _minuteArrowColor.ToHex();
            _clock.HourArrowColor = _hourArrowColor;
            _clock.HourArrowColorHex = _hourArrowColor.ToHex();
            _clock.ClockType = await _unitOfWork.ClockTypeRepository.GetAsync(_selectedClock.ClockTypeId);
            var cities = await _unitOfWork.CityRepository.GetAllAsync();
            cities.ForEach(city =>
            {
                if (city.Name == _selectedCity)
                {
                    _clock.City = city;
                }
            });

            await _navigationService.GoBackAsync(new NavigationParameters
            {
                { "editClock", _clock }
            });
        }
        
        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            if (parameters.ContainsKey("selectedClock"))
            {
                _selectedClock = parameters["selectedClock"] as Clock;
                SelectedCity = _selectedClock.City.Name;
                Offset = _selectedClock.City.Offset;
                TickMarksColor = _selectedClock.TickMarksColor;
                SecondArrowColor = _selectedClock.SecondArrowColor;
                MinuteArrowColor = _selectedClock.MinuteArrowColor;
                HourArrowColor = _selectedClock.HourArrowColor;
            }
        }
    }
}

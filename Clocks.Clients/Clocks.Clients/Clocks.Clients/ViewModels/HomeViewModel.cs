using Clocks.Clients.Core.Extensions;
using Clocks.Clients.Core.Helpers;
using Clocks.Clients.Core.Models.Database;
using Clocks.Clients.Core.Services.Analytic;
using Clocks.Clients.Core.Services.Dialog;
using Clocks.Clients.Core.ViewModels.Base;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Clocks.Clients.Core.ViewModels
{
	public class HomeViewModel : ViewModelBase
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
		/// Служба аналитики
		/// </summary>
		private readonly IAnalyticService _analyticService;

		/// <summary>
		/// Список часов
		/// </summary>
		private ObservableCollection<Clock> _clocks;

		private bool _hasItems;

		private readonly UnitOfWork _unitOfWork;

		private readonly Timer _timer;

		private Clock _selectedClock;

		public HomeViewModel(
			INavigationService navigationService,
			IDialogService dialogService,
			IAnalyticService analyticService)
		{
			_navigationService = navigationService;
			_dialogService = dialogService;
			_analyticService = analyticService;

			Title = "Список часов";

			IsBusy = true;

			_clocks = new ObservableCollection<Clock>();

			HasItems = true;

			_selectedClock = new Clock();

			_unitOfWork = new UnitOfWork();

			_timer = new Timer(TimeSpan.FromSeconds(1.0 / 60), UpdateDigitalTime, true);

			GetClocksFromDbAsync();
		}
		
		private async void GetClocksFromDbAsync()
		{
			try
			{
				IsBusy = true;
				var clocks = await _unitOfWork.ClockRepository.GetAllAsync();
				if (_clocks.Count > 0) _clocks.Clear();
				_clocks = clocks.ToObservableCollection();
				// Фикс бага, когда на UWP первый элемент списка отображал маленькие часы
				//var c = _clocks[0];
				//var i = _clocks.IndexOf(c);
				//_clocks.RemoveAt(i);
				//_clocks.Insert(i, c);
			}
			catch (Exception e)
			{
				// TODO: все сообщения-строки переместить в ресурсы
				await _dialogService.ShowAlertAsync($"Ошибка при получении часов из БД: {e.Message}\n\n" +
					"Мы работаем над этим...",
					"Ой, что-то пошло не так", "ОК =(");
				Console.WriteLine(e);
			}
			finally
			{
				IsBusy = false;
				_timer.Start();
			}
		}

		/// <summary>
		/// Локальный таймер для обновления цифрового времени
		/// </summary>
		private async void UpdateDigitalTime()
		{
			if (_clocks.Count > 0)
			{
				try
				{
					await _clocks
						.ToAsyncEnumerable()
						.ForEachAsync(clock =>
						{
							clock.Description = DateTime.UtcNow.AddHours(clock.City.Offset).ToString("HH:mm:ss");
						});

					//ClocksList
					//    .AsParallel()
					//    .ForAll(clock =>
					//    {
					//        clock.Description = DateTime.UtcNow.AddHours(clock.City.Offset).ToString("HH:mm:ss");
					//    });
				}
				catch (Exception e)
				{
					await _dialogService.ShowAlertAsync(
						$"Ошибка при обновлении цифрового времени часов: {e.Message}\n\n" +
						"Мы работаем над этим...",
						"Ой, что-то пошло не так", "ОК =(");
					Console.WriteLine(e);
				}
			}
		}

		public ObservableCollection<Clock> ClocksList {
			get => _clocks;
			set => SetProperty(ref _clocks, value);
		}

		public bool HasItems {
			get => _hasItems;
			set => SetProperty(ref _hasItems, value);
		}

		public ICommand DeleteClockCommand => new DelegateCommand<Clock>(async clock => await OnDelete(clock));
		public ICommand AddClockCommand => new DelegateCommand(async () => await OnAdd());
		public ICommand ClocksListItemSelectedCommand => new DelegateCommand<Clock>(async clock => await OnClickClockItem(clock));

		private async Task OnClickClockItem(Clock selectedClock)
		{
			IsBusy = true;
			_selectedClock = selectedClock;
			await _navigationService.NavigateAsync("ClockEditPopupView", new NavigationParameters
			{
				{ "selectedClock", selectedClock }
			});
			IsBusy = false;
		}

		private async Task OnAdd()
		{
			IsBusy = true;
			await _navigationService.NavigateAsync("ClockAddPopupView");
			IsBusy = false;
		}

		private async Task OnDelete(Clock clock)
		{
			if (clock != null)
			{
				IsBusy = true;

				try
				{
					// Производим удаление из списка запоминая индекс
					var index = _clocks.IndexOf(clock);
					_clocks.Remove(clock);
					_analyticService.TrackEvent("DeleteClock");

					// Производим вставку обратно в список, если удаление было не нарошным
					var confirm = await _dialogService.ShowConfirmAsync("Вы действительно хотите " +
						$"удалить часы с ID #{clock.ClockId}?",
						"Подтверждение удаления", "ДА", "ОТМЕНИТЬ УДАЛЕНИЕ");

					if (!confirm)
					{
						_clocks.Insert(index, clock);
					}
					else
					{
						await _unitOfWork.ClockRepository.DeleteAsync(clock.ClockId);
						await _unitOfWork.SaveAsync();
						_dialogService.ShowToast($"Часы с ID {clock.ClockId} успешно удалены");
					}

					HasItems = _clocks.Count > 0;
				}
				catch (Exception e)
				{
					await _dialogService.ShowAlertAsync($"Ошибка при удалении часов из БД: {e.Message}\n\n" +
						"Мы работаем над этим...",
						"Ой, что-то пошло не так", "ОК =(");
					Console.WriteLine(e);
				}
				finally
				{
					IsBusy = false;
				}
			}
		}

		public override async void OnNavigatedTo(INavigationParameters parameters)
		{
			base.OnNavigatedTo(parameters);

			/* ****************** Добавление ****************** */
			if (parameters.ContainsKey("newClock"))
			{
				_timer.Stop();

				IsBusy = true;

				_analyticService.TrackEvent("AddClock");
				
				try
				{
					var addClock = parameters["newClock"] as Clock;
					await _unitOfWork.ClockRepository.AddAsync(addClock);
					await _unitOfWork.SaveAsync();
					_clocks.Add(addClock);
					_dialogService.ShowToast("Добавлены новые часы");
				}
				catch (Exception e)
				{
					await _dialogService.ShowAlertAsync($"Ошибка при добавлении новых часов в БД: {e.Message}\n\n" +
						"Мы работаем над этим...",
						"Ой, что-то пошло не так", "ОК =(");
					Console.WriteLine(e);
				}
				finally
				{
					IsBusy = false;
					_timer.Start();
					HasItems = _clocks.Count > 0;
				}
			}

			/* **************** Редактирование **************** */
			if (parameters.ContainsKey("editClock"))
			{
				_timer.Stop();

				IsBusy = true;

				_analyticService.TrackEvent("EditClock");
				
				try
				{
					var editClock = parameters["editClock"] as Clock;
					var index = _clocks.IndexOf(_selectedClock);
					ClocksList[index] = editClock;
					_unitOfWork.ClockRepository.Update(editClock);
					await _unitOfWork.SaveAsync();
				}
				catch (Exception e)
				{
					await _dialogService.ShowAlertAsync($"Ошибка при изменении часов: {e.Message}\n\n" +
						"Мы работаем над этим...",
						"Ой, что-то пошло не так", "ОК =(");
					Console.WriteLine(e);
				}
				finally
				{
					IsBusy = false;
					_timer.Start();
				}
			} // if
		} // OnNavigatedTo
	} // HomeViewModel
}

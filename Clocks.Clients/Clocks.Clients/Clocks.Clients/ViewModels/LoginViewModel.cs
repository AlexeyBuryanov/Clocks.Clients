using Clocks.Clients.Core.Services.Analytic;
using Clocks.Clients.Core.Services.Authentication;
using Clocks.Clients.Core.Validations;
using Clocks.Clients.Core.ViewModels.Base;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Clocks.Clients.Core.ViewModels
{
    /// <inheritdoc />
    /// <summary>
    /// Модель представления авторизации
    /// </summary>
    public class LoginViewModel : ViewModelBase
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
        /// Валидатор имени пользователя
        /// </summary>
        private ValidatableObject<string> _userName;
        /// <summary>
        /// Валидатор пароля
        /// </summary>
        private ValidatableObject<string> _password;
        public bool IsValid { get; set; }

        public LoginViewModel(
            INavigationService navigationService,
            IAnalyticService analyticService, 
            IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _navigationService = navigationService;
            _analyticService = analyticService;

            Title = "Авторизация";

            _userName = new ValidatableObject<string>();
            _password = new ValidatableObject<string>();
            AddValidations();
        }

        /// <summary>
        /// Валидатор имени пользователя св-во
        /// </summary>
        public ValidatableObject<string> UserName {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        /// <summary>
        /// Валидатор пароля св-во
        /// </summary>
        public ValidatableObject<string> Password {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public ICommand SignInCommand => new DelegateCommand(async () => await SignInAsync());
        /// <summary>
        /// Авторизация
        /// </summary>
        private async Task SignInAsync()
        {
            IsBusy = true;

            // Проверяем валидацию введённых данных
            IsValid = Validate();

            // Если всё ок
            if (IsValid)
            {
                // Выполняем вход
                var isAuth = await _authenticationService.LoginAsync(UserName.Value, Password.Value);

                // Если всё ок
                if (isAuth)
                {
                    IsBusy = false;

                    // Фиксируем авторизацию в аналитике
                    var props = new Dictionary<string, string>
                    {
                        {"AvatarUrl", _authenticationService.AuthenticatedUser.AvatarUrl},
                        {"Email", _authenticationService.AuthenticatedUser.Email},
                        {"Device", Device.RuntimePlatform}
                    };
                    _analyticService.TrackEvent("SignIn", props);

                    // Переходим на главную
                    await _navigationService.NavigateAsync("/MainView/NavigationPage/HomeView");
                }
            }

            MessagingCenter.Send(this, MessengerKeys.SignInRequested);

            IsBusy = false;
        }

        /// <summary>
        /// Добавить валидации
        /// </summary>
        private void AddValidations()
        {
            _userName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Имя пользователя не должно быть пустым" });
            _userName.Validations.Add(new EmailRule());
            _password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Пароль не должен быть пустым" });
        }

        /// <summary>
        /// Проверка ввода
        /// </summary>
        private bool Validate()
        {
            var isValidUser = _userName.Validate();
            var isValidPassword = _password.Validate();

            return isValidUser && isValidPassword;
        }
    }
}

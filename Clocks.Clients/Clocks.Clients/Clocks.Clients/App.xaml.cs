using Amporis.Xamarin.Forms.ColorPicker;
using Clocks.Clients.Core.Models.Database;
using Clocks.Clients.Core.Services.Analytic;
using Clocks.Clients.Core.Services.Authentication;
using Clocks.Clients.Core.Services.Dialog;
using Clocks.Clients.Core.Services.OpenUri;
using Clocks.Clients.Core.ViewModels;
using Clocks.Clients.Core.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;
using Prism;
using Prism.Ioc;
using Prism.Plugin.Popups;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Device = Xamarin.Forms.Device;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Clocks.Clients.Core
{
    public partial class App
    {
        public const string DBFILENAME = "clocks.db";

        private UnitOfWork _uow;

        public App() : this(null) { }
        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override void OnStart()
        {
            AppCenter.Start($"ios={AppSettings.AppCenterAnalyticsIos};" +
                            $"uwp={AppSettings.AppCenterAnalyticsWindows};" +
                            $"android={AppSettings.AppCenterAnalyticsAndroid}",
                typeof(Analytics), typeof(Crashes), typeof(Distribute));
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            await InitializeDatabaseAsync();

            // Стартовая навигация в зависимости от того авторизован пользователь или нет
            if (Container.Resolve(typeof(IAuthenticationService)) 
                    is IAuthenticationService authenticationService 
                && await authenticationService.IsAuthenticatedAsync())
            {
                await NavigationService.NavigateAsync("/MainView/NavigationPage/HomeView");
            }
            else
            {
                await NavigationService.NavigateAsync("/LoginView");
            }
        }

        private async Task InitializeDatabaseAsync()
        {
            _uow = UnitOfWork.Instance;

            if (Device.RuntimePlatform == Device.iOS)
            {
                SQLitePCL.Batteries.Init();
            }
            
            // Создаем бд, если она отсутствует
            await _uow.EnsureCreatedAsync();

            // Заполняем данными
            var isAnyCities = await _uow.Cities.AnyAsync();
            if (!isAnyCities)
            {
                await FillCitiesAsync();
            }

            var isAnyTypeClock = await _uow.ClockTypes.AnyAsync();
            if (!isAnyTypeClock)
            {
                await FillTypeClockAsync();
            }

            var isAnyClocks = await _uow.Clocks.AnyAsync();
            if (!isAnyClocks)
            {
                await FillClockAsync();
            }
        }

        private async Task FillTypeClockAsync()
        {
            await Task.WhenAll(
                _uow.ClockTypes.AddAsync(new ClockType { ClockTypeId = 1, Name = "Аналоговые" }),
                _uow.ClockTypes.AddAsync(new ClockType { ClockTypeId = 2, Name = "Песочные" }),
                _uow.ClockTypes.AddAsync(new ClockType { ClockTypeId = 3, Name = "С погодой" }),
                _uow.SaveChangesAsync()
            );
        }

        private async Task FillCitiesAsync()
        {
            await Task.WhenAll(
                _uow.Cities.AddAsync(new City { CityId = 1, Name = "Киев", Offset = 2 }),
                _uow.Cities.AddAsync(new City { CityId = 2, Name = "Москва", Offset = 3 }),
                _uow.Cities.AddAsync(new City { CityId = 3, Name = "Гавайи", Offset = -10 }),
                _uow.Cities.AddAsync(new City { CityId = 4, Name = "Аляска", Offset = -9 }),
                _uow.Cities.AddAsync(new City { CityId = 5, Name = "Соломоновы Острова", Offset = 11 }),
                _uow.Cities.AddAsync(new City { CityId = 6, Name = "Фиджи", Offset = 12 }),
                _uow.Cities.AddAsync(new City { CityId = 7, Name = "Лос-Анджелес", Offset = -8 }),
                _uow.Cities.AddAsync(new City { CityId = 8, Name = "Нью-Йорк", Offset = -5 }),
                _uow.Cities.AddAsync(new City { CityId = 9, Name = "Лондон", Offset = 0 }),
                _uow.Cities.AddAsync(new City { CityId = 10, Name = "Париж", Offset = 1 }),
                _uow.Cities.AddAsync(new City { CityId = 11, Name = "Пекин", Offset = 8 }),
                _uow.Cities.AddAsync(new City { CityId = 12, Name = "Токио", Offset = 9 }),
                _uow.SaveChangesAsync()
            );
        }

        private async Task FillClockAsync()
        {
            var black = Color.Black.ToHex();
            var gray = Color.Gray.ToHex();
            var darkRed = Color.DarkRed.ToHex();

            await Task.WhenAll(
                _uow.Clocks.AddAsync(new Clock { ClockId = 1, HourArrowColorHex = black, MinuteArrowColorHex = gray, SecondArrowColorHex = darkRed, TickMarksColorHex = black, CityId = 1, ClockTypeId = 1 }),
                _uow.Clocks.AddAsync(new Clock { ClockId = 2, HourArrowColorHex = black, MinuteArrowColorHex = gray, SecondArrowColorHex = darkRed, TickMarksColorHex = black, CityId = 2, ClockTypeId = 1 }),
                _uow.Clocks.AddAsync(new Clock { ClockId = 3, HourArrowColorHex = black, MinuteArrowColorHex = gray, SecondArrowColorHex = darkRed, TickMarksColorHex = black, CityId = 3, ClockTypeId = 1 }),
                _uow.SaveChangesAsync()
            );
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            /************************ Navigation ******************************/
            containerRegistry.RegisterPopupNavigationService();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<ExtendedSplashView, ExtendedSplashViewModel>();
            containerRegistry.RegisterForNavigation<LoginView, LoginViewModel>();
            containerRegistry.RegisterForNavigation<MainView, MainViewModel>();
            containerRegistry.RegisterForNavigation<HomeView, HomeViewModel>();
            containerRegistry.RegisterForNavigation<ClockAddPopupView, ClockAddPopupViewModel>();
            containerRegistry.RegisterForNavigation<ClockEditPopupView, ClockEditPopupViewModel>();
            /************************* Services *******************************/
            containerRegistry.RegisterSingleton(typeof(IAnalyticService), typeof(AnalyticService));
            containerRegistry.RegisterSingleton(typeof(IAuthenticationService), typeof(AuthenticationService));
            containerRegistry.RegisterSingleton(typeof(IAvatarUrlProvider), typeof(GravatarUrlProvider));
            containerRegistry.RegisterSingleton(typeof(IBrowserCookiesService), typeof(DefaultBrowserCookiesService));
            containerRegistry.RegisterSingleton(typeof(IDialogService), typeof(DialogService));
            containerRegistry.RegisterSingleton(typeof(IOpenUriService), typeof(OpenUriService));
        }
    }
}

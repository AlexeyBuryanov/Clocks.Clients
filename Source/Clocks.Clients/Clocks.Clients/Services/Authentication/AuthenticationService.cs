using System.Threading.Tasks;

namespace Clocks.Clients.Core.Services.Authentication
{
    /// <inheritdoc />
    /// <summary>
    /// Сервис аутентификации
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        /// <summary>
        /// Сервис куки
        /// </summary>
        private readonly IBrowserCookiesService _browserCookiesService;
        /// <summary>
        /// Поставщик аватарки
        /// </summary>
        private readonly IAvatarUrlProvider _avatarProvider;

        public AuthenticationService(
            IBrowserCookiesService browserCookiesService,
            IAvatarUrlProvider avatarProvider)
        {
            _browserCookiesService = browserCookiesService;
            _avatarProvider = avatarProvider;
        }

        /// <summary>
        /// Аутентифицирован ли
        /// </summary>
        public bool IsAuthenticated => AppSettings.User != null;

        /// <summary>
        /// Пользователь, который вошёл
        /// </summary>
        public Models.User AuthenticatedUser => AppSettings.User;

        /// <summary>
        /// Вход
        /// </summary>
        public Task<bool> LoginAsync(string email, string password)
        {
            var user = new Models.User
            {
                Email = email,
                Name = email,
                LastName = string.Empty,
                AvatarUrl = _avatarProvider.GetAvatarUrl(email)
            };

            AppSettings.User = user;

            return Task.FromResult(true);
        }

        /// <summary>
        /// Выход
        /// </summary>
        public async Task LogoutAsync()
        {
            AppSettings.RemoveUserData();
            await _browserCookiesService.ClearCookiesAsync();
        }

        public async Task<bool> IsAuthenticatedAsync() => await Task.FromResult(IsAuthenticated);
    }
}
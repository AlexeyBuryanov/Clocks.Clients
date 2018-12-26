using System.Threading.Tasks;
using Clocks.Clients.Core.Models;

namespace Clocks.Clients.Core.Services.Authentication
{
    /// <summary>
    /// Описывает службу входа
    /// </summary>
    public interface IAuthenticationService
    {
        bool IsAuthenticated { get; }

        User AuthenticatedUser { get; }

        Task<bool> LoginAsync(string email, string password);

        Task LogoutAsync();

        Task<bool> IsAuthenticatedAsync();
    }
}

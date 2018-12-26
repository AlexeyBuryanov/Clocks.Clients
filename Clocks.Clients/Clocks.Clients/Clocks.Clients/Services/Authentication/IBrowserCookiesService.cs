using System.Threading.Tasks;

namespace Clocks.Clients.Core.Services.Authentication
{
    /// <summary>
    /// Описывает службу по работе с куки
    /// </summary>
    public interface IBrowserCookiesService
    {
        Task ClearCookiesAsync();
    }
}

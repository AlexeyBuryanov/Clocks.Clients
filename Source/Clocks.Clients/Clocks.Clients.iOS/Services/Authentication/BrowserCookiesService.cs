using Clocks.Clients.Core.Services.Authentication;
using Foundation;
using System.Threading.Tasks;

namespace Clocks.Clients.iOS.Services
{
    public class BrowserCookiesService : IBrowserCookiesService
    {
        public Task ClearCookiesAsync()
        {
            var storage = NSHttpCookieStorage.SharedStorage;

            foreach (var cookie in storage.Cookies)
            {
                storage.DeleteCookie(cookie);
            }

            return Task.FromResult(true);
        }
    }
}
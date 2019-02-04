using Android.OS;
using Android.Webkit;
using Clocks.Clients.Core.Services.Authentication;
using System.Threading.Tasks;

namespace Clocks.Clients.Droid.Services.Authentication
{
    public class BrowserCookiesService : IBrowserCookiesService
    {
        public Task ClearCookiesAsync()
        {
            var context = Android.App.Application.Context;

            if (Build.VERSION.SdkInt >= BuildVersionCodes.LollipopMr1)
            {
                System.Diagnostics.Debug.WriteLine("Очистка куки для API >= LollipopMr1");
                CookieManager.Instance.RemoveAllCookies(null);
                CookieManager.Instance.Flush();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Очистка куки для API < LollipopMr1");
#pragma warning disable CS0618 // Type or member is obsolete
                var cookieSyncMngr = CookieSyncManager.CreateInstance(context);
#pragma warning restore CS0618 // Type or member is obsolete
#pragma warning disable 618
                cookieSyncMngr.StartSync();
#pragma warning restore 618
                var cookieManager = CookieManager.Instance;
                cookieManager.RemoveAllCookie();
                cookieManager.RemoveSessionCookie();
#pragma warning disable 618
                cookieSyncMngr.StopSync();
                cookieSyncMngr.Sync();
#pragma warning restore 618
            }

            return Task.FromResult(true);
        }
    }
}
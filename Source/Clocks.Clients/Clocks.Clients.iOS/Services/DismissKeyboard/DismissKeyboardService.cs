using Clocks.Clients.Core.Services.DismissKeyboard;
using Clocks.Clients.iOS.Services;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(DismissKeyboardService))]
namespace Clocks.Clients.iOS.Services
{
    public class DismissKeyboardService : IDismissKeyboardService
    {
        public void DismissKeyboard() => UIApplication.SharedApplication.InvokeOnMainThread(() =>
        {
            var window = UIApplication.SharedApplication.KeyWindow;
            var vc = window.RootViewController;
            while (vc.PresentedViewController != null)
            {
                vc = vc.PresentedViewController;
            }

            vc.View.EndEditing(true);
        });
    }
}
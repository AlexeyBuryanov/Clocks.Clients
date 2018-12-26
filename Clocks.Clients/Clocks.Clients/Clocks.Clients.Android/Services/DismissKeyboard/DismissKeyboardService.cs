using Android.App;
using Android.Views.InputMethods;
using Clocks.Clients.Core.Services.DismissKeyboard;
using Clocks.Clients.Droid.Services.DismissKeyboard;

[assembly: Xamarin.Forms.Dependency(typeof(DismissKeyboardService))]
namespace Clocks.Clients.Droid.Services.DismissKeyboard
{
    public class DismissKeyboardService : IDismissKeyboardService
    {
        public void DismissKeyboard()
        {
            var inputMethodManager = InputMethodManager.FromContext(Application.Context);

#pragma warning disable 618
            inputMethodManager.HideSoftInputFromWindow(
                ((Activity)Xamarin.Forms.Forms.Context).Window.DecorView.WindowToken, HideSoftInputFlags.NotAlways);
#pragma warning restore 618
        }
    }
}
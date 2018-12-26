using Windows.UI.ViewManagement;
using Clocks.Clients.Core.Services.DismissKeyboard;
using Clocks.Clients.UWP.Services.DismissKeyboard;

[assembly: Xamarin.Forms.Dependency(typeof(DismissKeyboardService))]
namespace Clocks.Clients.UWP.Services.DismissKeyboard
{
    public class DismissKeyboardService : IDismissKeyboardService
    {
        /// <summary>
        /// Попытка закрыть клавиатуру ввода, если она отображена
        /// </summary>
        public void DismissKeyboard()
        {
            InputPane.GetForCurrentView().TryHide();
        }
    }
}
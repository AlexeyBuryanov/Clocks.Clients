using Clocks.Clients.Core;
using Clocks.Clients.Core.Models.Database;
using Clocks.Clients.Core.Services.Authentication;
using Clocks.Clients.Core.Services.DismissKeyboard;
using Clocks.Clients.iOS.Database;
using Clocks.Clients.iOS.Services;
using Foundation;
using Prism;
using Prism.Ioc;
using UIKit;
using Xamarin.Forms.Platform.iOS;

namespace Clocks.Clients.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Xamarin.Forms.Forms.Init();
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
            Rg.Plugins.Popup.Popup.Init();
            InitXamanimation();
            LoadApplication(new App(new iOSInitializer()));

            base.FinishedLaunching(app, options);

            return true;
        }

        private static void InitXamanimation()
        {
            var t1 = typeof(Xamanimation.AnimationBase);
        }
    }

    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton(typeof(IPath), typeof(IosDbPath));
            containerRegistry.RegisterSingleton(typeof(IBrowserCookiesService), typeof(BrowserCookiesService));
            containerRegistry.RegisterSingleton(typeof(IDismissKeyboardService), typeof(DismissKeyboardService));
        }
    }
}

using Clocks.Clients.Core.Models.Database;
using Clocks.Clients.Core.Services.DismissKeyboard;
using Clocks.Clients.UWP.Database;
using Clocks.Clients.UWP.Services.DismissKeyboard;
using Prism;
using Prism.Ioc;
using CoreApp = Clocks.Clients.Core.App;

namespace Clocks.Clients.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
            Rg.Plugins.Popup.Popup.Init();

            LoadApplication(new CoreApp(new UwpInitializer()));
        }
    }

    public class UwpInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton(typeof(IPath), typeof(UwpDbPath));
            containerRegistry.RegisterSingleton(typeof(IDismissKeyboardService), typeof(DismissKeyboardService));
        }
    }
}

using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Util;
using Android.Views;
using Clocks.Clients.Core;
using Clocks.Clients.Core.Helpers;
using Prism;
using Prism.Ioc;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace Clocks.Clients.Droid
{
    [Activity(
        Label = "Clocks A.B.", 
        Icon = "@drawable/ic_launcher", 
        Theme = "@style/MainTheme", 
        MainLauncher = false,
        NoHistory = false,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            Forms.Init(this, bundle);
            Xamarin.Essentials.Platform.Init(this, bundle);
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(false);
            Acr.UserDialogs.UserDialogs.Init(this);
            Rg.Plugins.Popup.Popup.Init(this, bundle);
            InitMessageCenterSubscriptions();
            LoadApplication(new App(new AndroidInitializer()));
            MakeStatusBarTranslucent(false);
        }

        /// <summary>
        /// При запросе разрешения
        /// </summary>
        /// <param name="requestCode">код запроса</param>
        /// <param name="permissions">разрешения</param>
        /// <param name="grantResults">результаты</param>
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        /// <summary>
        /// Подписываемся на сообщения об изменении прозрачности статус-бару
        /// </summary>
        private void InitMessageCenterSubscriptions() => 
            MessagingCenter.Instance.Subscribe<StatusBarHelper, bool>(this, StatusBarHelper.TranslucentStatusChangeMessage, OnTranslucentStatusRequest);

        /// <summary>
        /// При запросе сделать статус-бар прозрачным
        /// </summary>
        private void OnTranslucentStatusRequest(StatusBarHelper helper, bool makeTranslucent) => 
            MakeStatusBarTranslucent(makeTranslucent);

        /// <summary>
        /// Сделать статус-бар прозрачным
        /// </summary>
        private void MakeStatusBarTranslucent(bool makeTranslucent)
        {
            // Если да
            if (makeTranslucent)
            {
                // Прозрачный цвет
                SetStatusBarColor(Android.Graphics.Color.Transparent);

                // Если SDK выше, чем Android 5.0
                if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                {
                    // Настраиваем UI с помощью двух флагов
                    Window.DecorView.SystemUiVisibility = (StatusBarVisibility)(SystemUiFlags.LayoutFullscreen | SystemUiFlags.LayoutStable);
                }
            }
            // Если нет
            else
            {
                using (var value = new TypedValue())
                {
                    // Запрашиваем тёмный цвет из темы и устанавливаем его статус-бару
                    if (Theme.ResolveAttribute(Resource.Attribute.colorPrimaryDark, value, true))
                    {
                        var color = new Android.Graphics.Color(value.Data);
                        SetStatusBarColor(color);
                    }
                }

                // Если SDK больше, чем Android 5.0
                if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                {
                    // Отображаем все UI
                    Window.DecorView.SystemUiVisibility = StatusBarVisibility.Visible;
                }
            }
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}
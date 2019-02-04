using Clocks.Clients.Core.Models;
using Clocks.Clients.Core.Utils;
using Xamarin.Essentials;

namespace Clocks.Clients.Core
{
	public static class AppSettings
	{
		private const string DefaultAppCenterAndroid = "2d559969-0cef-41e2-9a4d-97b6145170d7";
		private const string DefaultAppCenteriOS = "7af0374f-1309-4a23-88c7-cdf31dbdd371";
		private const string DefaultAppCenterUWP = "2f1addf1-8d34-40ce-a907-136c5e669a03";

		// Юзер
		public static User User
		{
			get => PreferencesHelpers.Get(nameof(User), default(User));
			set => PreferencesHelpers.Set(nameof(User), value);
		}
		public static void RemoveUserData() =>
			Preferences.Remove(nameof(User));

		// Аналитика App Center
		public static string AppCenterAnalyticsAndroid
		{
			get => Preferences.Get(nameof(AppCenterAnalyticsAndroid), DefaultAppCenterAndroid);
			set => Preferences.Set(nameof(AppCenterAnalyticsAndroid), value);
		}
		public static string AppCenterAnalyticsIos
		{
			get => Preferences.Get(nameof(AppCenterAnalyticsIos), DefaultAppCenteriOS);
			set => Preferences.Set(nameof(AppCenterAnalyticsIos), value);
		}
		public static string AppCenterAnalyticsWindows
		{
			get => Preferences.Get(nameof(AppCenterAnalyticsWindows), DefaultAppCenterUWP);
			set => Preferences.Set(nameof(AppCenterAnalyticsWindows), value);
		}
	}
}

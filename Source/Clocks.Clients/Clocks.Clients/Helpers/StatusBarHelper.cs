using Xamarin.Forms;

namespace Clocks.Clients.Core.Helpers
{
    /// <summary>
    /// Помощник для работы со статус-баром
    /// </summary>
    public class StatusBarHelper
    {
        private static readonly StatusBarHelper _instance = new StatusBarHelper();
        public const string TranslucentStatusChangeMessage = "TranslucentStatusChange";

        public static StatusBarHelper Instance => _instance;

        private StatusBarHelper() { }

        /// <summary>
        /// Сделать статус-бар прозрачным
        /// </summary>
        public void MakeTranslucentStatusBar(bool translucent) => 
            MessagingCenter.Send(this, TranslucentStatusChangeMessage, translucent);
    }
}

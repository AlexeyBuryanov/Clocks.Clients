using Clocks.Clients.Core.Helpers;

namespace Clocks.Clients.Core.Views
{
    public partial class LoginView
    {
        public LoginView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            StatusBarHelper.Instance.MakeTranslucentStatusBar(true);
        }
    }
}

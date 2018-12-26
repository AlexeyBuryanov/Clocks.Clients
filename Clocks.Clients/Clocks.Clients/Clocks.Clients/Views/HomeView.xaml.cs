using Clocks.Clients.Core.Helpers;

namespace Clocks.Clients.Core.Views
{
    public partial class HomeView
    {
        public HomeView()
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

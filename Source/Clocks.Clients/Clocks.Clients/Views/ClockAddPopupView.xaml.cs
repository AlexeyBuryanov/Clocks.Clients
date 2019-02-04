namespace Clocks.Clients.Core.Views
{
    public partial class ClockAddPopupView
    {
        public ClockAddPopupView()
        {
            InitializeComponent();
        }

        protected override bool OnBackgroundClicked()
        {
            return false;
        }

        protected override bool OnBackButtonPressed()
        {
            return false;
        }
    }
}

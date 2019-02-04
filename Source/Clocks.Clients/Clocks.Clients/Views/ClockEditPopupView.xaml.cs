namespace Clocks.Clients.Core.Views
{
    public partial class ClockEditPopupView
    {
        public ClockEditPopupView()
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

using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Clocks.Clients.Core.Views.Templates
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ClockItemTemplate
	{
		public static readonly BindableProperty DeleteCommandProperty =
			BindableProperty.Create(
				"DeleteCommand",
				typeof(ICommand),
				typeof(ClockItemTemplate),
				default(ICommand));

		public ICommand DeleteCommand {
			get => (ICommand)GetValue(DeleteCommandProperty);
			set => SetValue(DeleteCommandProperty, value);
		}

		public static readonly BindableProperty ItemTappedCommandProperty =
			BindableProperty.Create(
				"ItemTappedCommand",
				typeof(ICommand),
				typeof(ClockItemTemplate),
				default(ICommand));

		public ICommand ItemTappedCommand {
			get => (ICommand)GetValue(ItemTappedCommandProperty);
			set => SetValue(ItemTappedCommandProperty, value);
		}

		public ClockItemTemplate()
		{
			InitializeComponent();

			var tapGestureItemTapped = new TapGestureRecognizer
			{
				Command = new Command(() => ItemTappedCommand?.Execute(BindingContext))
			};
			ItemContainer.GestureRecognizers.Add(tapGestureItemTapped);

			var tapGestureDelete = new TapGestureRecognizer
			{
				Command = new Command(OnDeleteTapped)
			};
			DeleteContainer.GestureRecognizers.Add(tapGestureDelete);

			InitializeCell();
		}

		private ICommand TransitionCommand => new Command(async () =>
		{
			var isUwp = Device.RuntimePlatform == Device.UWP;

			DeleteContainer.BackgroundColor = Color.FromHex("#EC0843");
			DeleteImage.Source = isUwp ? "Assets/ic_paperbin.png" : "ic_paperbin";

			await this.TranslateTo(-Width, 0, 500, Easing.SinIn);

			DeleteCommand?.Execute(BindingContext);

			InitializeCell();
		});

		private void OnDeleteTapped() => TransitionCommand.Execute(null);

		private void InitializeCell()
		{
			var isUwp = Device.RuntimePlatform == Device.UWP;

			TranslationX = 0;
			DeleteContainer.BackgroundColor = Color.FromHex("#F2F2F2");
			DeleteImage.Source = isUwp ? "Assets/ic_paperbin_red.png" : "ic_paperbin_red";
		}
	}
}
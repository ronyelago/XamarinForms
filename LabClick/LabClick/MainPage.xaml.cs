using LabClick.Views;
using Xamarin.Forms;

namespace LabClick
{
    public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
        }

        private async void btnNovoPaciente_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new NovoPacientePage());
        }
    }
}

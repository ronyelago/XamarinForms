using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LabClick.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NovoPacientePage : ContentPage
	{
		public NovoPacientePage ()
		{
			InitializeComponent ();
		}

        private async void btnCancelar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        private void btnSalvar_Clicked(object sender, EventArgs e)
        {

        }
    }
}
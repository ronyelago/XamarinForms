using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LabClick.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
		public Login ()
		{
			InitializeComponent ();
		}

        private async void btnEntrar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Home());
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
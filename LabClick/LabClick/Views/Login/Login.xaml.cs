using LabClick.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LabClick.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
        private LoginViewModel loginViewModel = new LoginViewModel();

		public Login ()
		{
			InitializeComponent ();
            this.BindingContext = loginViewModel;

            if (Device.Idiom == TargetIdiom.Tablet)
            {
                //Sizes to tablet
                MainStackLayout.WidthRequest = 300;

                ImageLogo.WidthRequest = 400;
                ImageLogo.HeightRequest = 400;

                btnEntrar.WidthRequest = 250;
                btnEntrar.HeightRequest = 50;
                btnEntrar.FontSize = 20;
                btnEntrar.CornerRadius = 10;

                emailEntry.WidthRequest = 270;
                emailEntry.HeightRequest = 30;


                senhaEntry.WidthRequest = 270;
                senhaEntry.HeightRequest = 30;
            }
            else if (Device.Idiom == TargetIdiom.Phone)
            {
                //Sizes to phone
                MainStackLayout.WidthRequest = 150;

                ImageLogo.WidthRequest = 200;
                ImageLogo.HeightRequest = 200;

                btnEntrar.WidthRequest = 50;
                btnEntrar.HeightRequest = 30;
                btnEntrar.FontSize = 7;
                btnEntrar.CornerRadius = 5;

                emailEntry.WidthRequest = 135;
                emailEntry.HeightRequest = 20;

                senhaEntry.WidthRequest = 135;
                senhaEntry.HeightRequest = 30;
            }
        }

        private async void btnEntrar_Clicked(object sender, EventArgs e)
        {



            await Navigation.PushAsync(new Home());
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
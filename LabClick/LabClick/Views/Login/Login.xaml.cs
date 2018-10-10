using AutoMapper;
using LabClick.Services;
using LabClick.ViewModels;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LabClick.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
        private LoginViewModel loginViewModel = new LoginViewModel();
        private LoginService loginService = new LoginService();

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
            if (loginViewModel.Email != null && loginViewModel.Senha != null )
            {
                var user = Mapper.Map<Domain.Entities.Usuario>(loginViewModel);

                Uri urinho = new Uri($@"http://192.168.0.15:3000/usuario/getbyemail={user.Email}");

                var result = App.Client.GetAsync(urinho);

                if (result.Result.IsSuccessStatusCode)
                {
                    var content = result.Result.Content.ReadAsStringAsync();
                    var userGot = JsonConvert.DeserializeObject<Domain.Entities.Usuario>(content.Result);

                    if (userGot.Senha == user.Senha)
                    {
                        App.User = userGot;
                        await Navigation.PushAsync(new Home());
                        NavigationPage.SetHasNavigationBar(this, false);
                    }
                    else
                    {
                        await DisplayAlert("Erro", "Senha inválida.", "Fechar");
                    }
                }
                else
                {
                    await DisplayAlert("Erro", "Usuário inválido.", "Fechar");
                }
            }
            else
            {
                await DisplayAlert("Erro", "Digite seu e-mail e senha.", "Fechar");
            }
        }
    }
}
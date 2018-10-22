using AutoMapper;
using LabClick.Services;
using LabClick.ViewModels;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading;
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
            NavigationPage.SetHasNavigationBar(this, false);

            if (Device.Idiom == TargetIdiom.Tablet)
            {
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
                ImageLogo.WidthRequest = 200;
                ImageLogo.HeightRequest = 200;

                btnEntrar.WidthRequest = 50;
                btnEntrar.HeightRequest = 30;
                btnEntrar.FontSize = 7;
                btnEntrar.CornerRadius = 3;

                emailEntry.WidthRequest = 300;
                emailEntry.HeightRequest = 40;
                emailEntry.FontSize = 20;

                senhaEntry.WidthRequest = 300;
                senhaEntry.HeightRequest = 40;
                senhaEntry.FontSize = 20;
            }
        }

        private async void btnEntrar_Clicked(object sender, EventArgs e)
        {
            if (loginViewModel.Email != null && loginViewModel.Senha != null )
            {
                btnEntrar.IsVisible = false;
                LoginIndicator.IsRunning = true;
                Thread.Sleep(2000);

                var user = Mapper.Map<Domain.Entities.UsuarioClinica>(loginViewModel);

                Uri urinho = new Uri($@"http://apilabclick.mflogic.com.br/usuarioclinica/getbyemail={user.Email}");

                try
                {
                    var result = await App.Client.GetAsync(urinho);

                    if (result.IsSuccessStatusCode)
                    {
                        var content = result.Content.ReadAsStringAsync();
                        var userGot = JsonConvert.DeserializeObject<Domain.Entities.UsuarioClinica>(content.Result);

                        if (userGot.Senha == user.Senha)
                        {
                            App.User = userGot;
                            await Navigation.PushAsync(new Home());
                            NavigationPage.SetHasNavigationBar(this, false);

                            btnEntrar.IsVisible = true;
                            LoginIndicator.IsRunning = false;
                        }
                        else
                        {
                            await DisplayAlert("Erro", "Senha inválida.", "Fechar");
                            btnEntrar.IsVisible = true;
                            LoginIndicator.IsRunning = false;
                        }
                    }
                    else
                    {
                        await DisplayAlert("Erro", "Usuário inválido.", "Fechar");
                        btnEntrar.IsVisible = true;
                        LoginIndicator.IsRunning = false;
                    }
                }
                catch (HttpRequestException ex)
                {
                    await DisplayAlert("Erro", "Falha ao tentar estabelecer conexão com a internet.", "Fechar");
                    btnEntrar.IsVisible = true;
                    LoginIndicator.IsRunning = false;
                }
            }
            else
            {
                await DisplayAlert("Erro", "Digite seu e-mail e senha.", "Fechar");
                btnEntrar.IsVisible = true;
                LoginIndicator.IsRunning = false;
            }
        }
    }
}
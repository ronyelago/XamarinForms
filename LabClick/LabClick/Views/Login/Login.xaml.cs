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
            //if (txtUsuario.Text != null && txtUsuario.Text != string.Empty && 
            //    txtSenha.Text != null && txtSenha.Text != string.Empty)
            //{
                await Navigation.PushAsync(new MainPage());
            //}
            //else
            //{
            //    await DisplayAlert("Erro", "Preencha os campos de Usuário e senha.", "Ok");
            //}
        }
    }
}
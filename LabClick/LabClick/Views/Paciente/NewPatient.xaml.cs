using LabClick.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LabClick.Views.Paciente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewPatient : ContentPage
	{
        public NewPatientViewModel NewPatientViewModel { get; set; }
        public NewPatient ()
		{
			InitializeComponent ();

            this.NewPatientViewModel = new NewPatientViewModel();
            this.BindingContext = NewPatientViewModel;

            this.dataNascimentoPicker.Date = DateTime.Now;
            this.txtNumero.Text = string.Empty;
            this.dataNascimentoPicker.MaximumDate = DateTime.Now;

            if (Device.Idiom == TargetIdiom.Phone)
            {
                mainImage.HeightRequest = 40;
                MainLabel.FontSize = 20;

                txtNome.WidthRequest = 250;
                txtNome.FontSize = 15;

                lblDataNascimento.FontSize = 15;
                dataNascimentoPicker.FontSize = 15;

                txtCpf.WidthRequest = 250;
                txtCpf.FontSize = 15;

                pickerGenero.WidthRequest = 250;
                pickerGenero.FontSize = 15;

                txtEmail.WidthRequest = 250;
                txtEmail.FontSize = 15;

                txtPhone.WidthRequest = 250;
                txtPhone.FontSize = 15;

                enderecoStack.Margin = new Thickness(50, 10, 20, 10);

                TxtCep.WidthRequest = 250;

                lblUf.FontSize = 13;
                lblUf.Margin = new Thickness(0, 0, 10, 0);
                txtUf.WidthRequest = 250;

                txtCidade.WidthRequest = 230;
                txtBairro.WidthRequest = 230;
                txtRua.WidthRequest = 250;
                txtNumero.WidthRequest = 230;

            }
		}

        private void BtnSalvar_Clicked(object sender, EventArgs e)
        {
            if (this.NewPatientViewModel.IsValid())
            {
                var result = NewPatientViewModel.Add(NewPatientViewModel);

                if (result)
                {
                    DisplayAlert("Sucesso", "Paciente adicionado com sucesso.", "Fechar");
                    Navigation.PushAsync(new Home());
                }

                else
                {
                    DisplayAlert("Erro", "Erro ao tentar adicionar o paciente.", "Fechar");
                }
            }

            else
            {
                DisplayAlert("Erro", "Favor preencher todos os campos", "Fechar");
            }
        }

        private void TxtCep_Unfocused(object sender, FocusEventArgs e)
        {
            var address = NewPatientViewModel.GetAddress(TxtCep.Text);

            if (address != null)
            {
                this.NewPatientViewModel.Cidade = address.Cidade;
                this.NewPatientViewModel.UF = address.UF;
                this.NewPatientViewModel.Bairro = address.Bairro;
                this.NewPatientViewModel.Logradouro = address.Logradouro;
            }

            else
            {
                DisplayAlert("Erro", "Endereço não localizado. Tente novamente ou preencha manualmente.", "Fechar");
            }
        }

        private void BtnCancelar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Home());
        }
    }
}
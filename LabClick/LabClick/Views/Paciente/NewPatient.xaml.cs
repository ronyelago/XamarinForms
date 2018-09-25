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

            this.dataNascimento.Date = DateTime.Now;
            this.txtNumero.Text = string.Empty;
            this.dataNascimento.MaximumDate = DateTime.Now;

            if (Device.Idiom == TargetIdiom.Phone)
            {
                mainImage.HeightRequest = 40;
                MainLabel.FontSize = 20;

                CepStack.Margin = new Thickness(10, 5, 10, 5);
                TxtCep.WidthRequest = 200;
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
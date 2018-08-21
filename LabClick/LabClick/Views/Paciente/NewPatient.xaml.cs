using LabClick.ViewModels;
using Newtonsoft.Json;
using System;
using System.Net.Http;
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
		}

        private void BtnSalvar_Clicked(object sender, EventArgs e)
        {
            if (this.NewPatientViewModel.IsValid())
            {
                var result = NewPatientViewModel.Add(NewPatientViewModel);

                if (result)
                {
                    DisplayAlert("Sucesso", "Paciente adicionado com sucesso.", "Fechar");
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
            if (TxtCep.Text != null && TxtCep.Text != string.Empty)
            {
                Uri urinho = new Uri($@"http://viacep.com.br/ws/{TxtCep.Text}/json/");
                HttpClient client = new HttpClient();

                var result = client.GetAsync(urinho);

                if (result.Result.IsSuccessStatusCode)
                {
                    var content = result.Result.Content.ReadAsStringAsync();
                    var endereco = JsonConvert.DeserializeObject<EnderecoViewModel>(content.Result);

                    if (endereco.IsValid())
                    {
                        txtUf.Text = endereco.Uf;
                        txtCidade.Text = endereco.Localidade;
                        txtBairro.Text = endereco.Bairro;
                        txtRua.Text = endereco.Logradouro;

                        txtNumero.Focus();
                    }
                    else
                    {
                        DisplayAlert("Erro", "Endereço não localizado. Tente novamente ou preencha manualmente.", "Fechar");
                    }
                }
                else
                {
                    DisplayAlert("Erro", "Endereço não localizado. Tente novamente ou preencha manualmente.", "Fechar");
                }
            }
        }

        private void BtnCancelar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Home());
        }
    }
}
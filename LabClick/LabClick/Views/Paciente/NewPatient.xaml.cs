using AutoMapper;
using LabClick.ViewModels;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
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
            this.BindingContext = new NewPatientViewModel();
            this.dataNascimento.Date = DateTime.Now;
            this.txtNumero.Text = string.Empty;
            this.dataNascimento.MaximumDate = DateTime.Now;
		}

        private void BtnSalvar_Clicked(object sender, EventArgs e)
        {
            //BackgroundColor = #80126683

            Domain.Entities.Paciente paciente;
            Domain.Entities.Endereco endereco;
            var newPatientViewModel = this.BindingContext as NewPatientViewModel;

            //
            if (newPatientViewModel.IsValid())
            {
                HttpClient client = new HttpClient();

                paciente = Mapper.Map<Domain.Entities.Paciente>(newPatientViewModel);
                endereco = Mapper.Map<Domain.Entities.Endereco>(newPatientViewModel);

                //Cadastro do Endereço
                var enderecoSerialized = JsonConvert.SerializeObject(endereco);
                var contentinho = new StringContent(enderecoSerialized, Encoding.UTF8, "application/json");
                Uri urinho = new Uri(@"http://apilabclick.mflogic.com.br/endereco/enderecos");

                var resultado = client.PostAsync(urinho, contentinho);
                var enderecoId = int.Parse(resultado.Result.Content.ReadAsStringAsync().Result);

                //Cadastro do Paciente
                paciente.ClinicaId = 1;
                paciente.EnderecoId = enderecoId;

                var pacienteSerialized = JsonConvert.SerializeObject(paciente);

                Uri uri = new Uri(@"http://apilabclick.mflogic.com.br/paciente/pacientes");
                var content = new StringContent(pacienteSerialized, Encoding.UTF8, "application/json");

                var result = client.PostAsync(uri, content);

                if (result.Result.IsSuccessStatusCode)
                {
                    DisplayAlert("Sucesso", "Paciente cadastrado com sucesso.", "Fechar");
                    Navigation.PushAsync(new Home());
                }
                else
                {
                    DisplayAlert("Erro", "Nãu foi possível realizar o cadastro.", "Fechar");
                }
            }

            else
            {
                DisplayAlert("Erro","Favor preencher todos os campos","Fechar");
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
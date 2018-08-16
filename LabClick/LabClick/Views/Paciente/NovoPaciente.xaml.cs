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
	public partial class NovoPaciente : ContentPage
	{
		public NovoPaciente ()
		{
			InitializeComponent ();
		}

        private async void BtnCancelar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Home());
        }

        private void BtnSalvar_Clicked(object sender, EventArgs e)
        {
            PacienteViewModel pacienteViewModel = new PacienteViewModel();
            pacienteViewModel.Paciente.Nome = txtNome.Text;
            pacienteViewModel.Paciente.DataNascimento = dataNascimento.Date;
            pacienteViewModel.Paciente.Sexo = pickerGenero.SelectedItem.ToString();
            pacienteViewModel.Paciente.Email = txtEmail.Text;
            pacienteViewModel.Paciente.Telefone = txtPhone.Text;
            pacienteViewModel.Paciente.Cpf = txtCpf.Text;
            pacienteViewModel.Endereco.Cep = txtCep.Text;
            pacienteViewModel.Endereco.UF = txtUf.Text;
            pacienteViewModel.Endereco.Cidade = txtCidade.Text;
            pacienteViewModel.Endereco.Bairro = txtBairro.Text;
            pacienteViewModel.Endereco.Logradouro = txtRua.Text;
            pacienteViewModel.Endereco.Numero = int.Parse(txtNumero.Text);

            HttpClient client = new HttpClient();

            //Cadastro do Endereço
            Domain.Entities.Endereco endereco = pacienteViewModel.Endereco;
            var enderecoSerialized = JsonConvert.SerializeObject(endereco);
            var contentinho = new StringContent(enderecoSerialized, Encoding.UTF8, "application/json");
            Uri urinho = new Uri(@"http://apilabclick.mflogic.com.br/endereco/enderecos");

            var resultado = client.PostAsync(urinho, contentinho);
            var enderecoId = int.Parse(resultado.Result.Content.ReadAsStringAsync().Result);

            //Cadastro do Paciente
            Domain.Entities.Paciente paciente = pacienteViewModel.Paciente;
            paciente.ClinicaId = 1;
            paciente.EnderecoId = enderecoId;

            var pacienteSerialized = JsonConvert.SerializeObject(paciente);

            Uri uri = new Uri(@"http://apilabclick.mflogic.com.br/paciente/pacientes");
            var content = new StringContent(pacienteSerialized, Encoding.UTF8, "application/json");
            
            var result = client.PostAsync(uri, content);

            if (result.Result.IsSuccessStatusCode)
            {
                DisplayAlert("Sucesso", "Paciente cadastrado com sucesso.", "Ok");
                Navigation.PushAsync(new Home());
            }
            else
            {
                DisplayAlert("Erro", "Nãu foi possível realizar o cadastro.", "Ok");
            }
        }

        private void txtCep_Unfocused(object sender, FocusEventArgs e)
        {
            if (txtCep.Text != null && txtCep.Text != string.Empty)
            {
                Uri urinho = new Uri($@"http://viacep.com.br/ws/{txtCep.Text}/json/");
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
                        DisplayAlert("Erro", "Endereço não localizado. Tente novamente ou preencha manualmente.", "Ok");
                    }
                }
                else
                {
                    DisplayAlert("Erro", "Endereço não localizado. Tente novamente ou preencha manualmente.", "Ok");
                }
            }
        }
    }
}
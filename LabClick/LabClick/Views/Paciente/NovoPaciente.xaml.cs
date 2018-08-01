using LabClick.Models;
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
            await Navigation.PushAsync(new MainPage());
        }

        private void BtnSalvar_Clicked(object sender, EventArgs e)
        {
            PacienteViewModel pacienteViewModel = new PacienteViewModel();
            pacienteViewModel.Paciente.Nome = $"{txtNome.Text} {txtSobrenome.Text}";
            pacienteViewModel.Paciente.DataNascimento = dataNascimento.Date;
            pacienteViewModel.Paciente.Sexo = pickerGenero.SelectedItem.ToString();
            pacienteViewModel.Paciente.Cpf = txtCpf.Text;
            pacienteViewModel.Endereco.Cep = txtCep.Text;
            pacienteViewModel.Endereco.UF = txtUf.Text;
            pacienteViewModel.Endereco.Cidade = txtCidade.Text;
            pacienteViewModel.Endereco.Bairro = txtBairro.Text;
            pacienteViewModel.Endereco.Logradouro = txtRua.Text;
            pacienteViewModel.Endereco.Numero = int.Parse(txtNumero.Text);

            HttpClient client = new HttpClient();

            //Cadastro do Endereço
            Endereco endereco = pacienteViewModel.Endereco;
            var enderecoSerialized = JsonConvert.SerializeObject(endereco);
            var contentinho = new StringContent(enderecoSerialized, Encoding.UTF8, "application/json");
            Uri urinho = new Uri(@"http://apilabclick.mflogic.com.br/endereco/enderecos");

            var resultado = client.PostAsync(urinho, contentinho);
            var enderecoId = int.Parse(resultado.Result.Content.ReadAsStringAsync().Result);

            //Cadastro do Paciente
            Models.Paciente paciente = pacienteViewModel.Paciente;
            paciente.ClinicaId = 1;
            paciente.EnderecoId = enderecoId;

            var pacienteSerialized = JsonConvert.SerializeObject(paciente);

            Uri uri = new Uri(@"http://apilabclick.mflogic.com.br/paciente/pacientes");
            var content = new StringContent(pacienteSerialized, Encoding.UTF8, "application/json");
            
            var result = client.PostAsync(uri, content);

            if (result.Result.IsSuccessStatusCode)
            {
                DisplayAlert("Sucesso", "Paciente cadastrado com sucesso.", "Ok");
                Navigation.PushAsync(new MainPage());

            }

            else
            {
                DisplayAlert("Erro", "Nãu foi possível realizar o cadastro.", "Ok");
            }
        }
    }
}
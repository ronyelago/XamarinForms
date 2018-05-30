using LabClick.Models;
using LabClick.ViewModels;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LabClick.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NovoPacientePage : ContentPage
	{
		public NovoPacientePage ()
		{
			InitializeComponent ();
		}

        private async void btnCancelar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        private void btnSalvar_Clicked(object sender, EventArgs e)
        {
            PacienteViewModel pacienteViewModel = new PacienteViewModel();
            pacienteViewModel.Paciente.Nome = $"{txtNome.Text} {txtSobrenome.Text}";
            pacienteViewModel.Paciente.DataNascimento = dataNascimento.Date;
            pacienteViewModel.Paciente.Sexo = pickerGenero.SelectedItem.ToString();
            pacienteViewModel.Paciente.Cpf = "234.234.234-23";
            pacienteViewModel.Endereco.Cep = txtCep.Text;
            pacienteViewModel.Endereco.UF = txtUf.Text;
            pacienteViewModel.Endereco.Cidade = txtCidade.Text;
            pacienteViewModel.Endereco.Bairro = txtBairro.Text;
            pacienteViewModel.Endereco.Logradouro = txtRua.Text;
            pacienteViewModel.Endereco.Numero = int.Parse(txtNumero.Text);

            Paciente paciente = pacienteViewModel.Paciente;
            var pacienteSerialized = JsonConvert.SerializeObject(paciente);

            HttpClient client = new HttpClient();
            Uri uri = new Uri(@"http://192.168.0.15:3000/paciente/pacientes");
            var content = new StringContent(pacienteSerialized, Encoding.UTF8, "application/json");

            var result = client.PostAsync(uri, content);

            if (result.Result.IsSuccessStatusCode)
            {
                DisplayAlert("Sucesso", "Paciente cadastrado com sucesso!", "Ok");
                Navigation.PushAsync(new MainPage());
            }
        }
    }
}
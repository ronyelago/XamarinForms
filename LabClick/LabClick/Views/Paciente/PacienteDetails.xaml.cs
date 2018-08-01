using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LabClick.Views.Paciente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PacienteDetails : ContentPage
	{
		public PacienteDetails (Domain.Entities.Paciente paciente)
		{
			InitializeComponent ();

            txtNome.Text = paciente.Nome;
            dataNascimento.Date = paciente.DataNascimento;
            pickerGenero.SelectedItem = paciente.Sexo;

            if (paciente.Endereco != null)
            {
                txtCep.Text = paciente.Endereco.Cep;
                txtUf.Text = paciente.Endereco.UF;
                txtCidade.Text = paciente.Endereco.Cidade;
                txtBairro.Text = paciente.Endereco.Bairro;
                txtRua.Text = paciente.Endereco.Logradouro;
                txtNumero.Text = paciente.Endereco.Numero.ToString();
            }

        }

        private void ContentPage_Appearing(object sender, System.EventArgs e)
        {
            
        }
    }
}
using LabClick.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LabClick.Views.PacientePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PacienteDetails : ContentPage
	{
		public PacienteDetails (Paciente paciente)
		{
			InitializeComponent ();

            this.txtNome.Text = paciente.Nome;
		}
	}
}
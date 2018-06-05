using LabClick.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LabClick.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PacientePage : ContentPage
	{
		public PacientePage (Paciente paciente)
		{
			InitializeComponent ();

            txtNome.Text = paciente.Nome;
		}
	}
}
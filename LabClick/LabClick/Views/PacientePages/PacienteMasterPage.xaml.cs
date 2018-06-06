using LabClick.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LabClick.Views.PacientePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PacienteMasterPage : MasterDetailPage
	{
		public PacienteMasterPage (Paciente paciente)
		{
			InitializeComponent ();
		}
	}
}

using LabClick.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LabClick.Views.Paciente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PacienteMainPage : MasterDetailPage
	{
		public PacienteMainPage (Models.Paciente paciente)
		{
			InitializeComponent ();

            this.Master = new PacienteMasterDetailPage(paciente);
            this.Detail = new NavigationPage(new PacienteDetails(paciente));
            App.PacientMasterDetailPage = this;
		}
	}
}
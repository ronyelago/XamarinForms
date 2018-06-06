
using LabClick.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LabClick.Views.PacientePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PacienteMainPage : MasterDetailPage
	{
		public PacienteMainPage (Paciente paciente)
		{
			InitializeComponent ();

            this.Master = new PacienteMasterDetailPage();
            this.Detail = new NavigationPage(new PacienteDetails(paciente));
            App.PacientMasterDetailPage = this;
		}
	}
}
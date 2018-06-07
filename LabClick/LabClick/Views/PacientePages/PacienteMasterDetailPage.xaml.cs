using LabClick.Models;
using LabClick.Views.ExamePages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LabClick.Views.PacientePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PacienteMasterDetailPage : ContentPage
	{
        private Paciente paciente;

		public PacienteMasterDetailPage (Paciente paciente)
		{
			InitializeComponent ();
            this.paciente = paciente;
		}

        private async void BtnExames_Clicked(object sender, System.EventArgs e)
        {
            await App.NavigateMasterDetail(new ExamesPage(paciente));
        }

        private async void BtnNovoExame_Clicked(object sender, System.EventArgs e)
        {
            await App.NavigateMasterDetail(new NovoExamePage(paciente));
        }
    }
}
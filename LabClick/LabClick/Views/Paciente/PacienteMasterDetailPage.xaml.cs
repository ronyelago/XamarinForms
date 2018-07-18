using LabClick.Views.Teste;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LabClick.Views.Paciente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PacienteMasterDetailPage : ContentPage
	{
        private Models.Paciente paciente;

		public PacienteMasterDetailPage(Models.Paciente paciente)
		{
			InitializeComponent ();
            this.paciente = paciente;
		}

        private async void BtnExames_Clicked(object sender, System.EventArgs e)
        {
            await App.NavigateMasterDetail(new ListaTeste(paciente));
        }

        private async void BtnNovoExame_Clicked(object sender, System.EventArgs e)
        {
            await App.NavigateMasterDetail(new NovoTeste(paciente));
        }
    }
}
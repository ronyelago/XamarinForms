using LabClick.Views.Paciente;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LabClick.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Home : ContentPage
	{
		public Home ()
		{
			InitializeComponent ();
		}

        private void NovoTeste_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new PesquisarPaciente());
        }

        private void NovoPaciente_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new NovoPaciente());
        }

        private void PesquisarPaciente_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new PesquisarPaciente());
        }

        private void PesquisarLaudo_Clicked(object sender, System.EventArgs e)
        {

        }
    }
}
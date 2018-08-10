using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LabClick.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Home : ContentPage
	{
        public TabbedPage Main { get; set; }

        public Home ()
		{
			InitializeComponent ();
        }

        private void NovoTeste_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MainTab(0));
        }

        private void NovoPaciente_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MainTab(1));
        }

        private void PesquisarPaciente_Clicked(object sender, System.EventArgs e)
        {
            
        }

        private void PesquisarLaudo_Clicked(object sender, System.EventArgs e)
        {

        }
    }
}
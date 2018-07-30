using LabClick.Views.Paciente;
using Xamarin.Forms;

namespace LabClick
{
    public partial class MainPage : TabbedPage
	{
		public MainPage()
		{
			InitializeComponent();
        }

        private void BtnNovoPaciente_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new NovoPaciente());
        }

        private void BtnPesquisarPaciente_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new PesquisarPaciente());
        }
    }
}

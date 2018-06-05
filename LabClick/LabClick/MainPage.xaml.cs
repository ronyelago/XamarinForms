using LabClick.Views;
using Xamarin.Forms;

namespace LabClick
{
    public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
        }

        private void BtnNovoPaciente_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new NovoPacientePage());
        }

        private void BtnPesquisarPaciente_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new PesquisarPacientePage());
        }
    }
}

using BottomBar.XamarinForms;
using LabClick.Views.Paciente;
using Xamarin.Forms;

namespace LabClick
{
    public partial class MainPage : BottomBarPage
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

        private void ContentPage_Focused(object sender, FocusEventArgs e)
        {
            DisplayAlert("hello", "", "");
        }
    }
}

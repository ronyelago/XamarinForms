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

            if (Device.Idiom == TargetIdiom.Phone)
            {
                DigitalizarExameImg.WidthRequest = 100;
                DigitalizarExameImg.HeightRequest = 100;

                PesquisarPacienteImg.WidthRequest = 100;
                PesquisarPacienteImg.HeightRequest = 100;

                NovoPacienteImg.WidthRequest = 100;
                NovoPacienteImg.HeightRequest = 100;

                listaLaudosImg.WidthRequest = 100;
                listaLaudosImg.HeightRequest = 100;
            }
        }

        // Cada um dos eventos abaixo passa um número como parâmetro
        // quando estancia a MainTab que indica qual página deve ser
        // selecionada para exibição

        private void DigitalizarExameImg_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MainTab(0));
        }

        private void PesquisarPacienteImg_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MainTab(1));
        }

        private void NovoPacienteImg_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MainTab(2));
        }

        private void listaLaudosImg_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MainTab(3));
        }
    }
}
using LabClick.Views.ExamePages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LabClick.Views.PacientePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PacienteMasterDetailPage : ContentPage
	{
		public PacienteMasterDetailPage ()
		{
			InitializeComponent ();
		}

        private void BtnExames_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new ExamesPage());
        }

        private void BtnNovoExame_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new NovoExamePage());
        }
    }
}
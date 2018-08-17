using LabClick.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LabClick.Views.Paciente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewPatient : ContentPage
	{
		public NewPatient ()
		{
			InitializeComponent ();
            this.BindingContext = new NewPatientViewModel();
		}

        private void BtnSalvar_Clicked(object sender, System.EventArgs e)
        {

        }
    }
}
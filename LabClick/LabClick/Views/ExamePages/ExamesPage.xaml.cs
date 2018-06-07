using LabClick.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LabClick.Views.ExamePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ExamesPage : ContentPage
	{
        private Paciente paciente;

		public ExamesPage (Paciente paciente)
		{
			InitializeComponent ();
            this.paciente = paciente;
		}
	}
}
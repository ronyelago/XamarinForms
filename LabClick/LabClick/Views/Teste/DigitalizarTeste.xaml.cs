using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LabClick.Views.Teste
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DigitalizarTeste : ContentPage
	{
		public DigitalizarTeste (Domain.Entities.Paciente paciente)
		{
			InitializeComponent ();
		}
	}
}
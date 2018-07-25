using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LabClick.Views.Teste
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LaudoTeste : ContentPage
	{
		public LaudoTeste (Domain.Entities.Teste teste)
		{
			InitializeComponent ();


		}
	}
}
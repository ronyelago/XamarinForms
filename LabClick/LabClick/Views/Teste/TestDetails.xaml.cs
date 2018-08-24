using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LabClick.Views.Teste
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TestDetails : ContentPage
	{
		public TestDetails(Domain.Entities.Teste teste)
		{
			InitializeComponent();
		}
	}
}
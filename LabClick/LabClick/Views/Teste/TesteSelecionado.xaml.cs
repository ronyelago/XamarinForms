using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LabClick.Views.Teste
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TesteSelecionado : ContentPage
	{
        public TesteSelecionado(Domain.Entities.Teste teste)
		{
			InitializeComponent ();

            lblExame.Text = $"Teste: {teste.Exame.Nome}";
            lblStatus.Text = $"Status: {teste.Status}";
            lblDataRealizado.Text = $"Data do Teste: {teste.DataCadastro.ToShortDateString()}";
            ImageSource imageSource = ImageSource.FromStream(() => new MemoryStream(teste.Imagem.ToArray()));
            ExameImg.Source = imageSource;

            if (teste.LaudoOk)
            {
                btnVerPdf.IsEnabled = true;
            }
		}

        private async Task BtnVerPdf_ClickedAsync(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync
        }
    }
}
using System.IO;
using System.Linq;
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

        private void BtnVerPdf_Clicked(object sender, System.EventArgs e)
        {
            
        }
    }
}
using System.IO;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LabClick.Views.Teste
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LaudoTeste : ContentPage
	{
        public LaudoTeste(Domain.Entities.Teste teste)
		{
			InitializeComponent ();

            lblExame.Text = $"Teste: {teste.Exame.Nome}";
            lblStatus.Text = $"Status: {teste.Status}";
            lblDataRealizado.Text = $"Data do Teste: {teste.DataCadastro.ToShortDateString()}";

            if (teste.LaudoOk)
            {
                lblResultado.Text = $"Resultado: {teste.Laudo.Resultado}";
            }

            ImageSource imageSource = ImageSource.FromStream(() => new MemoryStream(teste.Imagem.ToArray()));
            ExameImg.Source = imageSource;
		}
    }
}
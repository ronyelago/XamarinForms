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
                lblResultado.Text = $"Resultado: {teste.Resultado}";
            }

            //ImageSource imageSource = ImageSource.FromStream(() => new MemoryStream(teste.TesteImagem.Imagem.ToArray()));
            //ExameImg.Source = imageSource;
		}
    }
}
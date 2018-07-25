using Newtonsoft.Json;
using System.IO;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LabClick.Views.Teste
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LaudoTeste : ContentPage
	{
        public LaudoTeste(int pacienteId)
		{
			InitializeComponent ();

            var result = App.Client.GetAsync($@"http://apilabclick.mflogic.com.br/teste/getByPatientId={pacienteId}");
            var content = result.Result.Content.ReadAsStringAsync();
            var teste = JsonConvert.DeserializeObject<Domain.Entities.Teste>(content.Result);

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
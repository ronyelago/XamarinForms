using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Mobile;

namespace LabClick.Views.Teste
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NovoTeste : ContentPage
	{
        private byte[] pictureBits;
        private Domain.Entities.Paciente paciente;
        private Domain.Entities.Teste teste;

		public NovoTeste(Domain.Entities.Paciente paciente)
		{
			InitializeComponent ();
            BtnEnviarTeste.IsEnabled = false;
            this.paciente = paciente;
            this.teste = new Domain.Entities.Teste();
		}

        private async Task BtnTeste_ClickedAsync(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("Nenhuma Câmera", ":( Nenuma Câmera disponível.", "OK");
                return;
            }

            var armazenamento = new StoreCameraMediaOptions()
            {
                SaveToAlbum = true,
                Name = "MinhaFoto.jpg"
            };

            var foto = await CrossMedia.Current.TakePhotoAsync(armazenamento);

            if (foto == null)
            {
                return;
            }

            Stream stm = foto.GetStream();
            MemoryStream ms = new MemoryStream();

            stm.CopyTo(ms);
            pictureBits = ms.ToArray();

            imgFoto.Source = ImageSource.FromStream(() =>
            {
                var stream = foto.GetStream();
                foto.Dispose();
                return stream;
            });

            BtnEnviarTeste.IsEnabled = true;
        }

        public async Task BtnEnviarTeste_ClickedAsync(object sender, EventArgs e)
        {
            teste.ExameId = 1;
            teste.ClinicaId = 1;
            teste.PacienteId = paciente.Id;
            teste.Imagem = pictureBits;
            teste.Status = "Em análise";
            teste.DataCadastro = DateTime.Now;
            
            var serialized = JsonConvert.SerializeObject(teste);

            HttpClient client = new HttpClient();
            Uri uri = new Uri(@"http://apilabclick.mflogic.com.br/teste/testes");
            var content = new StringContent(serialized, Encoding.UTF8, "application/json");

            var result = await client.PostAsync(uri, content);

            if (result.IsSuccessStatusCode)
            {
                await DisplayAlert("Sucesso", "Teste enviado para análise.", "Ok");
            }

            else
            {
                await DisplayAlert("Falha", "Algo errado não deu certo.", "Ok");
            }
        }

        private async Task BtnQrCode_ClickedAsync(object sender, EventArgs e)
        {
            var scanner = new MobileBarcodeScanner();
            var result = await scanner.Scan();

            if (result != null)
            {
                teste.Code = result.Text;
            }
        }
    }
}
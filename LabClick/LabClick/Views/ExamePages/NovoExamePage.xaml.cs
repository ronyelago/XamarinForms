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

namespace LabClick.Views.ExamePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NovoExamePage : ContentPage
	{
        private byte[] pictureBits;

		public NovoExamePage ()
		{
			InitializeComponent ();
            BtnEnviarTeste.IsEnabled = false;
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
                return;

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
            var Teste = new
            {
                ExameId = 1,
                ClinicaId = 3,
                PacienteId = 2,
                Imagem = pictureBits,
                Status = "Em análise",
                DataCadastro = DateTime.Now
            };

            var serialized = JsonConvert.SerializeObject(Teste);

            HttpClient client = new HttpClient();
            Uri uri = new Uri(@"http://192.168.0.15:3000/teste/testes");
            var content = new StringContent(serialized, Encoding.UTF8, "application/json");

            var result = await client.PostAsync(uri, content);

            if (result.IsSuccessStatusCode)
            {
                await DisplayAlert("Sucesso", "Teste enviado para análise.", "Ok");
            }
        }
    }
}
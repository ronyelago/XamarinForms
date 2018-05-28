using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace LabClick
{
    public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
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
            byte[] bits = ms.ToArray();

            var Teste = new
            {
                ExameId = 1,
                ClinicaId = 3,
                PacienteId = 2,
                Imagem = bits,
                Status = "Fumegou.",
                DataCadastro = DateTime.Now
            };

            var serialized = JsonConvert.SerializeObject(Teste);

            HttpClient client = new HttpClient();
            Uri uri = new Uri(@"http://localhost:3000/testes");
            var content = new StringContent(serialized, Encoding.UTF8, "application/json");

            var result = client.PostAsync(uri, content);

            imgFoto.Source = ImageSource.FromStream(() =>
            {
                var stream = foto.GetStream();
                foto.Dispose();
                return stream;
            });
        }

        private async void btnSelecionarImagem_Clicked(object sender, EventArgs e)
        {
            var byteArray = Convert.FromBase64String(imgFoto.Source.ToString());

            HttpClient client = new HttpClient();
            string url = @"http://192.168.0.15:3000/api/Teste/4";
            var x = await client.GetAsync(url);

        }
    }
}

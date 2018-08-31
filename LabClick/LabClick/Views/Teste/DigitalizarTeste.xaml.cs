using AutoMapper;
using LabClick.ViewModels;
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
	public partial class DigitalizarTeste : ContentPage
	{
        public DigitalizarTesteViewModel DigitalizarTesteViewModel { get; set; }

        public DigitalizarTeste (Domain.Entities.Paciente paciente)
		{
            InitializeComponent();

            this.lblNomePaciente.Text = $"Paciente: {paciente.Nome}";

            // Inicialização dos campos do view model
            this.DigitalizarTesteViewModel = new DigitalizarTesteViewModel
            {
                IsBusy = false,
                PacienteId = paciente.Id,
                DataCadastro = DateTime.Now,
                ClinicaId = 1,
                Status = "Em Análise",
                ExameId = 1,
                ImageShow = "bgfoto.jpeg"
            };

            this.BindingContext = DigitalizarTesteViewModel;
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
            DigitalizarTesteViewModel.Imagem = ms.ToArray();

            imgFoto.Source = ImageSource.FromStream(() =>
            {
                var stream = foto.GetStream();
                foto.Dispose();
                return stream;
            });

            DigitalizarTesteViewModel.IsBusy = true;
            BtnEnviarTeste.IsEnabled = true;
            btnTeste.Text = "Fotografar Novamente";
        }

        public async Task BtnEnviarTeste_ClickedAsync(object sender, EventArgs e)
        {
            if (DigitalizarTesteViewModel.IsValid())
            {
                DigitalizarTesteViewModel.IsBusy = true;

                var teste = Mapper.Map<Domain.Entities.Teste>(DigitalizarTesteViewModel);
                var serialized = JsonConvert.SerializeObject(teste);

                HttpClient client = new HttpClient();
                Uri uri = new Uri(@"http://apilabclick.mflogic.com.br/teste/testes");
                var content = new StringContent(serialized, Encoding.UTF8, "application/json");

                var result = await client.PostAsync(uri, content);

                if (result.IsSuccessStatusCode)
                {
                    await DisplayAlert("Sucesso", "Teste enviado para análise com sucesso.", "Ok");
                    DigitalizarTesteViewModel.IsBusy = false;

                    await Navigation.PushAsync(new Home());
                }

                else
                {
                    await DisplayAlert("Erro", "Não foi possível enviar o teste.", "Ok");
                    DigitalizarTesteViewModel.IsBusy = false;
                }
            }

            else
            {
                await DisplayAlert("Erro", "Antes de enviar o Exame, o Teste Rápido deve ser fotografado" +
                    "e o QR-Code escaneado.", "Ok");
            }
        }

        //private async Task BtnQrCode_ClickedAsync(object sender, EventArgs e)
        //{
        //    var scanner = new MobileBarcodeScanner();
        //    var result = await scanner.Scan();

        //    if (result != null)
        //    {
        //        DigitalizarTesteViewModel.Code = result.Text;
        //    }
        //}

        private async Task TapGestureRecognizer_TappedAsync(object sender, EventArgs e)
        {
            var scanner = new MobileBarcodeScanner();
            var result = await scanner.Scan();

            if (result != null)
            {
                DigitalizarTesteViewModel.Code = result.Text;
            }
        }
    }
}
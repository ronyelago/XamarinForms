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

        public DigitalizarTeste(Domain.Entities.Paciente paciente)
        {
            InitializeComponent();

            this.lblNomePaciente.Text = $"Paciente: {paciente.Nome}";

            // Inicialização dos campos do view model
            this.DigitalizarTesteViewModel = new DigitalizarTesteViewModel
            {
                PacienteId = paciente.Id,
                DataCadastro = DateTime.Now,
                ClinicaId = 1,
                Status = "Em Análise",
                ExameId = 1,
                ImageShow = "bgfoto.jpeg",
                Code = " _ _ _ _ _ _ _ "
            };

            this.BindingContext = DigitalizarTesteViewModel;
        }

        // Evento que abre a câmera para fotografar o Teste
        private async Task BtnTeste_ClickedAsync(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("Nenhuma Câmera", "Nenuma Câmera disponível.", "Fechar");
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

            BtnEnviarTeste.IsEnabled = true;
            btnTeste.Text = "Fotografar novamente";
        }

        // Evento que abre a câmera para escanear o QR-Code do Teste
        private async Task BtnEscanear_ClickedAsync(object sender, EventArgs e)
        {
            var scanner = new MobileBarcodeScanner();
            var result = await scanner.Scan();

            if (result != null)
            {
                DigitalizarTesteViewModel.Code = result.Text;
                DigitalizarTesteViewModel.Scanned = true;
            }
        }

        // Evento que envia o teste para o banco de dados
        public async Task BtnEnviarTeste_ClickedAsync(object sender, EventArgs e)
        {
            if (DigitalizarTesteViewModel.IsValid())
            {
                await Navigation.PushAsync(App.LoadingPage);

                var teste = Mapper.Map<Domain.Entities.Teste>(DigitalizarTesteViewModel);
                var serialized = JsonConvert.SerializeObject(teste);

                HttpClient client = new HttpClient();
                Uri uri = new Uri(@"http://apilabclick.mflogic.com.br/teste/testes");
                var content = new StringContent(serialized, Encoding.UTF8, "application/json");

                var result = await client.PostAsync(uri, content);

                if (result.IsSuccessStatusCode)
                {
                    await DisplayAlert("Sucesso", "Teste enviado para análise com sucesso.", "Ok");
                    await Navigation.PushAsync(new Home());
                }

                else
                {
                    await DisplayAlert("Erro", "Não foi possível enviar o teste.", "Ok");
                }

                Navigation.RemovePage(App.LoadingPage);
            }

            else
            {
                await DisplayAlert("Erro", "Antes de enviar o Teste, você deve fotografar o " +
                    "teste rápido e escanear o QR-Code do mesmo.", "Ok");
            }
        }

        private void BtnCancelar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Home());
        }
    }
}
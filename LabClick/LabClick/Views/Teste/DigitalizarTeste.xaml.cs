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
        public TesteImagemViewModel TesteImagemViewModel { get; set;}

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

            if (Device.Idiom == TargetIdiom.Phone)
            {
                pacienteStackLayout.Margin = new Thickness(10, 5, 5, 10);

                DigitalizarTesteBar.HeightRequest = 30;

                lblNomePaciente.FontSize = 15;

                ImagemStackLayout.Margin = new Thickness(50, 5, 50, 5);
                imgFoto.WidthRequest = 400;
                imgFoto.HeightRequest = 200;

                btnTeste.WidthRequest = 150;
                btnTeste.HeightRequest = 30;
                btnTeste.FontSize = 8;
                btnTeste.CornerRadius = 10;

                btnEscanearStack.Margin = new Thickness(10, 5, 10, 10);
                BtnEscanear.WidthRequest = 150;
                BtnEscanear.HeightRequest = 30;
                BtnEscanear.FontSize = 8;
                BtnEscanear.CornerRadius = 10;

                codeStackLayout.Margin = new Thickness(50, 5, 50, 5);

                codeLabelStackLayout.WidthRequest = 250;
                codeLabelStackLayout.HeightRequest = 30;

                LblQrCode.FontSize = 20;

                buttonsStackLayout.Margin = new Thickness(10, 0, 10, 5);

                BtnCancelar.WidthRequest = 150;
                BtnCancelar.HeightRequest = 30;
                BtnCancelar.FontSize = 8;
                BtnCancelar.CornerRadius = 10;

                BtnEnviarTeste.WidthRequest = 150;
                BtnEnviarTeste.HeightRequest = 30;
                BtnEnviarTeste.FontSize = 8;
                BtnEnviarTeste.CornerRadius = 10;
            }
        }

        // Evento que abre a câmera para fotografar o Teste
        private async void BtnTeste_ClickedAsync(object sender, EventArgs e)
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
                Name = "photTest.jpg"
            };

            var foto = await CrossMedia.Current.TakePhotoAsync(armazenamento);

            if (foto == null)
            {
                return;
            }

            DigitalizarTesteViewModel.Fotografado = true;

            Stream stm = foto.GetStream();
            MemoryStream ms = new MemoryStream();

            stm.CopyTo(ms);
            TesteImagemViewModel = new TesteImagemViewModel { Imagem = ms.ToArray() };

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
        private async void BtnEscanear_ClickedAsync(object sender, EventArgs e)
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
        public async void BtnEnviarTeste_ClickedAsync(object sender, EventArgs e)
        {
            BtnEnviarTeste.IsEnabled = false;

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
                    var stringTest = result.Content.ReadAsStringAsync().Result;
                    var test = JsonConvert.DeserializeObject<Domain.Entities.Teste>(stringTest);

                    var img = Mapper.Map<Domain.Entities.TesteImagem>(TesteImagemViewModel);
                    img.TesteId = test.Id;
                    var imgJson = JsonConvert.SerializeObject(img);
                    var imgContent = new StringContent(imgJson, Encoding.UTF8, "application/json");
                    uri = new Uri(@"http://apilabclick.mflogic.com.br/imagem/new");

                    var postResult = await client.PostAsync(uri, imgContent);

                    if (postResult.IsSuccessStatusCode)
                    {
                        await DisplayAlert("Sucesso", "Teste enviado para análise com sucesso.", "Ok");
                        await Navigation.PushAsync(new Home());
                    }
                }

                else
                {
                    await DisplayAlert("Erro", "Não foi possível enviar o teste.", "Ok");
                    BtnEnviarTeste.IsEnabled = true;
                }

                Navigation.RemovePage(App.LoadingPage);
            }

            else
            {
                await DisplayAlert("Erro", "Antes de enviar o Teste, você deve fotografar o " +
                                   "teste rápido e escanear o QR-Code do mesmo.", "Ok");

                BtnEnviarTeste.IsEnabled = true;
            }
        }

        private void BtnCancelar_Clicked(object sender, EventArgs e)
        {
            Navigation.RemovePage(this);
        }
    }
}
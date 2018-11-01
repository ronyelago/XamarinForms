using AutoMapper;
using LabClick.ViewModels;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
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
                ClinicaId = App.User.ClinicaId,
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
                btnTeste.CornerRadius = 3;

                btnEscanearStack.Margin = new Thickness(10, 5, 10, 10);
                BtnEscanear.WidthRequest = 150;
                BtnEscanear.HeightRequest = 30;
                BtnEscanear.FontSize = 8;
                BtnEscanear.CornerRadius = 3;

                codeStackLayout.Margin = new Thickness(50, 5, 50, 5);

                codeLabelStackLayout.WidthRequest = 250;
                codeLabelStackLayout.HeightRequest = 30;

                LblQrCode.FontSize = 20;

                buttonsStackLayout.Margin = new Thickness(10, 0, 10, 5);

                BtnCancelar.WidthRequest = 150;
                BtnCancelar.HeightRequest = 30;
                BtnCancelar.FontSize = 8;
                BtnCancelar.CornerRadius = 3;

                BtnEnviarTeste.WidthRequest = 150;
                BtnEnviarTeste.HeightRequest = 30;
                BtnEnviarTeste.FontSize = 8;
                BtnEnviarTeste.CornerRadius = 3;
            }
        }

        // Evento que abre a câmera para fotografar o Teste
        private async void BtnTeste_ClickedAsync(object sender, EventArgs e)
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);

                if (status != PermissionStatus.Granted)
                {
                    if (!await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Camera))
                    {
                        await DisplayAlert("Permissão", "Permita o acesso à câmera.", "OK");
                        CrossPermissions.Current.OpenAppSettings();
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera });
                    status = results[Permission.Camera];
                }

                if (status == PermissionStatus.Granted)
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
                        Name = "photoTest.jpg",
                        PhotoSize = PhotoSize.Small,
                        CompressionQuality = 50
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

                else if (status != PermissionStatus.Unknown)
                {
                    await DisplayAlert("Permissões", "Não foi possível continuar, tente novamente.", "OK");
                }
            }

            catch (Exception ex)
            {
                await DisplayAlert("Erro", "Câmera indisponível.", "OK");
                await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Storage);


            }
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

                try
                {
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

                        try
                        {
                            var postResult = await client.PostAsync(uri, imgContent);

                            if (postResult.IsSuccessStatusCode)
                            {
                                await DisplayAlert("Sucesso", "Teste enviado para análise com sucesso.", "Fechar");
                                await Navigation.PushAsync(new Home());
                            }
                        }
                        catch (Exception ex)
                        {
                            await DisplayAlert("Erro", $"Não foi possível estabelecer conexção com o servidor. {ex.Message}", "Fechar");
                            BtnEnviarTeste.IsEnabled = true;
                        }
                    }

                    else
                    {
                        await DisplayAlert("Erro", "Não foi possível enviar o teste.", "Fechar");
                        BtnEnviarTeste.IsEnabled = true;
                    }

                    Navigation.RemovePage(App.LoadingPage);
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Erro", $"Não foi possível estabelecer conexção com o servidor. {ex.Message}", "Fechar");
                    BtnEnviarTeste.IsEnabled = true;
                    Navigation.RemovePage(App.LoadingPage);
                }
            }

            else
            {
                await DisplayAlert("Erro", "Antes de enviar o Teste, você deve fotografar o " +
                                   "teste rápido e escanear o QR-Code do mesmo.", "Fechar");

                BtnEnviarTeste.IsEnabled = true;
            }
        }

        private void BtnCancelar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Home());
        }
    }
}
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
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
            imgFoto.Source = ImageSource.FromStream(() =>
            {
                var stream = foto.GetStream();
                foto.Dispose();
                return stream;
            });
        }

        private async void btnSelecionarImagem_Clicked(object sender, EventArgs e)
        {
            if (CrossMedia.Current.IsTakePhotoSupported)
            {
                var imagem = await CrossMedia.Current.PickPhotoAsync();
                if (imagem != null)
                {
                    imgFoto.Source = ImageSource.FromStream(() =>
                    {
                        var stream = imagem.GetStream();
                        imagem.Dispose();
                        return stream;
                    });
                }
            }
        }
    }
}

using System.IO;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LabClick.Views.Teste
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TesteSelecionado : ContentPage
	{
        public TesteSelecionado(Domain.Entities.Teste teste)
		{
			InitializeComponent ();

            lblExame.Text = $"Teste: {teste.Exame.Nome}";
            lblStatus.Text = $"Status: {teste.Status}";
            lblDataRealizado.Text = $"Data do Teste: {teste.DataCadastro.ToShortDateString()}";
            ImageSource imageSource = ImageSource.FromStream(() => new MemoryStream(teste.Imagem.ToArray()));
            ExameImg.Source = imageSource;

            if (teste.LaudoOk)
            {
                btnVerPdf.IsEnabled = true;
            }
		}

        private void BtnVerPdf_Clicked(object sender, System.EventArgs e)
        {
            var browser = new WebView
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };


            var htmlSource = new HtmlWebViewSource();
            htmlSource.Html = @"<html><body>
                              <h1>Xamarin.Forms</h1>
                              <p>Welcome to WebView.</p>
                              </body></html>";
            browser.Source = htmlSource;

            ToolbarItems.Add(new ToolbarItem("Back", null, () =>
            {
                browser.GoBack();
            }));

            StackButton.Children.Add(browser);

            browser.IsVisible = true;
            browser.WidthRequest = 1000;
            browser.HeightRequest = 1000;
        }
    }
}
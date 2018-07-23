using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LabClick.Views.Teste
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PdfView : ContentPage
	{
		public PdfView ()
		{
			InitializeComponent ();

            string html = @"<!DOCTYPE html>
                            <html>
                                <head>
                                    
                                </head>
                                <body>
                                    <h1>Oi, eu sou o Goku!</h1>
                                    <button text=""Teste"" onclick=""Alerta()"">Touch me!</button>
                                </body>
                            </html>

                            <script>
                                function Alerta(){
                                    alert('oi');
                                }
                            </script>";
                            

            var htmlSource = new HtmlWebViewSource { Html = html };

            PdfViewer.Source = htmlSource;
            
            
		}
	}
}
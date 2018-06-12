using LabClick.Models;
using System.IO;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LabClick.Views.ExamePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ExameSelecionadoPage : ContentPage
	{
        public ExameSelecionadoPage(Teste teste)
		{
			InitializeComponent ();

            lblExame.Text = $"Teste: {teste.Exame.Nome}";
            lblStatus.Text = $"Status: {teste.Status}";
            lblDataRealizado.Text = $"Data do Teste: {teste.DataCadastro.ToShortDateString()}";
            ImageSource imageSource = ImageSource.FromStream(() => new MemoryStream(teste.Imagem.ToArray()));
            ExameImg.Source = imageSource;
		}
	}
}
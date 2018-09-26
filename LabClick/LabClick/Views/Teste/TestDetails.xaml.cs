using AutoMapper;
using LabClick.ViewModels;
using Newtonsoft.Json;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LabClick.Views.Teste
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TestDetails : ContentPage
	{
        public TestDetailsViewModel ViewModel { get; set; }

        public TestDetails(Domain.Entities.Teste teste)
		{
			InitializeComponent();

            ViewModel = new TestDetailsViewModel();
            ViewModel = Mapper.Map<TestDetailsViewModel>(teste);
            this.BindingContext = ViewModel;

            Uri urinho = new Uri($@"http://apilabclick.mflogic.com.br/imagem/getByTesteId={teste.Id}");
            var result = App.Client.GetAsync(urinho);
            var content = result.Result.Content.ReadAsStringAsync();

            var testeImagem = JsonConvert.DeserializeObject<TesteImagemViewModel>(content.Result);

            Stream stm = new MemoryStream(testeImagem.Imagem);

            ImgTeste.Source = ImageSource.FromStream(() =>
            {
                return stm;
            });

            if (Device.Idiom == TargetIdiom.Phone)
            {
                labelsStack.Margin = new Thickness(25, 5, 5, 10);

                lblExame.FontSize = 13;
                lblNomePaciente.FontSize = 13;
                lblDataTeste.FontSize = 13;
                lblStatus.FontSize = 13;

                lblExameDados.FontSize = 13;
                lblNomePacienteDados.FontSize = 13;
                lblDataCadastroDados.FontSize = 13;
                lblStatusDados.FontSize = 13;

                lblImagemTeste.FontSize = 15;
                ImgTeste.WidthRequest = 150;
                ImgTeste.HeightRequest = 300;

                lblTituloResultado.FontSize = 15;

                BtnFechar.WidthRequest = 150;
                BtnFechar.HeightRequest = 30;
                BtnFechar.FontSize = 10;
                BtnFechar.CornerRadius = 7;
            }
        }

        private void BtnFechar_Clicked(object sender, System.EventArgs e)
        {
            Navigation.RemovePage(this);
        }

        private void BtnVerPdf_Clicked(object sender, EventArgs e)
        {
            
        }

        
    }
}
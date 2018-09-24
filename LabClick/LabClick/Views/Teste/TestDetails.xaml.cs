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

                lblExame.FontSize = 15;
                lblNomePaciente.FontSize = 15;
                lblDataTeste.FontSize = 15;
                lblStatus.FontSize = 15;

                lblExameDados.FontSize = 15;
                lblNomePacienteDados.FontSize = 15;
                lblDataCadastroDados.FontSize = 15;
                lblStatusDados.FontSize = 15;

                ImgTeste.WidthRequest = 150;
                ImgTeste.HeightRequest = 300;
            }
		}

        private void BtnFechar_Clicked(object sender, System.EventArgs e)
        {
            Navigation.RemovePage(this);
        }
    }
}
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
            Navigation.PushAsync(App.LoadingPage);

            try
            {
                Uri urinho = new Uri($@"http://apilabclick.mflogic.com.br/imagem/getByTesteId={teste.Id}");
                var result = App.Client.GetAsync(urinho);

                if (result.Result.IsSuccessStatusCode)
                {
                    var content = result.Result.Content.ReadAsStringAsync();

                    var testeImagem = JsonConvert.DeserializeObject<TesteImagemViewModel>(content.Result);

                    Stream stm = new MemoryStream(testeImagem.Imagem);

                    ImgTeste.Source = ImageSource.FromStream(() =>
                    {
                        return stm;
                    });
                }
                else
                {
                    Navigation.RemovePage(App.LoadingPage);
                    Navigation.RemovePage(this);
                    DisplayAlert("Erro", "Não foi possível recuperar as informações do exame.", "Fechar");
                }
            }
            catch (Exception ex)
            {
                Navigation.RemovePage(App.LoadingPage);
                Navigation.RemovePage(this);
                DisplayAlert("Erro", $"Não foi possível recuperar as informações do exame. {ex.Message}", "Fechar");
            }

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
                ImgTeste.WidthRequest = 130;
                ImgTeste.HeightRequest = 280;

                lblTituloResultado.FontSize = 15;
            }

            Navigation.RemovePage(App.LoadingPage);
        }
    }
}
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

            Uri urinho = new Uri($@"http://192.168.0.15:3000/imagem/getByTesteId={teste.Id}");
            var result = App.Client.GetAsync(urinho);
            var content = result.Result.Content.ReadAsStringAsync();

            var testeImagem = JsonConvert.DeserializeObject<TesteImagemViewModel>(content.Result);

            Stream stm = new MemoryStream(testeImagem.Imagem);

            ImgTeste.Source = ImageSource.FromStream(() => 
            {
                return stm;
            });
		}

        private void BtnFechar_Clicked(object sender, System.EventArgs e)
        {
            Navigation.RemovePage(this);
        }
    }
}
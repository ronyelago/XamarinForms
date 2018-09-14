using AutoMapper;
using LabClick.ViewModels;
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

            Stream stm = new MemoryStream(ViewModel.Imagem);

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
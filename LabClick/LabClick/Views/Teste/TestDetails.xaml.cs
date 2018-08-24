using AutoMapper;
using LabClick.ViewModels;
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
		}
	}
}
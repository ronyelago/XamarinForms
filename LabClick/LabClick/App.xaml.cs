using LabClick.Mappers;
using LabClick.Services;
using LabClick.Views;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace LabClick
{
    public partial class App : Application
	{
        //Propriedade usada para controlar a��es da master detail
        public static MasterDetailPage MasterPage { get; set; }
        //HttpClient usado para as requisi��es � api
        public static HttpClient Client { get; set; }
        public static LoadingPage LoadingPage { get; set; }

        public App ()
		{
            InitializeComponent();

            Client = new HttpClient();

            LoadingPage = new LoadingPage();

            MainPage = new NavigationPage(new Login())
            {
                BarBackgroundColor = Color.LightSeaGreen
            };
		}

        //Invocar esta task ao clicar em um �tem da master detail
        //passando a p�gica a ser aberta por par�metro
        public async static Task NavigateMasterDetail(Page page)
        {
            //oculta a master
            App.MasterPage.IsPresented = false;
            
            //exibe outra p�gina
            await App.MasterPage.Navigation.PushAsync(page);
        }

		protected override void OnStart ()
		{
            // Handle when your app starts
            AutoMapperConfig.RegisterMappings();
        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumess
		}
	}
}

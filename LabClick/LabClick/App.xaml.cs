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
        //Propriedade usada para controlar ações da master detail
        public static MasterDetailPage MasterPage { get; set; }
        //HttpClient usado para as requisições à api
        public static HttpClient Client { get; set; }

        public App ()
		{
            InitializeComponent();

            MainPage = new NavigationPage(new Login())
            {
                BarBackgroundColor = Color.LightSeaGreen
            };

            Client = new HttpClient();
		}

        //Invocar esta task ao clicar em um ítem da master detail
        //passando a págica a ser aberta por parâmetro
        public async static Task NavigateMasterDetail(Page page)
        {
            //oculta a master
            App.MasterPage.IsPresented = false;
            
            //exibe outra página
            await App.MasterPage.Navigation.PushAsync(page);
        }

		protected override void OnStart ()
		{
            // Handle when your app starts
        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

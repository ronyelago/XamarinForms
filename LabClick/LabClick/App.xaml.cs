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
        public static MasterDetailPage PacientMasterDetailPage { get; set; }
        public static HttpClient Client { get; set; }

        public App ()
		{
			InitializeComponent();

            MainPage = new NavigationPage(new Login());
            Client = new HttpClient();
		}

        public async static Task NavigateMasterDetail(Page page)
        {
            App.PacientMasterDetailPage.IsPresented = false;
            await App.PacientMasterDetailPage.Navigation.PushAsync(page);
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

using LabClick.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace LabClick
{
    public partial class App : Application
	{
        public static MasterDetailPage PacientMasterDetailPage { get; set; }

        public App ()
		{
			InitializeComponent();

            MainPage = new NavigationPage(new Login());
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

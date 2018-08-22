using Xamarin.Forms;

namespace LabClick.Services
{
    interface ILoadingPageService
    {
        void InitLoadingPage
              (ContentPage loadingIndicatorPage = null);

        void ShowLoadingPage();

        void HideLoadingPage();
    }
}

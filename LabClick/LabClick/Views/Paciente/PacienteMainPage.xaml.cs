using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LabClick.Views.Paciente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PacienteMainPage : MasterDetailPage
	{
        

        public PacienteMainPage (Domain.Entities.Paciente paciente)
		{
			InitializeComponent ();

            this.Master = new PacienteMasterPage();
            this.Detail = new NavigationPage(new PacienteDetails(paciente));

            App.MasterPage = this;

            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(PacienteDetails), paciente));
        }
	}
}
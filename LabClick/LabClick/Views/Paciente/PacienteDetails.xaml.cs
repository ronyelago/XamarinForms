using AutoMapper;
using LabClick.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LabClick.Views.Paciente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PacienteDetails : ContentPage
	{
		public PacienteDetails (Domain.Entities.Paciente paciente)
		{
			InitializeComponent ();

            var newPatientViewModel = Mapper.Map<NewPatientViewModel>(paciente);
            this.BindingContext = newPatientViewModel;
        }

        private void TxtCep_Unfocused(object sender, FocusEventArgs e)
        {

        }

        private void BtnCancelar_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new Home());
        }

        private void BtnSalvar_Clicked(object sender, System.EventArgs e)
        {

        }
    }
}
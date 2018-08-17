using AutoMapper;
using LabClick.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LabClick.Views.Paciente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewPatient : ContentPage
	{
        public NewPatientViewModel NewPatientViewModel { get; set; }
        public NewPatient ()
		{
			InitializeComponent ();
            this.BindingContext = new NewPatientViewModel();
		}

        private void BtnSalvar_Clicked(object sender, System.EventArgs e)
        {
            var paciente = Mapper.Map<Domain.Entities.Paciente>(this.BindingContext as NewPatientViewModel);
            var endereco = Mapper.Map<Domain.Entities.Endereco>(this.BindingContext as NewPatientViewModel);
        }

        private void txtCep_Unfocused(object sender, FocusEventArgs e)
        {

        }

        private void BtnCancelar_Clicked(object sender, System.EventArgs e)
        {

        }
    }
}
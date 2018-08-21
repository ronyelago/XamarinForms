using AutoMapper;
using LabClick.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LabClick.Views.Paciente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PacienteDetails : ContentPage
	{
        public NewPatientViewModel NewPatientViewModel { get; set; }

        public PacienteDetails (Domain.Entities.Paciente paciente)
		{
			InitializeComponent ();

            txtNome.IsEnabled = false;
            dataNascimento.IsEnabled = false;
            txtCpf.IsEnabled = false;
            pickerGenero.IsEnabled = false;
            txtEmail.IsEnabled = false;
            txtPhone.IsEnabled = false;
            TxtCep.IsEnabled = false;
            txtUf.IsEnabled = false;
            txtCidade.IsEnabled = false;
            txtBairro.IsEnabled = false;
            txtRua.IsEnabled = false;
            txtNumero.IsEnabled = false;

            this.NewPatientViewModel = new NewPatientViewModel();
            this.NewPatientViewModel = Mapper.Map<NewPatientViewModel>(paciente);
            this.BindingContext = NewPatientViewModel;
        }

        private void TxtCep_Unfocused(object sender, FocusEventArgs e)
        {
            var address = NewPatientViewModel.GetAddress(TxtCep.Text);

            if (address != null)
            {
                this.NewPatientViewModel.Cidade = address.Cidade;
                this.NewPatientViewModel.UF = address.UF;
                this.NewPatientViewModel.Bairro = address.Bairro;
                this.NewPatientViewModel.Logradouro = address.Logradouro;
            }

            else
            {
                DisplayAlert("Erro", "Endereço não localizado. Tente novamente ou preencha manualmente.", "Fechar");
            }
        }

        private void BtnCancelar_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new Home());
        }

        private void BtnSalvar_Clicked(object sender, System.EventArgs e)
        {
            if (this.NewPatientViewModel.IsValid())
            {
                var result = NewPatientViewModel.Update(NewPatientViewModel);

                if (result)
                {
                    DisplayAlert("Sucesso", "Paciente atualizado com sucesso.", "Fechar");
                    Navigation.PushAsync(new Home());
                }

                else
                {
                    DisplayAlert("Erro", "Erro ao tentar atualizar o paciente.", "Fechar");
                }
            }

            else
            {
                DisplayAlert("Erro", "Favor preencher todos os campos", "Fechar");
            }
        }

        private void BtnEditarNome_Clicked(object sender, System.EventArgs e)
        {
            BtnSalvar.IsEnabled = true;
            txtNome.IsEnabled = true;
        }
    }
}
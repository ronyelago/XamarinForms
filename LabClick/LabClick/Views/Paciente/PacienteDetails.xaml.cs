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
            dataNascimentoPicker.IsEnabled = false;
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

            if (Device.Idiom == TargetIdiom.Phone)
            {
                mainImage.HeightRequest = 40;
                MainLabel.FontSize = 20;

                txtNome.WidthRequest = 200;
                txtNome.FontSize = 15;

                lblDataNascimento.FontSize = 15;
                dataNascimentoPicker.FontSize = 15;

                txtCpf.WidthRequest = 200;
                txtCpf.FontSize = 15;

                pickerGenero.WidthRequest = 200;
                pickerGenero.FontSize = 15;

                txtEmail.WidthRequest = 200;
                txtEmail.FontSize = 15;

                txtPhone.WidthRequest = 200;
                txtPhone.FontSize = 15;

                enderecoStack.Margin = new Thickness(50, 10, 20, 10);

                lblCep.FontSize = 13;
                TxtCep.WidthRequest = 150;
                TxtCep.FontSize = 13;

                lblUf.FontSize = 13;
                lblUf.Margin = new Thickness(0, 0, 10, 0);
                txtUf.WidthRequest = 150;

                lblCidade.FontSize = 13;
                txtCidade.WidthRequest = 140;
                txtCidade.FontSize = 13;

                lblBairro.FontSize = 13;
                txtBairro.WidthRequest = 150;
                txtBairro.FontSize = 13;

                txtRua.WidthRequest = 150;
                txtRua.FontSize = 12;
                txtRua.AnchorX = -3;
                txtRua.AnchorY = 0;

                lblNumero.FontSize = 13;
                txtNumero.WidthRequest = 100;
                txtNumero.FontSize = 13;

                BtnSalvar.WidthRequest = 150;
                BtnSalvar.HeightRequest = 30;
                BtnSalvar.FontSize = 8;
                BtnSalvar.CornerRadius = 10;

                BtnCancelar.WidthRequest = 150;
                BtnCancelar.HeightRequest = 30;
                BtnCancelar.FontSize = 8;
                BtnCancelar.CornerRadius = 10;
            }
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
                Navigation.PushAsync(App.LoadingPage);

                var result = NewPatientViewModel.Update(NewPatientViewModel);

                if (result)
                {
                    DisplayAlert("Sucesso", "Paciente atualizado com sucesso.", "Fechar");
                    Navigation.RemovePage(App.LoadingPage);
                }

                else
                {
                    DisplayAlert("Erro", "Erro ao tentar atualizar o paciente.", "Fechar");
                    Navigation.RemovePage(App.LoadingPage);
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

        private void BtnEditarData_Clicked(object sender, System.EventArgs e)
        {
            BtnSalvar.IsEnabled = true;
            dataNascimentoPicker.IsEnabled = true;
        }

        private void BtnEditarCpf_Clicked(object sender, System.EventArgs e)
        {
            BtnSalvar.IsEnabled = true;
            txtCpf.IsEnabled = true;
        }

        private void BtnEditarGenero_Clicked(object sender, System.EventArgs e)
        {
            BtnSalvar.IsEnabled = true;
            pickerGenero.IsEnabled = true;
        }

        private void BtnEditarEmail_Clicked(object sender, System.EventArgs e)
        {
            BtnSalvar.IsEnabled = true;
            txtEmail.IsEnabled = true;
        }

        private void BtnEditarTelefone_Clicked(object sender, System.EventArgs e)
        {
            BtnSalvar.IsEnabled = true;
            txtPhone.IsEnabled = true;
        }

        private void BtnEditarCep_Clicked(object sender, System.EventArgs e)
        {
            BtnSalvar.IsEnabled = true;
            TxtCep.IsEnabled = true;
        }

        private void BtnEditarUf_Clicked(object sender, System.EventArgs e)
        {
            BtnSalvar.IsEnabled = true;
            txtUf.IsEnabled = true;
        }

        private void BtnEditarCidade_Clicked(object sender, System.EventArgs e)
        {
            BtnSalvar.IsEnabled = true;
            txtCidade.IsEnabled = true;
        }

        private void BtnEditarBairro_Clicked(object sender, System.EventArgs e)
        {
            BtnSalvar.IsEnabled = true;
            txtBairro.IsEnabled = true;
        }

        private void BtnEditarRua_Clicked(object sender, System.EventArgs e)
        {
            BtnSalvar.IsEnabled = true;
            txtRua.IsEnabled = true;
        }

        private void BtnEditarNumero_Clicked(object sender, System.EventArgs e)
        {
            BtnSalvar.IsEnabled = true;
            txtNumero.IsEnabled = true;
        }
    }
}
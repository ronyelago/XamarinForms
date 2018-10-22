using AutoMapper;
using LabClick.ViewModels;
using System;
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

            this.NewPatientViewModel = new NewPatientViewModel();
            this.NewPatientViewModel = Mapper.Map<NewPatientViewModel>(paciente);
            this.BindingContext = NewPatientViewModel;

            if (Device.Idiom == TargetIdiom.Phone)
            {
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
                TxtCep.WidthRequest = 200;
                TxtCep.FontSize = 13;

                lblUf.FontSize = 13;
                lblUf.Margin = new Thickness(0, 0, 10, 0);
                txtUf.WidthRequest = 200;

                lblCidade.FontSize = 13;
                txtCidade.WidthRequest = 200;
                txtCidade.FontSize = 13;

                lblBairro.FontSize = 13;
                txtBairro.WidthRequest = 200;
                txtBairro.FontSize = 13;

                txtRua.WidthRequest = 200;
                txtRua.FontSize = 12;
                txtRua.AnchorX = -3;
                txtRua.AnchorY = 0;

                lblNumero.FontSize = 13;
                txtNumero.WidthRequest = 200;
                txtNumero.FontSize = 13;

                BtnSalvar.WidthRequest = 150;
                BtnSalvar.HeightRequest = 30;
                BtnSalvar.FontSize = 8;
                BtnSalvar.CornerRadius = 10;

                BtnEditar.WidthRequest = 150;
                BtnEditar.HeightRequest = 30;
                BtnEditar.FontSize = 8;
                BtnEditar.CornerRadius = 10;

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

                if (result.IsSuccessStatusCode)
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

        private void BtnEditar_Clicked(object sender, System.EventArgs e)
        {
            txtNome.IsEnabled = true;
            dataNascimentoPicker.IsEnabled = true;
            txtCpf.IsEnabled = true;
            pickerGenero.IsEnabled = true;
            txtEmail.IsEnabled = true;
            txtPhone.IsEnabled = true;
            TxtCep.IsEnabled = true;
            txtUf.IsEnabled = true;
            txtCidade.IsEnabled = true;
            txtBairro.IsEnabled = true;
            txtRua.IsEnabled = true;
            txtNumero.IsEnabled = true;

            BtnCancelar.Text = "Cancelar";
            BtnEditar.IsVisible = false;
            BtnSalvar.IsVisible = true;
        }

        private void txtEmail_Unfocused(object sender, FocusEventArgs e)
        {
            if (NewPatientViewModel.EmailValidate(NewPatientViewModel.Email) || String.IsNullOrEmpty(NewPatientViewModel.Email))
            {
                txtWarning.IsVisible = false;
            }
            else
            {
                txtWarning.IsVisible = true;
            }
        }

        private void txtCpf_Unfocused(object sender, FocusEventArgs e)
        {
            if (NewPatientViewModel.CpfValidade(NewPatientViewModel.Cpf) || String.IsNullOrEmpty(NewPatientViewModel.Cpf))
            {
                txtCpfValidate.IsVisible = false;
            }
            else
            {
                txtCpfValidate.IsVisible = true;
            }
        }
    }
}
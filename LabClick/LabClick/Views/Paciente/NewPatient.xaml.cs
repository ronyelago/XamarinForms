﻿using AutoMapper;
using LabClick.ViewModels;
using Newtonsoft.Json;
using System;
using System.Net.Http;
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
            this.dataNascimento.Date = DateTime.Now;
            this.txtNumero.Text = string.Empty;
		}

        private void BtnSalvar_Clicked(object sender, EventArgs e)
        {
            //BackgroundColor = #80126683

            Domain.Entities.Paciente paciente;
            Domain.Entities.Endereco endereco;
            var newPatientViewModel = this.BindingContext as NewPatientViewModel;

            if (newPatientViewModel.IsValid())
            {
                paciente = Mapper.Map<Domain.Entities.Paciente>(newPatientViewModel);
                endereco = Mapper.Map<Domain.Entities.Endereco>(newPatientViewModel);
            }
        }

        private void TxtCep_Unfocused(object sender, FocusEventArgs e)
        {
            if (TxtCep.Text != null && TxtCep.Text != string.Empty)
            {
                Uri urinho = new Uri($@"http://viacep.com.br/ws/{TxtCep.Text}/json/");
                HttpClient client = new HttpClient();

                var result = client.GetAsync(urinho);

                if (result.Result.IsSuccessStatusCode)
                {
                    var content = result.Result.Content.ReadAsStringAsync();
                    var endereco = JsonConvert.DeserializeObject<EnderecoViewModel>(content.Result);

                    if (endereco.IsValid())
                    {
                        txtUf.Text = endereco.Uf;
                        txtCidade.Text = endereco.Localidade;
                        txtBairro.Text = endereco.Bairro;
                        txtRua.Text = endereco.Logradouro;

                        txtNumero.Focus();
                    }
                    else
                    {
                        DisplayAlert("Erro", "Endereço não localizado. Tente novamente ou preencha manualmente.", "Ok");
                    }
                }
                else
                {
                    DisplayAlert("Erro", "Endereço não localizado. Tente novamente ou preencha manualmente.", "Ok");
                }
            }
        }

        private void BtnCancelar_Clicked(object sender, EventArgs e)
        {

        }
    }
}
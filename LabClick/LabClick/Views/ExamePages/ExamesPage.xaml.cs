using LabClick.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LabClick.Views.ExamePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ExamesPage : ContentPage
	{
        private Paciente paciente;

		public ExamesPage (Paciente paciente)
		{
			InitializeComponent ();
            this.paciente = paciente;
        }

        private void ExamesPage_Appearing(object sender, EventArgs e)
        {
            var result = App.Client.GetAsync($@"http://192.168.0.15:3000/teste/getAllByPacienteId={paciente.Id}");
            var content = result.Result.Content.ReadAsStringAsync();

            var testes = JsonConvert.DeserializeObject<List<Teste>>(content.Result);

            ExamesList.ItemsSource = testes;
        }
    }
}
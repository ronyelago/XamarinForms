using LabClick.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LabClick.Views.ExamePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ExamesPage : ContentPage
	{
        private Paciente paciente;
        private Teste teste;

		public ExamesPage (Paciente paciente)
		{
			InitializeComponent ();
            this.paciente = paciente;
        }

        private void ExamesPage_Appearing(object sender, EventArgs e)
        {
            var result = App.Client.GetAsync($@"http://apilabclick.mflogic.com.br/teste/getAllByPacienteId={paciente.Id}");
            var content = result.Result.Content.ReadAsStringAsync();

            var testes = JsonConvert.DeserializeObject<List<Teste>>(content.Result);

            ExamesList.ItemsSource = testes;
        }

        private async Task ExamesList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            this.teste = (Teste)ExamesList.SelectedItem;

            await App.NavigateMasterDetail(new ExameSelecionadoPage(teste));
        }
    }
}
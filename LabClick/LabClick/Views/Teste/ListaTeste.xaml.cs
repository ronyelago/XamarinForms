using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LabClick.Views.Teste
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListaTeste : ContentPage
	{
        private Models.Paciente paciente;
        private Models.Teste teste;

		public ListaTeste(Models.Paciente paciente)
		{
			InitializeComponent ();
            this.paciente = paciente;
        }

        private void ExamesPage_Appearing(object sender, EventArgs e)
        {
            var result = App.Client.GetAsync($@"http://apilabclick.mflogic.com.br/teste/getAllByPacienteId={paciente.Id}");
            var content = result.Result.Content.ReadAsStringAsync();

            var testes = JsonConvert.DeserializeObject<List<Models.Teste>>(content.Result);

            ListaTestes.ItemsSource = testes;
        }

        private async Task ExamesList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            this.teste = (Models.Teste)ListaTestes.SelectedItem;

            await App.NavigateMasterDetail(new TesteSelecionado(teste));
        }
    }
}
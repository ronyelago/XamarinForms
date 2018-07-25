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
        private Domain.Entities.Teste teste;

		public ListaTeste(Models.Paciente paciente)
		{
			InitializeComponent ();
            this.paciente = paciente;
        }

        private void ExamesPage_Appearing(object sender, EventArgs e)
        {
            var result = App.Client.GetAsync($@"http://apilabclick.mflogic.com.br/teste/getAllByPacienteId={paciente.Id}");
            var content = result.Result.Content.ReadAsStringAsync();

            var testes = JsonConvert.DeserializeObject<List<Domain.Entities.Teste>>(content.Result);

            TestesLista.ItemsSource = testes;
        }

        private async Task TestesLista_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            this.teste = (Domain.Entities.Teste)TestesLista.SelectedItem;

            await App.NavigateMasterDetail(new LaudoTeste(teste.PacienteId));
        }
    }
}
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LabClick.Views.Paciente
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PesquisarPaciente : ContentPage
	{
        List<Models.Paciente> pacientes = new List<Models.Paciente>();

        public PesquisarPaciente ()
		{
			InitializeComponent ();
		}

        private async Task PacienteSearchBar_SearchButtonPressed(object sender, System.EventArgs e)
        {
            var client = new HttpClient();
            var result = await client.GetAsync($@"http://apilabclick.mflogic.com.br/paciente/getByName={PacienteSearchBar.Text}");
            var content = result.Content.ReadAsStringAsync();

            pacientes = JsonConvert.DeserializeObject<List<Models.Paciente>>(content.Result);

            //melhorar isso aqui!
            List<string> nomes = new List<string>();

            foreach (var p in pacientes)
            {
                nomes.Add(p.Nome);
            }
            
            PacientesListView.ItemsSource = nomes;
        }

        private void PacientesListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var paciente = pacientes.FirstOrDefault(p => p.Nome == PacientesListView.SelectedItem.ToString());
            var pacientePage = new PacienteMainPage(paciente);
            
            Navigation.PushAsync(pacientePage);
        }
    }
}
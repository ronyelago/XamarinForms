using LabClick.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LabClick.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PesquisarPacientePage : ContentPage
	{
		public PesquisarPacientePage ()
		{
			InitializeComponent ();
		}

        private async Task PacienteSearchBar_SearchButtonPressed(object sender, System.EventArgs e)
        {
            var client = new HttpClient();
            var result = await client.GetAsync($@"http://192.168.0.15:3000/paciente/getByName={PacienteSearchBar.Text}");
            var content = result.Content.ReadAsStringAsync();

            var pacientes = JsonConvert.DeserializeObject<List<Paciente>>(content.Result);

            List<string> nomes = new List<string>();

            foreach (var p in pacientes)
            {
                nomes.Add(p.Nome);
            }

            PacientesListView.ItemsSource = nomes;
        }
    }
}
using LabClick.Views.Teste;
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
        List<Domain.Entities.Paciente> pacientes = new List<Domain.Entities.Paciente>();
        public List<SearchListViewItem> ListSearchItem { get; set; }
        public bool Scanning { get; set; }

        // construtor com parâmetro que indica que
        // a navegação será direto para page de digitalização 
        public PesquisarPaciente(bool scanning)
        {
            InitializeComponent();

            this.Scanning = scanning;
            this.MainLabel.Text = "Selecione o Paciente";
        }

        // construtor sem parâmetros que indica que
        // a navegação será para a page de paciente
        public PesquisarPaciente ()
		{
			InitializeComponent ();

            this.MainLabel.Text = "Busca de Paciente";
        }

        private async Task PacienteSearchBar_SearchButtonPressed(object sender, System.EventArgs e)
        {
            // Busca os pacientes pelo nome
            var client = new HttpClient();
            var result = await client.GetAsync($@"http://apilabclick.mflogic.com.br/paciente/getByName={PacienteSearchBar.Text}");
            var content = result.Content.ReadAsStringAsync();

            // cria lista com retorno da busca
            pacientes = JsonConvert.DeserializeObject<List<Domain.Entities.Paciente>>(content.Result);

            // lista de itens que possuem nome e cor
            ListSearchItem = new List<SearchListViewItem>();

            // inserindo os nomes e números que definirão o background
            for (int i = 0; i < pacientes.Count; i++)
            {
                ListSearchItem.Add(new SearchListViewItem { Name = pacientes[i].Nome, SetColor = i });
            }

            // define o background de cada ítem da lista de acordo com o número (ímpar ou par)
            foreach (var item in ListSearchItem)
            {
                if (item.SetColor % 2 == 0)
                {
                    //verde com 90% de transparência
                    item.Color = Color.FromHex("#1A126683");
                }
                else
                {
                    //branco com 50% de transparência
                    item.Color = Color.FromHex("#80FFFFFF");
                }
            }

            // Adiciona os ítens à lista resultante da busca
            PacientesListView.ItemsSource = ListSearchItem;
        }

        private void PacientesListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            // captura o ítem que foi selecionado na lista
            var item = (SearchListViewItem)e.Item;

            // busca na lista de pacientes o paciente com o nome 
            // selecionado na lista
            var paciente = pacientes.FirstOrDefault(p => p.Nome == item.Name);

            PacientesListView.SelectedItem = null;

            
            // Se a navegação for para digitalizar exame
            // o usuário é direcionado direto para a page DigitalizarTeste
            // com o paciente selecionado na lista.
            // Do contrário, o usuário é direcionado para a page com
            // os dados do paciente selecionado 
            if (Scanning)
            {
                Navigation.PushAsync(new DigitalizarTeste(paciente));
            }
            else
            {
                var pacienteMasterPage = new PacienteMasterPage(paciente);
                App.MasterPage = pacienteMasterPage;

                Navigation.PushAsync(pacienteMasterPage);
            }
        }
    }
}
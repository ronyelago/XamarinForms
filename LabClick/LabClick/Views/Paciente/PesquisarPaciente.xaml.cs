using LabClick.Views.Teste;
using Newtonsoft.Json;
using System;
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

            if (Device.Idiom == TargetIdiom.Phone)
            {
                PacienteSearchBar.HeightRequest = 40;
                PacienteSearchBar.WidthRequest = 100;
                PacienteSearchBar.FontSize = 15;
            }
            else
            {

            }
        }

        // construtor sem parâmetros que indica que
        // a navegação será para a page de paciente
        public PesquisarPaciente ()
		{
			InitializeComponent ();

            this.BindingContext = 

            this.MainLabel.Text = "Busca de Paciente";

            if (Device.Idiom == TargetIdiom.Phone)
            {
                PacienteSearchBar.HeightRequest = 40;
                PacienteSearchBar.WidthRequest = 100;
                PacienteSearchBar.FontSize = 15;
                PacientesListView.RowHeight = 50;
            }
            else
            {

            }
        }

        private async void PacienteSearchBar_SearchButtonPressed(object sender, System.EventArgs e)
        {
            var client = new HttpClient() { Timeout = TimeSpan.FromSeconds(30) };

            try
            {
                await Navigation.PushAsync(App.LoadingPage);

                var result = await client.GetAsync($@"http://apilabclick.mflogic.com.br/paciente/getByName={PacienteSearchBar.Text}");
                var content = result.Content.ReadAsStringAsync();

                // cria lista com retorno da busca
                pacientes = JsonConvert.DeserializeObject<List<Domain.Entities.Paciente>>(content.Result);

                if (pacientes != null)
                {
                    // lista de itens que possuem nome e cor
                    ListSearchItem = new List<SearchListViewItem>();

                    // inserindo os nomes e números que definirão o background
                    for (int i = 0; i < pacientes.Count; i++)
                    {
                        ListSearchItem.Add(new SearchListViewItem { Name = pacientes[i].Nome, SetColor = i });
                    }

                    // define o background de cada ítem da lista
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

                        item.FontSize = Device.Idiom == TargetIdiom.Phone ? 10 : 15;
                    }

                    // Adiciona os ítens à lista resultante da busca
                    PacientesListView.ItemsSource = ListSearchItem;

                    Navigation.RemovePage(App.LoadingPage);
                }

                else
                {
                    await DisplayAlert("Resultado da busca", "Nenhum paciente encontrado.", "Fechar");
                    Navigation.RemovePage(App.LoadingPage);
                    this.PacienteSearchBar.Text = string.Empty;
                }
            }

            catch (TaskCanceledException ex)
            {
                await DisplayAlert("Erro", $"Servidor indisponível \n {ex.Message} \n {ex.InnerException.Message}", "Fechar");
                Navigation.RemovePage(App.LoadingPage);
            }
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
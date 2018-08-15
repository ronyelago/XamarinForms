﻿using LabClick.Views.Teste;
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

        public PesquisarPaciente(bool scanning)
        {
            InitializeComponent();

            this.Scanning = scanning;
            this.Icon = "digitalizarexame.png";
            this.MainLabel.Text = "Selecione o Paciente";
        }

        public PesquisarPaciente ()
		{
			InitializeComponent ();

            this.MainLabel.Text = "Busca de Paciente";
        }

        private async Task PacienteSearchBar_SearchButtonPressed(object sender, System.EventArgs e)
        {
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
            }

            PacientesListView.ItemsSource = ListSearchItem;
        }

        private void PacientesListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = (SearchListViewItem)e.Item;
            item.Color = Color.Blue;

            var paciente = pacientes.FirstOrDefault(p => p.Nome == item.Name);
            var pacienteMasterPage = new PacienteMasterPage(paciente);
            App.MasterPage = pacienteMasterPage;

            PacientesListView.SelectedItem = null;

            if (Scanning)
            {
                Navigation.PushAsync(new DigitalizarTeste(paciente));
            }
            else
            {
                Navigation.PushAsync(pacienteMasterPage);
            }
        }
    }
}
﻿using Newtonsoft.Json;
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

        public PesquisarPaciente ()
		{
			InitializeComponent ();

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

            // inserindo os nomes e números 
            for (int i = 0; i < pacientes.Count; i++)
            {
                ListSearchItem.Add(new SearchListViewItem { Name = pacientes[i].Nome, SetColor = i });
            }

            foreach (var item in ListSearchItem)
            {
                if (item.SetColor % 2 == 0)
                {
                    item.Color = Color.FromHex("#EAF4F5");
                }
                else
                {
                    item.Color = Color.White;
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
            Navigation.PushAsync(pacienteMasterPage);
        }
    }
}
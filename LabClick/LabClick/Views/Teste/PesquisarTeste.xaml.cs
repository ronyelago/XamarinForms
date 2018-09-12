using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LabClick.Views.Teste
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PesquisarTeste : ContentPage
	{
        List<Domain.Entities.Teste> testes = new List<Domain.Entities.Teste>();
        List<TestListViewItem> ListItems;

        public PesquisarTeste ()
		{
			InitializeComponent ();
		}

        private async void PesquisarTesteSearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            var client = new HttpClient() { Timeout = TimeSpan.FromSeconds(60) };

            try
            {
                await Navigation.PushAsync(App.LoadingPage);

                // Busca os testes pelo nome ou trecho do nome do paciente
                var result = await client.GetAsync($@"http://apilabclick.mflogic.com.br/teste/getAllByPatientName={PesquisarTesteSearchBar.Text}");
                var content = result.Content.ReadAsStringAsync();

                // cria lista com retorno da busca
                testes = JsonConvert.DeserializeObject<List<Domain.Entities.Teste>>(content.Result);

                if (testes != null)
                {
                    // source da lista de ítens apresentados na page
                    ListItems = new List<TestListViewItem>();

                    for (int i = 0; i < testes.Count; i++)
                    {
                        ListItems.Add(new TestListViewItem()
                        {
                            SetColor = i,
                            NomeTeste = testes[i].Exame.Nome,
                            NomePaciente = $"Paciente: {testes[i].Paciente.Nome}",
                            PacienteId = testes[i].Paciente.Id,
                            Status = testes[i].Status,
                            DataTeste = $"Data do Exame: {testes[i].DataCadastro.ToShortDateString()}"
                        });
                    }

                    // definição dos backgrounds
                    foreach (var item in ListItems)
                    {
                        if (item.SetColor % 2 == 0)
                        {
                            item.Color = Color.FromHex("#1A126683");
                        }

                        else
                        {
                            item.Color = Color.FromHex("#80FFFFFF");
                        }
                    }

                    // Adiciona os ítens à lista resultante da busca
                    TestesListView.ItemsSource = ListItems;
                    Navigation.RemovePage(App.LoadingPage);
                }

                else
                {
                    await DisplayAlert("Resultado da busca", "Nenhum paciente encontrado.", "Fechar");
                    Navigation.RemovePage(App.LoadingPage);
                    this.PesquisarTesteSearchBar.Text = string.Empty;
                }
            }

            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Servidor indisponível \n {ex.Message} \n {ex.InnerException.Message}", "Fechar");
                Navigation.RemovePage(App.LoadingPage);
            }
        }

        private void TestesListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var t = e.Item as TestListViewItem;
            var teste = testes.FirstOrDefault(m => m.PacienteId == t.PacienteId);

            Navigation.PushAsync(new TestDetails(teste), true);
        }

        private void BtnDetalhar_Clicked(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            var parent = btn.Parent;
            int pacienteId = int.Parse(parent.FindByName<Label>("LblPacienteId").Text);
            var teste = testes.FirstOrDefault(t => t.PacienteId == pacienteId);

            Navigation.PushAsync(new TestDetails(teste), true);
        }
    }
}
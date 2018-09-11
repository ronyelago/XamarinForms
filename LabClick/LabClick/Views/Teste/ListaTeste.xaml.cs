using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LabClick.Views.Teste
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListaTeste : ContentPage
	{
        List<TestListViewItem> ListItems;
        private int pacienteId;

		public ListaTeste(Domain.Entities.Paciente paciente)
		{
			InitializeComponent ();
            pacienteId = paciente.Id;
        }

        // Evento Appearing (aparecer) carrega todos os exames de um paciente
        private async void TestList_Appearing(object sender, EventArgs e)
        {
            var client = new HttpClient() { Timeout = TimeSpan.FromSeconds(60) };

            try
            {
                await Navigation.PushAsync(App.LoadingPage);

                var result = await client.GetAsync($@"http://apilabclick.mflogic.com.br/teste/getAllByPacienteId={pacienteId}");
                var content = result.Content.ReadAsStringAsync();

                // Lista de testes de um paciente
                var testes = JsonConvert.DeserializeObject<List<Domain.Entities.Teste>>(content.Result);

                if (testes != null)
                {
                    ListItems = new List<TestListViewItem>();

                    // preenchimento da lista
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

                    TestesListView.ItemsSource = ListItems;
                    Navigation.RemovePage(App.LoadingPage);
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

        }

        private void BtnDetalhar_Clicked(object sender, EventArgs e)
        {

        }
    }
}
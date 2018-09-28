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

            if (Device.Idiom == TargetIdiom.Phone)
            {
                PesquisarTesteSearchBar.HeightRequest = 40;
                PesquisarTesteSearchBar.WidthRequest = 100;
                PesquisarTesteSearchBar.FontSize = 15;
            }
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

                if (testes.Count > 0)
                {
                    // source da lista de ítens apresentados na page
                    ListItems = new List<TestListViewItem>();

                    for (int i = 0; i < testes.Count; i++)
                    {
                        ListItems.Add(new TestListViewItem()
                        {
                            SetColor = i,
                            NomeTeste = testes[i].Exame.Nome,
                            TesteId = testes[i].Id,
                            NomePaciente = $"Paciente: {testes[i].Paciente.Nome}",
                            PacienteId = testes[i].Paciente.Id,
                            Status = testes[i].Status,
                            DataTeste = $"Data do Exame: {testes[i].DataCadastro.ToShortDateString()}"
                        });
                    }

                    // definição dos backgrounds e responsividade para smartphones
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

                        if (Device.Idiom == TargetIdiom.Phone)
                        {
                            item.LeftHeaderStackWidth = 150;
                            item.LabelNomeTesteFontSize = 12;
                            item.LabelNomePacienteFontSize = 12;
                            item.LabelDataTesteFontSize = 12;
                            item.CenterHeaderStackWidth = 100;
                            item.LabelStatusFontSize = 12;
                            item.RightHeaderStackWidth = 100;
                            item.IsEnable = true;
                        }
                    }

                    // Adiciona os ítens à lista resultante da busca
                    TestesListView.ItemsSource = ListItems;
                    Navigation.RemovePage(App.LoadingPage);
                }

                else
                {
                    TestesListView.ItemsSource = null;
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
            TestesListView.IsEnabled = false;

            var t = e.Item as TestListViewItem;
            var teste = testes.FirstOrDefault(test => test.Id == t.TesteId);

            Navigation.PushAsync(new TestDetails(teste), true);
            TestesListView.SelectedItem = null;
        }

        private void BtnDetalhar_Clicked(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            var parent = btn.Parent;
            int testeId = int.Parse(parent.FindByName<Label>("LblTesteId").Text);
            var teste = testes.FirstOrDefault(test => test.Id == testeId);

            Navigation.PushAsync(new TestDetails(teste), true);
        }
    }
}
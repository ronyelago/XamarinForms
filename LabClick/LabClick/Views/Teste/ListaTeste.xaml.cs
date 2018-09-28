using LabClick.Views.Paciente;
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
	public partial class ListaTeste : ContentPage
	{
        private List<TestListViewItem> ListItems;
        private List<Domain.Entities.Teste> listTests;
        private Domain.Entities.Paciente paciente;
        private int pacienteId;

		public ListaTeste(Domain.Entities.Paciente paciente)
		{
			InitializeComponent ();
            pacienteId = paciente.Id;
            this.paciente = paciente;
        }

        // Evento Appearing => carrega todos os exames de um paciente
        private async void TestList_Appearing(object sender, EventArgs e)
        {
            var client = new HttpClient();
            this.Loading.IsRunning = true;

            try
            {
                var result = await client.GetAsync($@"http://apilabclick.mflogic.com.br/teste/getAllByPacienteId={pacienteId}");
                var content = result.Content.ReadAsStringAsync();

                // Lista de testes de um paciente
                listTests = JsonConvert.DeserializeObject<List<Domain.Entities.Teste>>(content.Result);

                if (listTests != null && listTests.Count > 0)
                {
                    ListItems = new List<TestListViewItem>();

                    // preenchimento da lista
                    for (int i = 0; i < listTests.Count; i++)
                    {
                        ListItems.Add(new TestListViewItem()
                        {
                            SetColor = i,
                            NomeTeste = listTests[i].Exame.Nome,
                            NomePaciente = $"Paciente: {listTests[i].Paciente.Nome}",
                            PacienteId = listTests[i].Paciente.Id,
                            Status = listTests[i].Status,
                            DataTeste = $"Data do Exame: {listTests[i].DataCadastro.ToShortDateString()}"
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

                    this.Loading.IsRunning = false;
                    this.Loading.IsVisible = false;
                    this.loadingAbsoluteLayout.IsVisible = false;

                    TestesListView.ItemsSource = ListItems;
                }

                else
                {
                    await DisplayAlert("Aviso", $"Nenhum exame encontrado.", "Fechar");

                    this.Loading.IsRunning = false;
                    this.Loading.IsVisible = false;

                    await Navigation.PushAsync(new PacienteMasterPage(this.paciente));
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Não foi possível carregar a lista de testes.", "Fechar");
                this.Loading.IsRunning = false;
                this.Loading.IsVisible = false;
            }
        }

        private void TestesListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var t = e.Item as TestListViewItem;
            var teste = listTests.FirstOrDefault(m => m.PacienteId == t.PacienteId);

            Navigation.PushAsync(new TestDetails(teste), true);
        }

        private void BtnDetalhar_Clicked(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.IsEnabled = false;
            var parent = btn.Parent;
            int pacienteId = int.Parse(parent.FindByName<Label>("LblPacienteId").Text);
            var teste = listTests.FirstOrDefault(t => t.PacienteId == pacienteId);

            Navigation.PushAsync(new TestDetails(teste), true);
        }
    }
}
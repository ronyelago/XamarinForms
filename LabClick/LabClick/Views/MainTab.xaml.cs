using LabClick.Views.Paciente;
using LabClick.Views.Teste;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LabClick.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainTab : TabbedPage
    {
        public MainTab (int page)
        {
            InitializeComponent();

            // Adiciona à MainTab uma page PesquisarPaciente [0]
            // Pesquisa o paciente e exibe view de digitalizar exame
            // Flag no construtor para redirecionar para view de digitalizar exame
            this.Children.Add(new PesquisarPaciente(true)
            {
                Icon = "smallscan.png",
                Title = "Digitalizar Teste"
            });

            // Adiciona à MainTab uma page PesquisarPaciente [1]
            // Pesquisa o paciente e exibe o mesmo
            this.Children.Add(new PesquisarPaciente()
            {
                Icon = "smallsearch.png",
                Title = "Pesquisar Paciente"
            });

            // Adiciona à MainTab uma page NewPatient [2]
            // Exibe page de novo paciente
            this.Children.Add(new NewPatient()
            {
                Icon = "smalladd.png",
                Title = "Novo Paciente"
            });

            // Adiciona à MainTab uma page PesquisarTeste [3]
            this.Children.Add(new PesquisarTeste()
            {
                Icon = "smallreports.png",
                Title = "Pesquisar Exame"
            });

            // Define a página que será exibida de acordo com o parâmetro page(int)
            this.CurrentPage = this.Children[page];

            this.BackgroundImage = "footer.png";
            this.BarBackgroundColor = Color.FromHex("#126683");
        }
    }
}
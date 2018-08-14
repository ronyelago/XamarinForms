using LabClick.Views.Paciente;
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

            // Pesquisa o paciente e exibe view de digitalizar exame
            // Flag no construtor para redirecionar para view de digitalizar exame
            this.Children.Add(new PesquisarPaciente(true));
            
            // Pesquisa o paciente e exibe o mesmo
            this.Children.Add(new PesquisarPaciente() { Icon = "pesquisar.png", Title = "Pesquisar Paciente" });

            // Exibe view de novo paciente
            this.Children.Add(new NovoPaciente() { Icon = "novo.png", Title = "Novo Paciente" });

            this.CurrentPage = this.Children[page];

            
        }
    }
}
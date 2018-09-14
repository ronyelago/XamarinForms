using System.Drawing;

namespace LabClick.Views.Teste
{
    public class TestListViewItem
    {
        public string NomeTeste { get; set; }
        public int TesteId { get; set; }
        public string NomePaciente { get; set; }
        public int PacienteId { get; set; }
        public string Status { get; set; }
        public string Resultado { get; set; }
        public string ResultadoDetalhes { get; set; }
        public byte[] LaudoPdf { get; set; }
        public Color Color { get; set; }
        public int SetColor { get; set; }
        public string DataTeste { get; set; }
    }
}
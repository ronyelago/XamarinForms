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
        public string DataTeste { get; set; }
        public Color Color { get; set; }
        public int SetColor { get; set; }
        public double LeftHeaderStackWidth { get; set; }
        public double LabelNomeTesteFontSize { get; set; }
        public double LabelNomePacienteFontSize { get; set; }
        public double LabelDataTesteFontSize { get; set; }
        public double CenterHeaderStackWidth { get; set; }
        public double LabelStatusFontSize { get; set; }
        public double RightHeaderStackWidth { get; set; }
    }
}
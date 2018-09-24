using LabClick.Domain.Entities;

namespace LabClick.ViewModels
{
    public class TestDetailsViewModel
    {
        private string resultado;
        private string resultadoDetalhes;

        public string Resultado
        {
            get { return this.resultado; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    this.resultado = "";
                else
                    this.resultado = value;
            }
        }

        public string ResultadoDetalhes
        {
            get { return this.resultadoDetalhes; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    this.resultadoDetalhes = "Exame em análise laboratorial";
                else this.resultadoDetalhes = value;
            }
        }

        public int Id { get; set; }
        public int ExameId { get; set; }
        public int ClinicaId { get; set; }
        public int PacienteId { get; set; }
        public string Code { get; set; }
        public string Status { get; set; }
        public bool LaudoOk { get; set; }
        public string DataCadastro { get; set; }
        public virtual Exame Exame { get; set; }
        public virtual Clinica Clinica { get; set; }
        public virtual Paciente Paciente { get; set; }
        public virtual Laudo Laudo { get; set; }
    }
}

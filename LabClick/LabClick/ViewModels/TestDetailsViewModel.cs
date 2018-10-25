using LabClick.Domain.Entities;

namespace LabClick.ViewModels
{
    public class TestDetailsViewModel
    {
        private string resultado;
        private string resultadoDetalhes;
        private string observacoes;

        public string Resultado
        {
            get { return string.Format($"{this.resultado} {this.ResultadoDetalhes}"); }
            set
            {
                if (string.IsNullOrEmpty(value))
                    this.resultado = "Não avaliado";
                else
                    this.resultado = value;
            }
        }

        /// <summary>
        /// Somente para resultado Indeterminado 
        /// ou Positivo (IGG reagente, IGM reagente ou IGG e IGM reagente)
        /// </summary>
        public string ResultadoDetalhes
        {
            get { return this.resultadoDetalhes; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    this.resultadoDetalhes = "";
                else this.resultadoDetalhes = $" - {value}";
            }
        }

        public string Observacoes
        {
            get { return this.observacoes; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    this.observacoes = "Não avaliado";
                else
                    this.observacoes = value;
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

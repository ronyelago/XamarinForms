using System;

namespace LabClick.Models
{
    public class Teste
    {
        public int Id { get; set; }
        public int ExameId { get; set; }
        public int ClinicaId { get; set; }
        public int PacienteId { get; set; }
        public byte[] Imagem { get; set; }
        public string Status { get; set; }
        public virtual Exame Exame { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}

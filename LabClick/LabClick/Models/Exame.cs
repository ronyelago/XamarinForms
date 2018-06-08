using System;

namespace LabClick.Models
{
    public class Exame
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int ClinicaId { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}

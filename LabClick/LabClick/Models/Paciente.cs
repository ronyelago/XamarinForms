using System;

namespace LabClick.Models
{
    public class Paciente
    {
        public string Nome { get; set; }
        public string Sexo { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Cpf { get; set; }
        public int ClinicaId { get; set; }
        public int EnderecoId { get; set; }
        public DateTime DataNascimento { get; set; }
        public virtual Endereco Endereco { get; set; }
    }
}

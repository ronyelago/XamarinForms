using System;

namespace LabClick.ViewModels
{
    public class PacienteViewModel
    {
        public int ClinicaId { get; set; }
        public int EnderecoId { get; set; }
        public string Nome { get; set; }
        public string Sexo { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }

        public string Cep { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public int Numero { get; set; }
        public string Bairro { get; set; }
        public string Logradouro { get; set; }
    }
}

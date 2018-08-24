using LabClick.Domain.Entities;
using System;

namespace LabClick.ViewModels
{
    public class TestDetailsViewModel
    {
        public int Id { get; set; }
        public int ExameId { get; set; }
        public int ClinicaId { get; set; }
        public int PacienteId { get; set; }
        public byte[] Imagem { get; set; }
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

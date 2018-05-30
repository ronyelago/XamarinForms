using LabClick.Models;

namespace LabClick.ViewModels
{
    public class PacienteViewModel
    {
        public PacienteViewModel()
        {
            Paciente = new Paciente();
            Endereco = new Endereco();
        }

        public Paciente Paciente { get; set; }
        public Endereco Endereco { get; set; }
    }
}

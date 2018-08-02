
namespace LabClick.ViewModels
{
    public class PacienteViewModel
    {
        public PacienteViewModel()
        {
            Paciente = new Domain.Entities.Paciente();
            Endereco = new Domain.Entities.Endereco();
        }

        public Domain.Entities.Paciente Paciente { get; set; }
        public Domain.Entities.Endereco Endereco { get; set; }
    }
}

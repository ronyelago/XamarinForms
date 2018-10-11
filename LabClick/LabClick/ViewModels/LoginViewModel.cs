using LabClick.Domain.Entities;

namespace LabClick.ViewModels
{
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Senha { get; set; }
        public int ClinicaId { get; set; }
        public Clinica Clinica { get; set; }
    }
}

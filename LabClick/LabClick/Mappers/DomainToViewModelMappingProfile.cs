using AutoMapper;
using LabClick.ViewModels;

namespace LabClick.Mappers
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Domain.Entities.Paciente, PacienteViewModel>();
            CreateMap<Domain.Entities.Paciente, NewPatientViewModel>();
            CreateMap<Domain.Entities.Endereco, NewPatientViewModel>();
        }
    }
}

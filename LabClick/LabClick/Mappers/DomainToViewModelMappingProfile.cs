using AutoMapper;
using LabClick.ViewModels;

namespace LabClick.Mappers
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Domain.Entities.Paciente, PacienteViewModel>();
        }
    }
}

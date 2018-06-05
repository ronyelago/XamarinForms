using AutoMapper;
using LabClick.Models;
using LabClick.ViewModels;

namespace LabClick.Mappers
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Paciente, PacienteViewModel>();
        }
    }
}

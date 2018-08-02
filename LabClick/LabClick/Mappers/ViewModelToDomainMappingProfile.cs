using AutoMapper;
using LabClick.ViewModels;

namespace LabClick.Mappers
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<PacienteViewModel, Domain.Entities.Paciente>();
        }
    }
}

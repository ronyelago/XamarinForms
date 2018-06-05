using AutoMapper;
using LabClick.Models;
using LabClick.ViewModels;

namespace LabClick.Mappers
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<PacienteViewModel, Paciente>();
        }
    }
}

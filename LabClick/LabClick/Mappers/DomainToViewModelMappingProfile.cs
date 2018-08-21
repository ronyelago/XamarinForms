using AutoMapper;
using LabClick.ViewModels;

namespace LabClick.Mappers
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Domain.Entities.Paciente, PacienteViewModel>();
            CreateMap<Domain.Entities.Paciente, NewPatientViewModel>()
                .ForMember(end => end.Cep,
                           opt => opt.MapFrom( e => e.Endereco.Cep));
            CreateMap<Domain.Entities.Endereco, NewPatientViewModel>();
            CreateMap<Domain.Entities.Teste, DigitalizarTesteViewModel>();
        }
    }
}

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
                .ForMember(end => end.Cep, opt => opt.MapFrom(p => p.Endereco.Cep))
                .ForMember(end => end.UF, opt => opt.MapFrom(p => p.Endereco.UF))
                .ForMember(end => end.Cidade, opt => opt.MapFrom(p => p.Endereco.Cidade))
                .ForMember(end => end.Bairro, opt => opt.MapFrom(p => p.Endereco.Bairro))
                .ForMember(end => end.Logradouro, opt => opt.MapFrom(p => p.Endereco.Logradouro))
                .ForMember(end => end.Numero, opt => opt.MapFrom(p => p.Endereco.Numero));
            CreateMap<Domain.Entities.Endereco, NewPatientViewModel>();
            CreateMap<Domain.Entities.Teste, DigitalizarTesteViewModel>();
        }
    }
}

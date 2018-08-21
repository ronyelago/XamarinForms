using AutoMapper;
using LabClick.ViewModels;

namespace LabClick.Mappers
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<PacienteViewModel, Domain.Entities.Paciente>();

            CreateMap<NewPatientViewModel, Domain.Entities.Paciente>();

            CreateMap<NewPatientViewModel, Domain.Entities.Endereco>()
                .ForMember(m => m.Id, e => e.MapFrom(p => p.EnderecoId));

            CreateMap<DigitalizarTesteViewModel, Domain.Entities.Teste>();

            CreateMap<EnderecoViewModel, Domain.Entities.Endereco>()
                .ForMember(e => e.Cidade, c => c.MapFrom(cid => cid.Localidade));
        }
    }
}

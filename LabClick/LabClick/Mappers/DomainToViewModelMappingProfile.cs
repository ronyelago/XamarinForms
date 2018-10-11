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
                .ForMember(end => end.EnderecoId, opt => opt.MapFrom(p => p.Endereco.Id))
                .ForMember(end => end.Cep, opt => opt.MapFrom(p => p.Endereco.Cep))
                .ForMember(end => end.UF, opt => opt.MapFrom(p => p.Endereco.UF))
                .ForMember(end => end.Cidade, opt => opt.MapFrom(p => p.Endereco.Cidade))
                .ForMember(end => end.Bairro, opt => opt.MapFrom(p => p.Endereco.Bairro))
                .ForMember(end => end.Logradouro, opt => opt.MapFrom(p => p.Endereco.Logradouro))
                .ForMember(end => end.Numero, opt => opt.MapFrom(p => p.Endereco.Numero));

            CreateMap<Domain.Entities.Endereco, NewPatientViewModel>();
            CreateMap<Domain.Entities.Teste, DigitalizarTesteViewModel>();
            CreateMap<Domain.Entities.Teste, TestDetailsViewModel>()
                .ForMember(t => t.DataCadastro, opt => opt.MapFrom(test => test.DataCadastro.ToShortDateString()))
                .ForMember(t => t.Resultado, opt => opt.MapFrom(test => test.Laudo.Resultado))
                .ForMember(t => t.ResultadoDetalhes, opt => opt.MapFrom(test => test.Laudo.ResultadoDetalhes));

            CreateMap<Domain.Entities.TesteImagem, TesteImagemViewModel>();
            CreateMap<Domain.Entities.UsuarioClinica, LoginViewModel>()
                .ForMember(t => t.ClinicaId, opt => opt.MapFrom(c => c.ClinicaId))
                .ForMember(t => t.Clinica, opt => opt.MapFrom(c => c.Clinica));
        }
    }
}

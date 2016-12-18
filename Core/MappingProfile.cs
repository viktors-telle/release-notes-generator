using AutoMapper;
using ReleaseNotesGenerator.Domain;

namespace ReleaseNotesGenerator.Core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Repository, Repository>()
                .ForMember(src => src.Id, dest => dest.Ignore());

            CreateMap<Project, Project>()
                .ForMember(src => src.Id, dest => dest.Ignore());
        }    
    }
}

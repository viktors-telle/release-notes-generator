using AutoMapper;
using ReleaseNotes.Generator.Domain;
using ReleaseNotes.Generator.Domain.Commit;
using ReleaseNotes.Generator.Dto;

namespace ReleaseNotes.Generator.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Repository, CommitQuery>()
                .ForMember(src => src.RepositoryName, dest => dest.MapFrom(r => r.Name));

            CreateMap<Branch, CommitQuery>()
                .ForMember(src => src.BranchName, dest => dest.MapFrom(r => r.Name))
                .ForMember(src => src.DateTime, dest => dest.MapFrom(r => r.LastCommitDateTime));

            CreateMap<EmailRequest, ReleaseNotesRequest>();

            CreateMap<Repository, Repository>()
                .ForMember(src => src.Id, dest => dest.Ignore());

            CreateMap<Project, Project>()
                .ForMember(src => src.Id, dest => dest.Ignore());

            CreateMap<Branch, Branch>()
                .ForMember(src => src.Id, dest => dest.Ignore());

            CreateMap<ProjectTrackingTool, ProjectTrackingTool>()
                .ForMember(src => src.Id, dest => dest.Ignore());

            CreateMap<RepositoryItemPath, RepositoryItemPath>()
                .ForMember(src => src.Id, dest => dest.Ignore());
        }
    }
}

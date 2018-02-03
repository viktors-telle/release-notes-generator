using AutoMapper;
using ReleaseNotesGenerator.Features.Email;
using ReleaseNotesGenerator.Features.Projects;
using ReleaseNotesGenerator.Features.ProjectTrackingTools;
using ReleaseNotesGenerator.Features.ReleaseNotes;
using ReleaseNotesGenerator.Features.ReleaseNotes.Commit;
using ReleaseNotesGenerator.Features.SourceCodeRepositories;

namespace ReleaseNotesGenerator.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Repository, CommitQuery>()
                .ForMember(dest => dest.RepositoryName, src => src.MapFrom(r => r.Name))
                .ForMember(dest => dest.BranchName, src => src.Ignore())
                .ForMember(dest => dest.RepositoryPath, src => src.Ignore())
                .ForMember(dest => dest.From, src => src.Ignore())
                .ForMember(dest => dest.Until, src => src.Ignore());

            CreateMap<EmailRequest, ReleaseNotesRequest>()
                .ForMember(dest => dest.Until, src => src.Ignore())
                .ForMember(dest => dest.RepositoryPath, src => src.Ignore());

            CreateMap<Repository, Repository>()
                .ForMember(dest => dest.Id, src => src.Ignore());

            CreateMap<Project, Project>()
                .ForMember(dest => dest.Id, src => src.Ignore());

            CreateMap<ProjectTrackingTool, ProjectTrackingTool>()
                .ForMember(dest => dest.Id, src => src.Ignore());

            CreateMap<ReleaseNotesRequest, ReleaseNote>()
                .ForMember(dest => dest.Notes, src => src.Ignore())
                .ForMember(dest => dest.Created, src => src.Ignore())
                .ForMember(dest => dest.RepositoryId, src => src.Ignore())
                .ForMember(dest => dest.Repository, src => src.Ignore())
                .ForMember(dest => dest.Id, src => src.Ignore());

            CreateMap<ReleaseNotesRequest, CommitQuery>()
                .ForMember(dest => dest.Url, src => src.Ignore())
                .ForMember(dest => dest.Owner, src => src.Ignore());
        }
    }
}

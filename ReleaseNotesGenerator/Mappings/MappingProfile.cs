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
                .ForMember(src => src.RepositoryName, dest => dest.MapFrom(r => r.Name));

            CreateMap<EmailRequest, ReleaseNotesRequest>();

            CreateMap<Repository, Repository>()
                .ForMember(src => src.Id, dest => dest.Ignore());

            CreateMap<Project, Project>()
                .ForMember(src => src.Id, dest => dest.Ignore());

            CreateMap<ProjectTrackingTool, ProjectTrackingTool>()
                .ForMember(src => src.Id, dest => dest.Ignore());

            CreateMap<ReleaseNotesRequest, ReleaseNote>();

            CreateMap<ReleaseNotesRequest, CommitQuery>();
        }
    }
}

using System.Threading.Tasks;
using ReleaseNotesGenerator.Dto;

namespace ReleaseNotesGenerator.Features.Email
{
    public interface IEmailComponent
    {
        Task Send(EmailRequest emailRequest);
    }
}
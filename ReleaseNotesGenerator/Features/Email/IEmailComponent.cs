using System.Threading.Tasks;

namespace ReleaseNotesGenerator.Features.Email
{
    public interface IEmailComponent
    {
        Task Send(EmailRequest emailRequest);
    }
}
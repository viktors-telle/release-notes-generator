using System.Threading.Tasks;
using ReleaseNotesGenerator.Dto;

namespace ReleaseNotesGenerator.Components
{
    public interface IEmailComponent
    {
        Task Send(EmailRequest emailRequest);
    }
}
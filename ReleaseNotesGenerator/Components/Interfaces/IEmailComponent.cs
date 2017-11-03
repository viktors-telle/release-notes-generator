using System.Threading.Tasks;
using ReleaseNotesGenerator.Dto;

namespace ReleaseNotesGenerator.Components.Interfaces
{
    public interface IEmailComponent
    {
        Task Send(EmailRequest emailRequest);
    }
}
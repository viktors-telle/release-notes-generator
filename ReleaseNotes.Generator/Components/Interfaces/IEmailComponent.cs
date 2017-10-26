using System.Threading.Tasks;
using ReleaseNotes.Generator.Dto;

namespace ReleaseNotes.Generator.Components.Interfaces
{
    public interface IEmailComponent
    {
        Task Send(EmailRequest emailRequest);
    }
}
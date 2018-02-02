using System;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using ReleaseNotesGenerator.Dto;
using ReleaseNotesGenerator.Dto.Options;
using ReleaseNotesGenerator.Exceptions;

namespace ReleaseNotesGenerator.Features.Email
{
    public class EmailComponent : IEmailComponent
    {
        private readonly SmtpOptions _smtpOptions;

        public EmailComponent(IOptions<SmtpOptions> smtpOptions)
        {
            _smtpOptions = smtpOptions.Value;
        }

        public async Task Send(EmailRequest emailRequest)
        {
            using (var client = new SmtpClient())
            {
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Host = _smtpOptions.Host;
                client.DeliveryFormat = SmtpDeliveryFormat.International;       
                         
                var message = new MailMessage
                {
                    Subject = emailRequest.Subject,
                    BodyEncoding = Encoding.UTF8,
                    IsBodyHtml = true,
                    Body = emailRequest.Body,
                    From = new MailAddress(emailRequest.From)
                };

                foreach (var to in emailRequest.To)
                {
                    message.To.Add(to);
                }

                if (emailRequest.Cc != null)
                {
                    foreach (var cc in emailRequest.Cc)
                    {
                        message.CC.Add(cc);
                    }
                }
                
                try
                {
                    await client.SendMailAsync(message);
                }
                catch (Exception ex)
                {
                    throw new EmailSendingFailedException(ex.Message, ex.InnerException);
                }
            }
        }
    }
}

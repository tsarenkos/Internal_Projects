using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Threading.Tasks;
using TaskTracker.BL.Interfaces;
using TaskTracker.Models;

namespace TaskTracker.BL.Services
{
    public class MailService : IMailService
    {
        private readonly OptionsForSMTPServer _smtpServer;

        public MailService(IOptions<OptionsForSMTPServer> smtpServer)
        {
            _smtpServer = smtpServer.Value;
        }
        public async Task SendAsync(MailModelBL mailModel)
        {
            try
            {
                MimeMessage message = new MimeMessage();
                message.From.Add(new MailboxAddress(_smtpServer.senderName, (string.IsNullOrWhiteSpace(mailModel.From)) ? _smtpServer.senderMail : mailModel.From));
                message.To.Add(new MailboxAddress(mailModel.To));
                message.Subject = mailModel.Subject;
                var builder = new BodyBuilder() {HtmlBody = $"<div style=\"color: green;\">{mailModel.Body}</div>"};
                if (mailModel.Attachments!=null && mailModel.Attachments.Count > 0)
                {
                    foreach (var attachment in mailModel.Attachments)
                    {
                        builder.Attachments.Add(attachment.FileName, attachment.FileContent);
                    }
                }

                message.Body = builder.ToMessageBody();

                using (MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient())
                {
                    await client.ConnectAsync(_smtpServer.serverAddress, _smtpServer.serverPort, true);
                    await client.AuthenticateAsync(_smtpServer.userName, _smtpServer.password);
                    await client.SendAsync(message);

                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }
    }
}

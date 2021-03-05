using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;
using Core.Api.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using System;
using Nustache.Core;

namespace Core.Api.Configuration
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger<EmailSender> _logger;
        private MailSettings Options { get; }

        public EmailSender(IOptions<MailSettings> optionsAccessor, 
                           ILogger<EmailSender> logger)
        {
            Options = optionsAccessor.Value;
            _logger = logger;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(email, subject, message);
        }

        private async Task Execute(string email, string subject, string htmlMessage)
        {
            try
            {
                using (var emailMessage = new MailMessage(new MailAddress(Options.From, Options.DisplayName), new MailAddress(email)))
                {
                    var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff");

                    emailMessage.IsBodyHtml = true;
                    emailMessage.Body = Render.FileToString(htmlMessage.Contains("Redefina") ? Options.TemplateRedefinir : Options.TemplateRegistro, new { handlerName = email, message = htmlMessage, timestamp = timestamp, name = Options.Name });
                    emailMessage.Subject = subject;

                    using (var smtp = new SmtpClient
                    {
                        EnableSsl = true,
                        Host = Options.Host,
                        Port = Options.Port,
                        Credentials = new NetworkCredential(Options.Username, Options.Password)
                    })
                    {
                        await smtp.SendMailAsync(emailMessage);
                        _logger.LogInformation("Email enviado com sucesso " + email);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro no método de envio de email - SendAsync: " + ex.Message);
                throw new Exception("Erro no método de envio de email - SendAsync: ", ex);
            }
        }
    }
}
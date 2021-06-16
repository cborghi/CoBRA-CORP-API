using CoBRA.Infra.CrossCutting.EmailService.Interfaces;
using CoBRA.Infra.CrossCutting.EmailService.ViewModels;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Net.Mail;
using System;
using System.Net;

namespace CoBRA.Infra.CrossCutting.EmailService.Services
{
    public class EmailService : IEmailService
    {
        private IConfiguration _configuration;

        public EmailService(IConfiguration config)
        {
            _configuration = config;
        }

        public async Task<string> EnviarEmail(EmailViewModel Email)
        {
            try
            {
                CredentialViewModel Credential = new CredentialViewModel()
                {
                    Host = _configuration.GetSection("API-EMAIL").GetSection("Host").Value,
                    Port = Convert.ToInt32(_configuration.GetSection("API-EMAIL").GetSection("Port").Value),
                    UserName = _configuration.GetSection("API-EMAIL").GetSection("UserName").Value,
                    Password = _configuration.GetSection("API-EMAIL").GetSection("Password").Value,
                };

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = Credential.Host;
                    smtp.Port = Credential.Port;
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(Credential.UserName, Credential.Password);

                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress(Credential.UserName);

                        if (Email != null && Email.Destinatario != null)
                        {
                            mail.To.Add(new MailAddress(Email.Destinatario));
                        }

                        mail.Subject = Email.Assunto;
                        mail.Body = Email.CorpoEmail;
                        mail.IsBodyHtml = true;

                        await smtp.SendMailAsync(mail);
                    }

                    return "E-mail enviado com sucesso !";
                }
            }
            catch (Exception ex)
            {
                return "Erro ao enviar e-mail !";
            }
        }
    }
}

using System.Net.Mail;

namespace SOLID_DEMO.Services.After;

public interface IEmailService
{
    void SendEmail(MailMessage mailMessage);
    bool ValidateEmail(string email);
}
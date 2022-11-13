using System.Net.Mail;

namespace SOLID_DEMO.Services.After;

public class EmailService : IEmailService
{
    SmtpClient _smtpClient;
    public EmailService(SmtpClient aSmtpClient)
    {
        _smtpClient = aSmtpClient;
    }
    public bool ValidateEmail(string email)
    {
        return email.Contains("@");
    }
    public void SendEmail(MailMessage message)
    {
        _smtpClient.Send(message);
    }
}
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using Shared;
using SOLID_DEMO.Services.DataAccess;

namespace SOLID_DEMO.Services.Before;

public class UserService
{
    private readonly SmtpClient _smtpClient;
    private readonly UserContext _dbContext;

    public UserService(SmtpClient smtpClient, UserContext dbContext)
    {
        _smtpClient = smtpClient;
        _dbContext = dbContext;
    }

    public void Register(string email, string password)
    {
        if (!ValidateEmail(email))
            throw new ValidationException("Email is not an email");
        var user = new User(email, password);
        _dbContext.Add(user);
        _dbContext.SaveChanges();

        SendEmail(new MailMessage("mysite@nowhere.com", email) { Subject = "HEllo foo" });
    }
    public virtual bool ValidateEmail(string email)
    {
        return email.Contains("@");
    }
    public void SendEmail(MailMessage message)
    {
        _smtpClient.Send(message);
    }
}
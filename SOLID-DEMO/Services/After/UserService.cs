using Shared;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using SOLID_DEMO.Services.DataAccess;

namespace SOLID_DEMO.Services.After;

public class UserService : IUserService
{
    IEmailService _emailService;
    UserContext _dbContext;
    public UserService(IEmailService emailService, UserContext dbContext)
    {
        _emailService = emailService;
        _dbContext = dbContext;
    }
    public void Register(string email, string password)
    {
        if (!_emailService.ValidateEmail(email))
            throw new ValidationException("Email is not an email");
        var user = new User(email, password);
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
        _emailService.SendEmail(new MailMessage("myname@mydomain.com", email) { Subject = "Hi. How are you!" });
    }
}
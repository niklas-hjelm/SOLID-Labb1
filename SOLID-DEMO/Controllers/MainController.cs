using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;
using System.ComponentModel.DataAnnotations;
using Server.DataAccess;

namespace Server.Controllers;

[ApiController]
[Route("api")]
public class MainController : ControllerBase
{
    public ShopContext _userContext;

    public MainController(ShopContext userContext)
    {
        _userContext = userContext;
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetUsers()
    {
        return Ok(_userContext.Users.ToList());
    }

    [HttpGet("users/{email}")]
    public async Task<IActionResult> GetUsers(string email)
    {
        return Ok(_userContext.Users.FirstOrDefault(u=> u.Name.Equals(email)));
    }

    [HttpPost("user/register")]
    public async Task<IActionResult> RegisterUser(string email, string password)
    {
        try
        {
            Register(email, password);
            return Ok();
        }
        catch (ValidationException e)
        {
            throw e;
        }
    }

    [HttpPost("user/login")]
    public IActionResult LoginUser(string email, string password)
    {
        if (Login(email,password))
        {
            return Ok();
        }
        return BadRequest();
    }

    [HttpDelete("user/delete/{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = _userContext.Users.FirstOrDefault(u => u.Id == id);
        if (user == null) return BadRequest();

        _userContext.Users.Remove(user);
        await _userContext.SaveChangesAsync();
        return Ok();
    }

    public bool Login(string email, string password)
    {
        var user = _userContext.Users.FirstOrDefault(u => u.Name.Equals(email) && u.Password.Equals(password));
        return user is not null;
    }

    public void Register(string email, string password)
    {
        if (!ValidateEmail(email))
            throw new ValidationException("Email is not an email");
        var user = new User(email, password);
        _userContext.Add(user);
        _userContext.SaveChanges();
    }

    public virtual bool ValidateEmail(string email)
    {
        return email.Contains("@");
    }
}
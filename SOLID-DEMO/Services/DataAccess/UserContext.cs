using Microsoft.EntityFrameworkCore;
using Shared;

namespace SOLID_DEMO.Services.DataAccess;

public class UserContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public UserContext(DbContextOptions options) : base(options)
    {

    }
}
using AuthenticationWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationWebAPI.Data
{
    public class MyDbContext(DbContextOptions<MyDbContext> o) : DbContext(o)
    {
        public DbSet<Login> logins { get; set; }
        public DbSet<UserRole> roles { get; set; }
    }
}

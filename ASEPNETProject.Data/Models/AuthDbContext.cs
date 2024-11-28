
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ASEPNETProject.Data.Models;
public class AuthDbContext:IdentityDbContext<IdentityUser>
{
    public AuthDbContext(DbContextOptions options) : base(options)
    {

    }
}

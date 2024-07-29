using Dashboard.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.Areas.Identity.Data;

public class UserContext : IdentityDbContext<DashboardUser>
    { 
    public UserContext(DbContextOptions<UserContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }




}


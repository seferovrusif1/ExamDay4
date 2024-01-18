using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Context
{
    public class ExamDay4DBContext : IdentityDbContext<AppUser>
    {
        public ExamDay4DBContext(DbContextOptions options) : base(options){}
        public DbSet<Experts> Experts { get; set; }
        public DbSet<Profession> Professions { get; set; }
        public DbSet<SMIcon> SMIcons { get; set; }
        public DbSet<SMLinks> SMLinks { get; set; }
        public DbSet<ExpertsSMLinks> ExpertsSMLinks { get; set; }


    }
}

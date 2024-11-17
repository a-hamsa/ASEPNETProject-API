using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ASEPNETProject.Data.Models
{
    public class ArticleContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public ArticleContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("default");
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Person> People { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>()
                .HasOne(a => a.Author)
                .WithMany()
                .HasForeignKey(a => a.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

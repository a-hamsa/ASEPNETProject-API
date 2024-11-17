using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ASEPNETProject.Data.Models;
public class ProductContext : DbContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;
    public ProductContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("default");
    }
    public DbSet<Product> Products { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }
}
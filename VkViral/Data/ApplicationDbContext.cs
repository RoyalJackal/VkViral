using Microsoft.EntityFrameworkCore;
using VkViral.Model;

namespace VkViral.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<Token> Tokens { get; set; }
}
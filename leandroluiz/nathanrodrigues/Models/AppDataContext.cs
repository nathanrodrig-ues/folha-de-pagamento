using Microsoft.EntityFrameworkCore;

namespace leandroluiz.Models;

public class AppDataContext : DbContext
{
    public DbSet<Folha> Folhas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Nathan_Leandro.db");
    }
}
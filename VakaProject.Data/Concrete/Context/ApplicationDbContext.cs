using VakaProject.Domain.Concrete;
using Microsoft.EntityFrameworkCore;

namespace VakaProject.Data.Concrete.Context;

public class ApplicationDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost,1470;Initial Catalog=VakaDbContext;User=sa;Password=Kk.54174155431921;Encrypt=True;TrustServerCertificate=True;");
    }

    public DbSet<DataProfile> DataProfiles { get; set; }
    public DbSet<IndividualData> IndividualDatas { get; set; }
}
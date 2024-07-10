using Microsoft.EntityFrameworkCore;
using WebLibrary.DataAccess;

public class MigrationDbContext : ApplicationDbContext
{
    public MigrationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {

    }
}
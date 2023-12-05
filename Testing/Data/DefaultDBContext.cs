using Microsoft.EntityFrameworkCore;
using Testing.Models.Domain;

namespace Testing.Data
{
    public class DefaultDBContext: DbContext
    {
        public DefaultDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        
        }

        public DbSet<DataDb> Data { get; set; }

    }
}

using Microsoft.EntityFrameworkCore;
using Veebirakenduste_loomine_API_MartinKemppi.Models;

namespace Veebirakenduste_loomine_API_MartinKemppi.Data
{
    public class DBContext : DbContext
    {
        public DbSet<Kasutaja> Kasutajad { get; set; }
        public DbSet<Toode> Tooded { get; set; }
        public DbSet<Tellimus> Tellimused { get; set; }

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }        
    }
}

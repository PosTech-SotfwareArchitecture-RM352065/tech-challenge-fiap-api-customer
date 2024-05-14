using Microsoft.EntityFrameworkCore;
using Sanduba.Infrastructure.Persistence.SqlServer.Costumers.Schema;

namespace Sanduba.Infrastructure.Persistence.SqlServer
{
    public class InfrastructureDbContext : DbContext
    {
        public InfrastructureDbContext(DbContextOptions<InfrastructureDbContext> options) : base(options) { }

        internal DbSet<Costumer> Costumers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

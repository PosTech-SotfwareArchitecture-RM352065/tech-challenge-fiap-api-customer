using Microsoft.EntityFrameworkCore;
using Sanduba.Infrastructure.Persistence.SqlServer.Customers.Schemas;

namespace Sanduba.Infrastructure.Persistence.SqlServer
{
    public class InfrastructureDbContext : DbContext
    {
        public InfrastructureDbContext(DbContextOptions<InfrastructureDbContext> options) : base(options) { }

        internal DbSet<Customer> Customers { get; set; }
        internal DbSet<CustomerRequest> CustomerRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

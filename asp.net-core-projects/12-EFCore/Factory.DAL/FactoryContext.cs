using Factory.DAL.Entities;
using Microsoft.EntityFrameworkCore;


namespace Factory.DAL
{
    public class FactoryContext:DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Deliverer> Deliverers { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestStatusType> RequestStatusTypes { get; set; }
        public DbSet<CriticalLevelType> CriticalLevelTypes { get; set; }        
        public DbSet<Breakage> Breakages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=STS-PC1;Database=Factory;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Machine>().Property(m => m.MachineName).IsRequired();
            modelBuilder.Entity<Machine>().Property(m => m.MachineName).HasColumnType("nvarchar(50)");
            modelBuilder.Entity<Machine>().Property(m => m.DateOfDelivery).HasColumnType("date");

            modelBuilder.Entity<CriticalLevelType>().Property(c => c.CriticalLevelTypeId).HasColumnName("CriticalLevelId");
            modelBuilder.Entity<CriticalLevelType>().Property(c => c.CriticalLevelValue).IsRequired();
            modelBuilder.Entity<CriticalLevelType>().Property(c => c.CriticalLevelValue).HasColumnType("nvarchar(50)");

            modelBuilder.Entity<Request>()
                .HasOne(r => r.RequestCreator)
                .WithMany(e => e.RequestsCreated)
                .HasForeignKey(r => r.RequestCreatorId);

            modelBuilder.Entity<Request>()
                .HasOne(r => r.RequestHandler)
                .WithMany(e => e.RequestsHandled)
                .HasForeignKey(r => r.RequestHandlerId);
           
        }
    }
}

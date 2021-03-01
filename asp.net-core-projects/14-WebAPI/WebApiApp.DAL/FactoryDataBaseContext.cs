using Microsoft.EntityFrameworkCore;
using WebApiApp.DAL.Entities;


namespace WebApiApp.DAL
{
    public partial class FactoryDataBaseContext : DbContext
    {        
        public FactoryDataBaseContext()
        {
            
        }
        public FactoryDataBaseContext(DbContextOptions<FactoryDataBaseContext> options)
            : base(options)
        {

        }
        public virtual DbSet<Breakage> Breakages { get; set; }
        public virtual DbSet<BreakagesInRequest> BreakagesInRequests { get; set; }
        public virtual DbSet<CancelledRequest> CancelledRequests { get; set; }
        public virtual DbSet<CorrectingRequest> CorrectingRequests { get; set; }
        public virtual DbSet<CriticalLevelType> CriticalLevelTypes { get; set; }
        public virtual DbSet<Deliverer> Deliverers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Machine> Machines { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<RequestStatusType> RequestStatusTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=STS-PC1;Database=FactoryDataBase;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Breakage>(entity =>
            {
                entity.HasKey(e => e.BreakeageId);

                entity.Property(e => e.BreakeageName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DateOfCreation).HasColumnType("date");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.HasOne(d => d.CriticalLevel)
                    .WithMany(p => p.Breakages)
                    .HasForeignKey(d => d.CriticalLevelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Breakages_CriticalLevelTypes");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Breakages)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Breakages_Employees");

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.Breakages)
                    .HasForeignKey(d => d.MachineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Breakages_Machines");
            });

            modelBuilder.Entity<BreakagesInRequest>(entity =>
            {
                entity.HasKey(e => e.BreakageId);

                entity.Property(e => e.BreakageId).ValueGeneratedNever();

                entity.HasOne(d => d.Breakage)
                    .WithOne(p => p.BreakagesInRequest)
                    .HasForeignKey<BreakagesInRequest>(d => d.BreakageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BreakagesInRequests_Breakages");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.BreakagesInRequests)
                    .HasForeignKey(d => d.RequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BreakagesInRequests_Requests");
            });

            modelBuilder.Entity<CancelledRequest>(entity =>
            {
                entity.HasKey(e => e.RequestId);

                entity.Property(e => e.RequestId).ValueGeneratedNever();

                entity.HasOne(d => d.Fitter)
                    .WithMany(p => p.CancelledRequestFitters)
                    .HasForeignKey(d => d.FitterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CancelledRequests_Employees");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.CancelledRequestManagers)
                    .HasForeignKey(d => d.ManagerId)
                    .HasConstraintName("FK_CancelledRequests_Employees1");

                entity.HasOne(d => d.Request)
                    .WithOne(p => p.CancelledRequest)
                    .HasForeignKey<CancelledRequest>(d => d.RequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CancelledRequests_Requests");
            });

            modelBuilder.Entity<CorrectingRequest>(entity =>
            {
                entity.HasKey(e => new { e.RequestId, e.EmployeeId });

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.CorrectingRequests)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CorrectingRequests_Employees");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.CorrectingRequests)
                    .HasForeignKey(d => d.RequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CorrectingRequests_Requests");
            });

            modelBuilder.Entity<CriticalLevelType>(entity =>
            {
                entity.HasKey(e => e.CriticalLevelId);

                entity.Property(e => e.CriticalLevelValue)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Deliverer>(entity =>
            {
                entity.Property(e => e.DelivererName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Position)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Machine>(entity =>
            {
                entity.Property(e => e.DateOfDelivery).HasColumnType("date");

                entity.Property(e => e.MachineName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Deliverer)
                    .WithMany(p => p.Machines)
                    .HasForeignKey(d => d.DelivererId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Machines_Deliverers");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(e => e.BreakageId);

                entity.Property(e => e.BreakageId).ValueGeneratedNever();

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Breakage)
                    .WithOne(p => p.Notification)
                    .HasForeignKey<Notification>(d => d.BreakageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notifications_Breakages");
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.Property(e => e.DateOfCreate).HasColumnType("date");

                entity.HasOne(d => d.InnerRequest)
                    .WithMany(p => p.InverseInnerRequest)
                    .HasForeignKey(d => d.InnerRequestId)
                    .HasConstraintName("FK_Requests_Requests");

                entity.HasOne(d => d.Machine)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.MachineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Requests_Machines");

                entity.HasOne(d => d.RequestCreator)
                    .WithMany(p => p.RequestRequestCreators)
                    .HasForeignKey(d => d.RequestCreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Requests_Employees1");

                entity.HasOne(d => d.RequestHadler)
                    .WithMany(p => p.RequestRequestHadlers)
                    .HasForeignKey(d => d.RequestHadlerId)
                    .HasConstraintName("FK_Requests_Employees");

                entity.HasOne(d => d.RequestStatus)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.RequestStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Requests_RequestStatusTypes");
            });

            modelBuilder.Entity<RequestStatusType>(entity =>
            {
                entity.HasKey(e => e.RequestStatusCode);

                entity.Property(e => e.RequestStatusValue)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

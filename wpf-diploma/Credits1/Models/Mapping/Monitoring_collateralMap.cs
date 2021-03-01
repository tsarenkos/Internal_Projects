using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Credits1.Models.Mapping
{
    public class Monitoring_collateralMap : EntityTypeConfiguration<Monitoring_collateral>
    {
        public Monitoring_collateralMap()
        {
            // Primary Key
            this.HasKey(t => t.Collateral_agreement);

            // Properties
            this.Property(t => t.Collateral_agreement)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Note)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Monitoring_collateral");
            this.Property(t => t.Collateral_agreement).HasColumnName("Collateral_agreement");
            this.Property(t => t.Previous_date).HasColumnName("Previous_date");
            this.Property(t => t.Planned_date).HasColumnName("Planned_date");
            this.Property(t => t.Note).HasColumnName("Note");

            // Relationships
            this.HasMany(t => t.Employees)
                .WithMany(t => t.Monitoring_collateral)
                .Map(m =>
                    {
                        m.ToTable("Employees_monitoring");
                        m.MapLeftKey("Collateral_agreement");
                        m.MapRightKey("Employee_Id");
                    });

            this.HasRequired(t => t.Collateral)
                .WithOptional(t => t.Monitoring_collateral);

        }
    }
}

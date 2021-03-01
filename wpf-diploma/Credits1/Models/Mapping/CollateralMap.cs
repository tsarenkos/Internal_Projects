using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Credits1.Models.Mapping
{
    public class CollateralMap : EntityTypeConfiguration<Collateral>
    {
        public CollateralMap()
        {
            // Primary Key
            this.HasKey(t => t.Collateral_agreement);

            // Properties
            this.Property(t => t.Collateral_agreement)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.TypeId)
                .IsRequired();

            this.Property(t => t.Description)
                .HasMaxLength(100);

            this.Property(t => t.Credit_agreement)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Id_Person)
                .HasMaxLength(20);

            this.Property(t => t.Id_Firm)
                .HasMaxLength(9);

            this.Property(t => t.FormId);

            // Table & Column Mappings
            this.ToTable("Collateral");
            this.Property(t => t.Collateral_agreement).HasColumnName("Collateral_agreement");
            this.Property(t => t.Start_date).HasColumnName("Start_date");
            this.Property(t => t.End_date).HasColumnName("End_date");
            this.Property(t => t.TypeId).HasColumnName("TypeId");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Currency_Id).HasColumnName("Currency_Id");
            this.Property(t => t.Sum).HasColumnName("Sum");
            this.Property(t => t.Credit_agreement).HasColumnName("Credit_agreement");
            this.Property(t => t.Id_Person).HasColumnName("Id_Person");
            this.Property(t => t.Id_Firm).HasColumnName("Id_Firm");
            this.Property(t => t.Closing_date).HasColumnName("Closing_date");
            this.Property(t => t.FormId).HasColumnName("FormId");

            // Relationships
            this.HasMany(t => t.Properties)
                .WithMany(t => t.Collaterals)
                .Map(m =>
                {
                    m.ToTable("Property_collateral");
                    m.MapLeftKey("Collateral_agreement");
                    m.MapRightKey("Id");
                });
            this.HasRequired(t => t.Credit)
                .WithMany(t => t.Collaterals)
                .HasForeignKey(d => d.Credit_agreement);
            this.HasRequired(t => t.Currencies_directory)
                .WithMany(t => t.Collaterals)
                .HasForeignKey(d => d.Currency_Id);
            this.HasOptional(t => t.Firm)
                .WithMany(t => t.Collaterals)
                .HasForeignKey(d => d.Id_Firm);
            this.HasOptional(t => t.Individual)
                .WithMany(t => t.Collaterals)
                .HasForeignKey(d => d.Id_Person);
            this.HasRequired(t => t.TypeCollateral)
                .WithMany(t => t.Collaterals)
                .HasForeignKey(d => d.TypeId);

        }
    }
}

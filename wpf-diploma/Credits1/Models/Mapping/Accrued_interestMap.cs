using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Credits1.Models.Mapping
{
    public class Accrued_interestMap : EntityTypeConfiguration<Accrued_interest>
    {
        public Accrued_interestMap()
        {
            // Primary Key
            this.HasKey(t => t.Credit_agreement);

            // Properties
            this.Property(t => t.Credit_agreement)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("Accrued_interest");
            this.Property(t => t.Credit_agreement).HasColumnName("Credit_agreement");
            this.Property(t => t.Currency_Id).HasColumnName("Currency_Id");
            this.Property(t => t.Sum).HasColumnName("Sum");

            // Relationships
            this.HasRequired(t => t.Credit)
                .WithOptional(t => t.Accrued_interest);
            this.HasRequired(t => t.Currencies_directory)
                .WithMany(t => t.Accrued_interest)
                .HasForeignKey(d => d.Currency_Id);

        }
    }
}

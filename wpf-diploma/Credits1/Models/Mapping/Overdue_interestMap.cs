using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Credits1.Models.Mapping
{
    public class Overdue_interestMap : EntityTypeConfiguration<Overdue_interest>
    {
        public Overdue_interestMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Credit_agreement, t.Date });

            // Properties
            this.Property(t => t.Credit_agreement)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("Overdue_interest");
            this.Property(t => t.Credit_agreement).HasColumnName("Credit_agreement");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.Currency_Id).HasColumnName("Currency_Id");
            this.Property(t => t.Sum).HasColumnName("Sum");

            // Relationships
            this.HasRequired(t => t.Credit)
                .WithMany(t => t.Overdue_interest)
                .HasForeignKey(d => d.Credit_agreement);
            this.HasRequired(t => t.Currencies_directory)
                .WithMany(t => t.Overdue_interest)
                .HasForeignKey(d => d.Currency_Id);

        }
    }
}

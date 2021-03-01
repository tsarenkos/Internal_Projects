using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Credits1.Models.Mapping
{
    public class Principal_balanceMap : EntityTypeConfiguration<Principal_balance>
    {
        public Principal_balanceMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Credit_agreement, t.Date });

            // Properties
            this.Property(t => t.Credit_agreement)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("Principal_balance");
            this.Property(t => t.Credit_agreement).HasColumnName("Credit_agreement");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.Currency_Id).HasColumnName("Currency_Id");
            this.Property(t => t.Balance).HasColumnName("Balance");

            // Relationships
            this.HasRequired(t => t.Credit)
                .WithMany(t => t.Principal_balance)
                .HasForeignKey(d => d.Credit_agreement);
            this.HasRequired(t => t.Currencies_directory)
                .WithMany(t => t.Principal_balance)
                .HasForeignKey(d => d.Currency_Id);

        }
    }
}

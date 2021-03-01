using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Credits1.Models.Mapping
{
    public class CreditMap : EntityTypeConfiguration<Credit>
    {
        public CreditMap()
        {
            // Primary Key
            this.HasKey(t => t.Credit_agreement);

            // Properties
            this.Property(t => t.Credit_agreement)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Id_Firm)
                .IsRequired()
                .HasMaxLength(9);

            // Table & Column Mappings
            this.ToTable("Credits");
            this.Property(t => t.Credit_agreement).HasColumnName("Credit_agreement");
            this.Property(t => t.Start_date).HasColumnName("Start_date");
            this.Property(t => t.End_date).HasColumnName("End_date");
            this.Property(t => t.Currency_Id).HasColumnName("Currency_Id");
            this.Property(t => t.Sum).HasColumnName("Sum");
            this.Property(t => t.Id_Firm).HasColumnName("Id_Firm");
            this.Property(t => t.Closing_date).HasColumnName("Closing_date");

            // Relationships
            this.HasRequired(t => t.Currencies_directory)
                .WithMany(t => t.Credits)
                .HasForeignKey(d => d.Currency_Id);
            this.HasRequired(t => t.Firm)
                .WithMany(t => t.Credits)
                .HasForeignKey(d => d.Id_Firm);

        }
    }
}

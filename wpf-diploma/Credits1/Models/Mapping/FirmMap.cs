using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Credits1.Models.Mapping
{
    public class FirmMap : EntityTypeConfiguration<Firm>
    {
        public FirmMap()
        {
            // Primary Key
            this.HasKey(t => t.Id_Firm);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.Id_Firm)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Phone)                
                .HasMaxLength(20);

            this.Property(t => t.Legal_address)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Firms");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Id_Firm).HasColumnName("Id_Firm");
            this.Property(t => t.Registration_date).HasColumnName("Registration_date");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.Legal_address).HasColumnName("Legal_address");
        }
    }
}

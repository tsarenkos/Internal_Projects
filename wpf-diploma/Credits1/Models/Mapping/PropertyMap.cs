using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Credits1.Models.Mapping
{
    public class PropertyMap : EntityTypeConfiguration<Property>
    {
        public PropertyMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Collateral_agreement)
                .IsRequired()
                .HasMaxLength(20);            

            this.Property(t => t.Description)
                .HasMaxLength(100);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("Property");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Collateral_agreement).HasColumnName("Collateral_agreement");           
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Currency_Id).HasColumnName("Currency_Id");
            this.Property(t => t.Price).HasColumnName("Price");


            // Relationships            
            this.HasRequired(t => t.Currencies_directory)
                .WithMany(t => t.Properties)
                .HasForeignKey(d => d.Currency_Id);

        }
    }
}

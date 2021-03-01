using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Credits1.Models.Mapping
{
    public class IndividualMap : EntityTypeConfiguration<Individual>
    {
        public IndividualMap()
        {
            // Primary Key
            this.HasKey(t => t.Id_Person);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(40);

            this.Property(t => t.Id_Person)
                .IsRequired()
                .HasMaxLength(20);            

            this.Property(t => t.Address)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Phone)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("Individuals");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Id_Person).HasColumnName("Id_Person");
            this.Property(t => t.Birthday).HasColumnName("Birthday");            
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.Phone).HasColumnName("Phone");
        }
    }
}

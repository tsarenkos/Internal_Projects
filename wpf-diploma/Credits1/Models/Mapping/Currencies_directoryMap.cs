using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Credits1.Models.Mapping
{
    public class Currencies_directoryMap : EntityTypeConfiguration<Currencies_directory>
    {
        public Currencies_directoryMap()
        {
            // Primary Key
            this.HasKey(t => t.Currency_Id);

            // Properties
            this.Property(t => t.Currency_Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("Currencies_directory");
            this.Property(t => t.Currency_Id).HasColumnName("Currency_Id");
            this.Property(t => t.Name).HasColumnName("Name");
        }
    }
}

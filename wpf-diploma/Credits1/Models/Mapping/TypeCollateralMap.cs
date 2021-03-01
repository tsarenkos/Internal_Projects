using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Credits1.Models.Mapping
{
    public class TypeCollateralMap : EntityTypeConfiguration<TypeCollateral>
    {
        public TypeCollateralMap()
        {
            // Primary Key
            this.HasKey(t => t.TypeId);

            // Properties
            this.Property(t => t.TypeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Type)
                .HasMaxLength(60);
            
            // Table & Column Mappings
            this.ToTable("TypeCollateral");
            this.Property(t => t.TypeId).HasColumnName("TypeId");
            this.Property(t => t.Type).HasColumnName("Type");
        }
    }
}

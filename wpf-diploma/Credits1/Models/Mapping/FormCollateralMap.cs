using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Credits1.Models.Mapping
{
    public class FormCollateralMap : EntityTypeConfiguration<FormCollateral>
    {
        public FormCollateralMap()
        {
            // Primary Key
            this.HasKey(t => t.FormId);

            // Properties
            this.Property(t => t.FormId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Form)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("FormCollateral");
            this.Property(t => t.FormId).HasColumnName("FormId");
            this.Property(t => t.Form).HasColumnName("Form");
        }
    }
}

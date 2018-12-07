using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CXY.CJS.Model;
using CXY.CJS.Constant;

namespace CXY.CJS.EntityFrameworkCore
{
    public class WebSiteConfiguration : IEntityTypeConfiguration<WebSite>
    {
        public void Configure(EntityTypeBuilder<WebSite> builder)
        {
            builder.ToTable("WebSites", EFCoreConsts.SchemaNames.CXY);
            
			builder.Property(a => a.WebSiteId).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.WebSiteName).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.ConnectionString).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.CreationTime).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.CreatorUserId).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.LastModifierUserId).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.LastModificationTime).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.DeleterUserId).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.DeletionTime).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.IsDeleted).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);


        }
    }
}



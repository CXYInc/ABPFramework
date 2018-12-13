using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CXY.CJS.Model;
using CXY.CJS.Core.Constant;

namespace CXY.CJS.EntityFrameworkCore
{
    public class TestConfiguration : IEntityTypeConfiguration<Test>
    {
        public void Configure(EntityTypeBuilder<Test> builder)
        {
            builder.ToTable("Tests", EFCoreConsts.SchemaNames.CXY);
            builder.HasKey(c => c.Id);
            builder.Property(a => a.Id).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.Name).HasMaxLength(EFCoreConsts.EntityLengthNames.Length512);
        }
    }
}



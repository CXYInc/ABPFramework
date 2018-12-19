

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CXY.CJS.Model;
using CXY.CJS.Core.Constant;

namespace CXY.CJS.EntityFrameworkCore
{
    public class BatchCarConfiguration : IEntityTypeConfiguration<BatchCar>
    {
        public void Configure(EntityTypeBuilder<BatchCar> builder)
        {
             
            builder.ToTable("BatchCars");

            builder.Property(a => a.Id).HasMaxLength(EFCoreConsts.EntityLengthNames.Length32);
			builder.Property(a => a.WebSiteId).HasMaxLength(EFCoreConsts.EntityLengthNames.Length6);
			builder.Property(a => a.CarNumber).HasMaxLength(EFCoreConsts.EntityLengthNames.Length32);
			builder.Property(a => a.CarCode).HasMaxLength(EFCoreConsts.EntityLengthNames.Length32);
			builder.Property(a => a.EngineNo).HasMaxLength(EFCoreConsts.EntityLengthNames.Length32);
			builder.Property(a => a.PrivateCar).HasMaxLength(EFCoreConsts.EntityLengthNames.Length32);
			builder.Property(a => a.CarType).HasMaxLength(EFCoreConsts.EntityLengthNames.Length32);
			builder.Property(a => a.CarTypeName).HasMaxLength(EFCoreConsts.EntityLengthNames.Length32);
			builder.Property(a => a.IsLock).HasMaxLength(EFCoreConsts.EntityLengthNames.Length32);
			builder.Property(a => a.DriverName).HasMaxLength(EFCoreConsts.EntityLengthNames.Length32);
			builder.Property(a => a.DriverPhone).HasMaxLength(EFCoreConsts.EntityLengthNames.Length32);
			builder.Property(a => a.DriverLicense).HasMaxLength(EFCoreConsts.EntityLengthNames.Length32);
			builder.Property(a => a.IsNeedSearch).HasMaxLength(EFCoreConsts.EntityLengthNames.Length32);
			builder.Property(a => a.HaveLockRule).HasMaxLength(EFCoreConsts.EntityLengthNames.Length32);
			builder.Property(a => a.IsChoose).HasMaxLength(EFCoreConsts.EntityLengthNames.Length32);
			builder.Property(a => a.ViolationMsg).HasMaxLength(EFCoreConsts.EntityLengthNames.Length32);
			builder.Property(a => a.CreationTime).HasMaxLength(EFCoreConsts.EntityLengthNames.Length32);


        }
    }
}



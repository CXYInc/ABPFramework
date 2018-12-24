

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CXY.CJS.Model;
using CXY.CJS.Core.Constant;

namespace CXY.CJS.EntityMapper.Orderss
{
    public class OrdersCfg : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {

            builder.ToTable("Orders");

            
			builder.Property(a => a.WebSiteId).HasMaxLength(EFCoreConsts.EntityLengthNames.Length32);
			builder.Property(a => a.BatchId).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.CurrentBatchId).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.ViolationId).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.PrefixCarNum).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.CarNumber).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.VehicleType).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.VehicleTypeName).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.VINNO).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.EngineNum).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.ViolationTime).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.ViolationCity).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.DocumentNum).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.LocationId).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.ViolationLocale).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.ViolationType).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.OrderType).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.ViolationCode).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.Degree).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.Fine).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.LateFine).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.ServiceCharge).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.TotalFee).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.ProxyFine).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.ProxyServiceCharge).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.ProxyTotalFee).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.MakeFine).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.MakeLateFine).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.MakeServiceCharge).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.Proxy).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.Proxyname).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.ProxyTime).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.PayType).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.UniqueCode).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.PrivateFlag).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.ReviseId).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.PriceSource).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.ViolationReson).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.Difference).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.VAT).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.PriceBasis).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.DriverName).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.DriverPhone).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.DriverNo).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.TailUserId).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.TailUserName).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.PriceFrom).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.OrderSource).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.TaxRate).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.OrderByNo).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.State).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.NeedMakeUpPrice).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.MakeUpTimes).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.NeedMakeUpData).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.NeedMakeDataEnum).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.AlreadyMakeDataEnum).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.UserMemo).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.IsDestory).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.DestoryRemark).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.Apply).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.Applytime).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.PassId).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.PassMemo).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.ExportNum).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.CreatorUserId).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.CreationTime).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.LastModifierUserId).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.LastModificationTime).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.DeleterUserId).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.DeletionTime).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.IsDeleted).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.Remark).HasMaxLength(EFCoreConsts.EntityLengthNames.Length64);


        }
    }
}



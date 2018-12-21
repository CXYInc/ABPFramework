using System;
using CXY.CJS.Model;

namespace CXY.CJS.Tests.TestDatas
{
    public static class BatchCarDatas
    {

        public static readonly BatchCar TestQuotePriceBatchCarDatas = new BatchCar
        {
            Id = "A8888A8888A8888",
            CarNumber = "粤A8888",
            CarCode = "888888",
            EngineNo = "888888",
            PrivateCar = false,
            CarType = "02",
            CarTypeName = "小型汽车",
            IsLock = 0,
            HaveLockRule = false,
            IsChoose = false,
            BatchId = BatchInfoDatas.TestQuotePriceBatchInfo.Id
        };
    }
}
using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public  class CarInfoRecord
    {
        public string Id { get; set; }
        public string FileNo { get; set; }
        public bool? PrivateCar { get; set; }
        public string CarSeries { get; set; }
        public string CarType { get; set; }
        public DateTime? InsuranceEndDate { get; set; }
        public DateTime? InitialRegDate { get; set; }
        public string CarModel { get; set; }
        public string Color { get; set; }
        public string CarOwner { get; set; }
        public string Displacement { get; set; }
        public string IdType { get; set; }
        public string UseCharacter { get; set; }
        public string VehicleType { get; set; }
        public string EngineNo { get; set; }
        public string CarCode { get; set; }
        public string CarNumber { get; set; }
        public int? Seating { get; set; }
        public string Ownership { get; set; }
        public string PhoneNo { get; set; }
        public string Status { get; set; }
        public string CarBrand { get; set; }
        public string OwnerAddress { get; set; }
        public DateTime? AnnualCheckTime { get; set; }
        public string DriverLicense { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public  class DrivingLicenceRecord
    {
        public string Id { get; set; }
        public string WebSiteId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string DrivingLicenceNo { get; set; }
        public string Idcard { get; set; }
        public string VehicleAllowable { get; set; }
        public string Mobile { get; set; }
        public string CarType { get; set; }
        public string CarNumber { get; set; }
        public int? Score { get; set; }
        public string DrivingLicenceStatus { get; set; }
        public bool? DrivingLicenseMobileStatus { get; set; }
        public bool? PrivateCar { get; set; }
        public bool? OwnCar { get; set; }
        public DateTime? ValidityDate { get; set; }
        public string Remark { get; set; }
        public DateTime? CreateDate { get; set; }
        public string Operator { get; set; }
    }
}

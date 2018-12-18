using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public  class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProvinceId { get; set; }
        public string CarPrefix { get; set; }
        public int Rank { get; set; }
        public int Status { get; set; }
        public string Ename { get; set; }
        public string AreaCode { get; set; }
        public string ProvinceName { get; set; }
        public string CityName { get; set; }
        public bool HaveLockRule { get; set; }

        public City IdNavigation { get; set; }
        public City InverseIdNavigation { get; set; }
    }
}

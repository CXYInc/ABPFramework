using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public  class InterFaceRecord
    {
        public string WebSiteId { get; set; }
        public string Selectid { get; set; }
        public string Hphm { get; set; }
        public string Hpzl { get; set; }
        public string Fdjh { get; set; }
        public string Cjh { get; set; }
        public DateTime? Created { get; set; }
        public int? Success { get; set; }
        public int? Errorcode { get; set; }
        public string Errormessage { get; set; }
        public int? Hasdata { get; set; }
        public string Lastsearchtime { get; set; }
        public string Other { get; set; }
        public string Records { get; set; }
        public string Fromuserid { get; set; }
        public int? Iskf { get; set; }
        public int? Isky { get; set; }
        public int? Isreset { get; set; }
        public string Hpzlmc { get; set; }
        public string Provinceid { get; set; }
        public string InterfaceName { get; set; }
        public string Operator { get; set; }
        public bool? PrivateFlag { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public  class BWfblTempExcel
    {
        public string TexcelId { get; set; }
        public string TexcelName { get; set; }
        public string Memo { get; set; }
        public string FromUserId { get; set; }
        public DateTime? Created { get; set; }
        public int? TexcelState { get; set; }
    }
}

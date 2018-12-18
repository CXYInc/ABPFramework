using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public  class FileRecord
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Type { get; set; }
        public long Size { get; set; }
        public DateTime CreateDate { get; set; }
    }
}

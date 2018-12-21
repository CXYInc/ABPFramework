using CXY.CJS.Core.NPOI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace CXY.CJS.Tests.NPOI
{
    public class ImportExcelTest
    {
        [Fact]
        public void ImportTest()
        {
            var path1 = Path.Combine("Source", "test1.xlsx");
            var path2 = Path.Combine("Source", "test2.xls");
            var ds1 = NPOIExcelHelper.ReadExcel(path1);
            var ds2 = NPOIExcelHelper.ReadExcel(path2);

           var tempTable = ds2.Tables["订单信息"];
        }
    }
}

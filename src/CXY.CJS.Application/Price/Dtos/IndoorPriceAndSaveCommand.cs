

using System.Collections.Generic;
using CXY.CJS.Core.Bus.Commands;
using CXY.CJS.Model;

namespace CXY.CJS.Application.Dtos
{
    /// <summary>
    /// 获取报价并入库
    /// </summary>
    public class IndoorPriceAndSaveCommand : Command<List<PriceResultOutput>>
    {
        public string GlobalKey { get; set; }
        public IndoorPriceInput IndoorPrice { get; set; }
    }
}
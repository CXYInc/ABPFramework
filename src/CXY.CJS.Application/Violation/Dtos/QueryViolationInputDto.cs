using CXY.CJS.Core.Enums;

namespace CXY.CJS.Application.Dtos
{
    /// <summary>
    /// 违章查询输入参数
    /// </summary>
    public class QueryViolationInputDto
    {
        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarNumber { get; set; }

        /// <summary>
        /// 车架号
        /// </summary>
        public string CarCode { get; set; }

        /// <summary>
        /// 发动机号
        /// </summary>
        public string CarEngine { get; set; }

        /// <summary>
        /// 车辆类型
        /// </summary>
        public int CarType { get; set; }

        /// <summary>
        /// 车辆类型名称
        /// </summary>
        public string CarTypeName { get; set; }

        /// <summary>
        /// 是否使用历史数据(0否，1是)
        /// </summary>
        public int IsUseHistory { get; set; }

        /// <summary>
        /// 省份代码
        /// </summary>
        public string ProvinceCode { get; set; }

        /// <summary>
        /// 车辆性质
        /// </summary>
        public CarNatureEnum EnumCarNature { get; set; }

        /// <summary>
        /// 是否检测超证(0否，1是)
        /// </summary>
        public int IsCheckLock { get; set; }
    }
}

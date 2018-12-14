namespace CXY.CJS.Application
{
    /// <summary>
    /// 查询车架号输入信息
    /// </summary>
    public class QueryCarCodeInputDto
    {
        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarNumber { get; set; }

        /// <summary>
        /// 车架号长度
        /// </summary>
        public int CarCodeLen { get; set; }

        /// <summary>
        /// 发动机号长度
        /// </summary>
        public int CarEngineLen { get; set; }

        /// <summary>
        /// 车辆类型
        /// </summary>
        public string CarType { get; set; }
    }
}

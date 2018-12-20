namespace CXY.CJS.Application.Dtos
{
    /// <summary>
    /// Excel数据实体
    /// </summary>
    public class BatchTableModelDto
    {
        public string 序号 { get; set; }
        public string 车牌号 { get; set; }
        public string 车型名称 { get; set; }
        public string 车架号 { get; set; }
        public string 发动机号 { get; set; }
        public string 车辆性质 { get; set; }
        public string 是否挑单 { get; set; }
        public string 是否超证 { get; set; }
        public string 违章时间 { get; set; }
        public string 文书号 { get; set; }
        public string 违章城市 { get; set; }
        public string 违章地点 { get; set; }
        public string 违章原因 { get; set; }
        public string 违法代码 { get; set; }
        public string 扣分 { get; set; }
        public string 罚金 { get; set; }
        public string 滞纳金 { get; set; }
        public string 单子来源 { get; set; }
        public string 收单批次 { get; set; }
        public string 收单价格 { get; set; }
        public string 备注 { get; set; }
        public string 手续费 { get; set; }
        public string 车型代码 { get; set; }
        public string 违章城市代码 { get; set; }

        public string 代办方 { get; set; }

        public string 代办成本 { get; set; }

        public string Uniquecode { get; set; }

        /// <summary>
        /// 数据正确性状态
        /// </summary>
        public int DataStatus { get; set; } = 1;

        /// <summary>
        /// 0:无重复，1:当前批次有重复，2：和已有批次数据重复
        /// </summary>
        public int IsRepeat { get; set; }

        public string DataErrorDesc { get; set; }
    }
}

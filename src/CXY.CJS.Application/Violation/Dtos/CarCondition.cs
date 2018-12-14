namespace CXY.CJS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public class ConditionInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public int CarOwnerLen { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CarEngineLen { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CarNumberPrefix { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ProxyEnable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CarCodeLen { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CityID { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ConditionApiResult
    {
        /// <summary>
        /// 
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ConditionInfo Data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string msg { get; set; }
    }
}

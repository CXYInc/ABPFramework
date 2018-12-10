namespace CXY.CJS.WebApi
{
    public class Pagination
    {
        private int _pageIndex = 1;
        private int _pageSize = 15;

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex
        {
            get
            {
                return this._pageIndex;
            }
            set
            {
                this._pageIndex = value;
            }
        }

        /// <summary>
        /// 每页显示行数
        /// </summary>
        public int PageSize
        {
            get
            {
                return _pageSize <= 0 ? 10 : _pageSize;
            }
            set
            {
                _pageSize = value <= 0 ? 10 : value;
            }
        }
    }
}
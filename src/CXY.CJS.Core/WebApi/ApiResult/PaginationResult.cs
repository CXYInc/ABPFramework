using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CXY.CJS.WebApi
{
     public class PaginationResult<TEntity>:Pagination
    {
        private int _totalCount = 0;

        public int TotalCount
        {
            get
            {
                return this._totalCount;
            }
            set
            {
                this._totalCount = value;
            }
        }

        public IEnumerable<TEntity> Datas { get; set; }=new List<TEntity>();
    }
}

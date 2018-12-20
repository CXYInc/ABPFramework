using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace CXY.CJS.Application.Service.Dtos
{
    public class TestFile
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 单个文件
        /// </summary>
        public IFormFile File { get; set; }

        /// <summary>
        /// 数组文件
        /// </summary>
        public IFormFile[] Files { get; set; }

        /// <summary>
        /// 列表文件
        /// </summary>
        public List<IFormFile> LFiles { get; set; }
    }
}

using System;
using Abp.AutoMapper;

namespace CXY.CJS.Role.Dto
{
    [AutoMapTo(typeof(Model.Role))]
    public class ListRoleOutput
    {
        public string Id { get; set; }
        public string WebSiteId { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
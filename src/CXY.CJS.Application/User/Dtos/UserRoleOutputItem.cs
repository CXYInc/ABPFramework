using Abp.AutoMapper;

namespace CXY.CJS.Application.Dtos
{
    [AutoMapTo(typeof(Model.Users))]
    public class UserRoleOutputItem
    {
        public string RoleId { get; set; }
        public string UserId { get; set; }
    }
}
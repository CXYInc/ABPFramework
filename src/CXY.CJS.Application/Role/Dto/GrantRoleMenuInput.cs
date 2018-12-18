using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CXY.CJS.Application.Dtos
{
    public class GrantRoleMenuInput
    {
        [Required]
        public string RoleId { get; set; }

        [Required]
        public string MenuId { get; set; }

        [Required]
        public bool IsGrant { get; set; }
        //[Required]
        //public List<ListMenu> ListMenu { get; set; }

    }
    //public class ListMenu
    //{
    //    public string MenuId { get; set; }
    //    public bool IsGrant { get; set; }
    //}
}

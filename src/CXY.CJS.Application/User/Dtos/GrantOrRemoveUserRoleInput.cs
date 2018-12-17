using System;
using System.ComponentModel.DataAnnotations;
using Abp.Runtime.Validation;

namespace CXY.CJS.Application.Dtos
{
    public class GrantOrRemoveUserRoleInput
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string RoleId { get; set; }

        [Required]
        public bool IsGrant { get; set; }
    }
}
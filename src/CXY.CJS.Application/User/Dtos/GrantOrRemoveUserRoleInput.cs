using System;
using System.ComponentModel.DataAnnotations;
using Abp.Runtime.Validation;

namespace CXY.CJS.Application.Dtos
{
    public class GrantOrRemoveUserRoleInput:ICustomValidate
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string RoleId { get; set; }

        [Required]
        public bool IsGrant { get; set; }


        public void AddValidationErrors(CustomValidationContext context)
        {
            if (string.IsNullOrWhiteSpace(UserId))
            {
                context.Results.Add(new ValidationResult("UserId 不能为空"));
            }
            if (string.IsNullOrWhiteSpace(RoleId))
            {
                context.Results.Add(new ValidationResult("RoleId 不能为空"));
            }
        }
    }
}
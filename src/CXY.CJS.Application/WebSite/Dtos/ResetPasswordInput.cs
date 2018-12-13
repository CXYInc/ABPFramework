using System.ComponentModel.DataAnnotations;
using Abp.Runtime.Validation;

namespace CXY.CJS.Application.Dtos
{
    public class ResetPasswordInput : ICustomValidate
    {
        [Required]
        public string UserId { get; set; }
        public void AddValidationErrors(CustomValidationContext context)
        {
            if (string.IsNullOrEmpty(UserId))
            {
                context.Results.Add(new ValidationResult("缺少参数"));
            }
        }
    }
}
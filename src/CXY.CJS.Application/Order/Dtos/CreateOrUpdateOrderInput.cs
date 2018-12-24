using System.ComponentModel.DataAnnotations;

namespace CXY.CJS.Application.Dtos
{
    public class CreateOrUpdateOrderInput
    {
        [Required]
        public OrderEditDto Order { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CXY.CJS.Application.Dtos
{
    public class PriceInput
    {
        [Required]
        public string BatchId { get; set; } 
    }
}

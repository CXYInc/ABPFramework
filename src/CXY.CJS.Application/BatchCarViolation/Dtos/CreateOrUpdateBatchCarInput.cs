

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CXY.CJS.Model;

namespace CXY.CJS.Application.Dtos
{
    public class CreateOrUpdateBatchCarInput
    {
        [Required]
        public BatchCarEditDto BatchCar { get; set; }

    }
}
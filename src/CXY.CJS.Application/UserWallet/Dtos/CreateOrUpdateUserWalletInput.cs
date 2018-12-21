using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CXY.CJS.Application.Dtos
{
    public class CreateOrUpdateUserWalletInput
    {
        [Required]
        public UserWalletEditDto UserWallet { get; set; }
    }
}

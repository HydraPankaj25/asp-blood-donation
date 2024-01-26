using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.DTO.Admin
{
    public class AdminLoginDetails
    {
        [Required]
        public string? adminName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? adminPassword { get; set; }
    }
}

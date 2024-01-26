using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.DTO.Admin
{
    public class AdminDetails
    {
        [Required]
        public string? adminName {  get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string? adminEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? adminPassword { get; set; }
    }
}

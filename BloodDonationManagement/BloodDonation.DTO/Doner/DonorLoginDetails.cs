using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.DTO.Doner
{
    public class DonorLoginDetails
    {
        [Required(ErrorMessage = "Email Must Be Required")]
        [DataType(DataType.EmailAddress)]
        public string? DonorEmail { get; set; }




        [Required(ErrorMessage = "Password Must Be Required")]
        [DataType(DataType.Password)]
        public string? DonorPassword { get; set; }
    }
}

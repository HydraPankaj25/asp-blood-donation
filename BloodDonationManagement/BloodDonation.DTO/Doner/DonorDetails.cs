using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.DTO.Donor
{
    public class DonorDetails
    {
        public int Id { get; set; }

        public string DonorId { get; set; }

        [Required]
        public string DonorName { get; set; }


        [Required]
        public string DonorAddress { get; set; }


        [Required]
        public string DonorCity { get; set; }


        [Required]
        [DataType(DataType.EmailAddress)]
        public string DonorEmail { get; set; }


        [Required]
        public string DonorMobile { get; set; }


        [Required]
        [DataType(DataType.Date)]
        public string DonorDob { get; set; }


        [Required]
        public string DonorGender { get; set; }


        [Required]
        public string DonorBloodGrp { get; set; }


        [Required]
        public bool DonorAggreement { get; set; } 


        [Required]
        public bool DonorAppointmentStatus { get; set; } = false;


        public bool DeleteFlag { get; set; } = false;

        public string Test1 { get; set; }
        public string Test2 { get; set; }
        public string Test3 { get; set; }
        public string Test4 { get; set; }

        public IList<DonorDonationDetails> DonorDonationDetails { get; set; }

	}
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.DTO.Donor
{
    public class DonorRegistrationDetails
    {
        public int Id { get; set; }

        public string DonorCustomeId { get; set; } = " ";


        [Required]
        public string DonorName { get; set; }


        [Required]
        public string DonorAddress { get; set; }

        [Required]
        public string DonorCity { get; set; }


        [Required]
        [DataType(DataType.EmailAddress)]
        [Remote("CheckEmail","Home",ErrorMessage ="Email Already Exist! ",HttpMethod ="Get")]
        public string DonorEmail { get; set; }


        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{6,15}$", ErrorMessage = "Password must be between 6 and 15 characters long. Password must contain at least one number. Password must contain at least one uppercase letter. Password must contain at least one special Character.")]
        public string DonorPassword { get; set; }
        
        
        [Required]
        [Compare("DonorPassword", ErrorMessage ="Password Must Be Same")]
        public string DonorCnfPassword { get; set; }


        [Required]
        [StringLength(10,ErrorMessage ="Maximum Length Can Be 10")]
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
    }
}

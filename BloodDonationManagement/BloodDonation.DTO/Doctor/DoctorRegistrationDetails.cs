using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonation.DTO.Doctor
{
    public class DoctorRegistrationDetails
    {
        public int Id { get; set; } = 0;


        public string? HId { get; set; } = " ";


        [Required]
        public string? DoctorName { get; set; }


        [Required]
        [DataType(DataType.EmailAddress)]
        [Remote("checkEmail","Admin",ErrorMessage ="Doctor Email ALready Exist",HttpMethod ="Get")]
        public string? DoctorEmail { get; set; }


        [Required]
        public string? DoctorAddress { get; set; }

        
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{6,15}$",ErrorMessage = "String must be between 6 and 15 characters long. String must contain at least one number. String must contain at least one uppercase letter. String must contain at least one special Character.")]
        public string? DoctorPassword { get; set; }


        [Required]
        public string? DoctorSpeciality { get; set; }


        [Required]
        public string? DoctorTiming { get; set; }



        [Required]
        public string? DoctorFloor { get; set; }



        [Required]
        public string? DoctorDegree { get; set; }


        [Required]
        [DataType(DataType.Date)]
        public string? DoctorDOJ { get; set; }


    }
}

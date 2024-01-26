using BloodDonation.DAL.Entities.Donor;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.DAL.Entities.Doctor
{
    public class DoctorEntity
    {
        public int Id { get; set; }



        [Column("HospitalId")]
        public string? HId { get; set; }


        public string? DoctorName { get; set; }


        [DataType(DataType.EmailAddress)]
        public string? DoctorEmail { get; set; }    


        public string? DoctorAddress { get; set; }


        public string? DoctorSpeciality { get; set; }


        public string? DoctorTiming { get; set; }



        public string? DoctorFloor { get; set; }



        public string? DoctorDegree { get; set; }



        public bool DoctorStatus { get; set; } = true;


        [DataType(DataType.Date)]
        public string? DoctorDOJ { get; set; }



        public bool DeleteFlag { get; set; } = false;


        public List<DonorDonationDetailsEntity>? DonorDonationDetails { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.DAL.Entities.Donor
{
    public class DonorDetailsEntity
    {
        public int Id { get; set; }

        public string? DonorCustomeId { get; set; }

        public string? DonorName { get; set; }

        public string? DonorAddress { get; set; }
        public string? DonorCity { get; set; }

        public string? DonorEmail { get; set; }

        public string? DonorMobile { get; set; }

        public string? DonorDob { get; set; }

        public string? DonorGender { get; set; }

        public string? DonorBloodGrp { get; set; }

        public bool DonorAggreement { get; set; } = false;

        public bool DonorAppointmentStatus { get; set; } = false;

        public bool DeleteFlag { get; set; } = false;

        public string? Test1 { get; set; } 
        public string? Test2 { get; set; }
        public string? Test3 { get; set; }
        public string? Test4 { get; set; }

        public List<DonorDonationDetailsEntity>? DonorDonationDetails { get; set; }
	}
}

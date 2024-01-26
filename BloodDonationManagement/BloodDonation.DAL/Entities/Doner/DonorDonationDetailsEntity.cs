using BloodDonation.DAL.Entities.Doctor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.DAL.Entities.Donor
{
	public class DonorDonationDetailsEntity
	{
		public int Id { get; set; }

		public string? BloodGrp {  get; set; }

		public int DonorId { get; set; } = 0;

		public int DoctorId { get; set; } = 0;

		public string? DonationDate { get; set; }

		public string? BloodExpirationDate { get; set; }

		public int BloodPoint {  get; set; } = 0;

		public int BloodPointUsed { get; set; } = 0;

		public DonorDetailsEntity DonorDetails { get; set; }
		public DoctorEntity DoctorDetails { get; set; }
	}
}

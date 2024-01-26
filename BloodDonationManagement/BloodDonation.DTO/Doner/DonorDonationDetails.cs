using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.DTO.Donor
{
	public class DonorDonationDetails
	{
		public int Id { get; set; }

		public string BloodGrp {  get; set; }

		public string DonorId { get; set; }

        public int DoctorId { get; set; }

        public string DonationDate { get; set; }

		public string BloodExpirationDate { get; set; }

		public int BloodPoint {  get; set; }

		public int BloodPointUsed { get; set; }

		public DonorDetails DonorDetails { get; set; }
	}
}

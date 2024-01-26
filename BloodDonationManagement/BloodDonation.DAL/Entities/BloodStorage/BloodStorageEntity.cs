using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.DAL.Entities.BloodStorage
{
    public class BloodStorageEntity
    {
        public int Id { get; set; }


        public string? BloodGroup { get; set; }


        public int TotalBloodPoint { get; set; } = 0;


        public int TotalDonatedBloodPoint { get; set; } = 0;
    }
}

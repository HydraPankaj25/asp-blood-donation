using BloodDonation.DAL.AppContext;
using BloodDonation.DAL.Entities.Doctor;
using BloodDonation.DAL.Entities.Donor;
using BloodDonation.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.DAL.Implementation
{
    public class Doctor : Repository<DoctorEntity>, IDoctor
    {
        private readonly ApplicationDbContext context;

        public Doctor(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<bool> UpdateAsync(DoctorEntity entity)
        {
            context.DoctorDetails.Update(entity);

            var res = await context.SaveChangesAsync();

            if (res > 0)
            {
                return true;
            }
            return false;
        }


        public async Task<DoctorEntity> GetDataUsingAnotherId(string id)
        {
            var data = context.DoctorDetails.FirstOrDefault(a => a.HId == id);

            if (data != null)
                return data;

            return new DoctorEntity();
        }



        public async Task<DoctorEntity> GetDataByEmail(string email)
        {
            var data = context.DoctorDetails.FirstOrDefault(x=>x.DoctorEmail == email);

            if (data != null) return data;

            return new DoctorEntity();
        }
    }
}

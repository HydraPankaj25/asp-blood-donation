using BloodDonation.DAL.AppContext;
using BloodDonation.DAL.Entities.Donor;
using BloodDonation.DAL.Entities.States;
using BloodDonation.DAL.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.DAL.Implementation
{
    public class Donor : Repository<DonorDetailsEntity>, IDonor
    {
        private readonly ApplicationDbContext context;
        public Donor(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }


        public async Task<bool> UpdateAsync(DonorDetailsEntity entity)
        {
            context.DonorDetails.Update(entity);

            var res = await context.SaveChangesAsync();

            if (res > 0)
            {
                return true;
            }
            return false;
        }



        public async Task<DonorDetailsEntity> GetDataUsingAnotherId(string id)
        {
            var data = context.DonorDetails.FirstOrDefault(a=>a.DonorCustomeId == id);

            if(data != null)
                return data;

            return new DonorDetailsEntity();
        }


        public async Task<DonorDetailsEntity> GetDataByEmail(string email)
        {
            var data = context.DonorDetails.FirstOrDefault(a=>a.DonorEmail == email);

            if(data != null) return data;

            return new DonorDetailsEntity();
        }


        public async Task<List<State>> GetStates()
        {
            var states = context.states.ToList();
            return states;
        }
    }
}

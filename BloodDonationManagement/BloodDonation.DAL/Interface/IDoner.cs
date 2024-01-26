using BloodDonation.DAL.Entities.Doctor;
using BloodDonation.DAL.Entities.Donor;
using BloodDonation.DAL.Entities.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.DAL.Interface
{
    public interface IDonor:IRepository<DonorDetailsEntity>
    {
        Task<bool> UpdateAsync(DonorDetailsEntity entity);

        Task<DonorDetailsEntity> GetDataUsingAnotherId(string id);

        Task<DonorDetailsEntity> GetDataByEmail(string email);

        Task<List<State>> GetStates();
    }
}

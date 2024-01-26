using BloodDonation.DAL.Entities.Doctor;
using BloodDonation.DAL.Entities.Donor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.DAL.Interface
{
    public interface IDoctor: IRepository<DoctorEntity>
    {
        Task<bool> UpdateAsync(DoctorEntity entity);

        Task<DoctorEntity> GetDataUsingAnotherId(string id);

        Task<DoctorEntity> GetDataByEmail(string email);
    }
}

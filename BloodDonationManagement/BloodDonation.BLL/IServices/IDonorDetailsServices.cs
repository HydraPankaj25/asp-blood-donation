using BloodDonation.DAL.Entities.States;
using BloodDonation.DAL.Interface;
using BloodDonation.DTO.Doctor;
using BloodDonation.DTO.Doner;
using BloodDonation.DTO.Donor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.BLL.IServices
{
    public interface IDonorDetailsServices
    {
        bool RegisterDonor(DonorRegistrationDetails entity);

        bool DeleteDonor(DonorDetails entity);
        
        IEnumerable<DonorDetails> GetAllDonor();

        bool UpdateDonor(DonorRegistrationDetails entity);

        DonorRegistrationDetails GetDonorById(int id);

        Task<(int, string)> DonorLogin(DonorLoginDetails entity);

        DonorRegistrationDetails GetDonorByEmail(string email);

        List<State> GetAllStates();

        bool checkDonorExistence(string email);

        DonorDetails getDonorDataByEmail(string email);
    }
}

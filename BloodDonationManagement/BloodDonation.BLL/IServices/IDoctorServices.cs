using BloodDonation.DTO.Doctor;
using BloodDonation.DTO.Donor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.BLL.IServices
{
    public interface IDoctorServices
    {
        bool RegisterDoctor(DoctorRegistrationDetails entity);

        bool DeleteDoctor(DoctorRegistrationDetails entity);

        IEnumerable<DoctorDetails> GetAllDoctor();

        bool UpdateDoctor(DoctorRegistrationDetails entity);

        DoctorRegistrationDetails GetDoctorById(int id);

        Task<(int, string)> DoctorLogin(DoctorLoginDetails entity);

        DoctorRegistrationDetails GetDoctorByEmail(string email);

        bool checkDoctorExistence(string email);
    }
}

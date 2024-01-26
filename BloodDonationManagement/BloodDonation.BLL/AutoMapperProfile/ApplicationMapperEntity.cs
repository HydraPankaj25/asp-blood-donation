using AutoMapper;
using BloodDonation.DAL.Entities.Doctor;
using BloodDonation.DAL.Entities.Donor;
using BloodDonation.DTO.Doctor;
using BloodDonation.DTO.Donor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.BLL.AutoMapperProfile
{
    public class ApplicationMapperEntity:Profile
    {
        public ApplicationMapperEntity()
        {
            CreateMap<DonorDetailsEntity, DonorDetails>().ReverseMap();
            CreateMap<DonorDetailsEntity, DonorRegistrationDetails>().ReverseMap();
            CreateMap<DonorDonationDetailsEntity, DonorDonationDetails>().ReverseMap();
            CreateMap<DoctorEntity,DoctorRegistrationDetails>().ReverseMap();
            CreateMap<DoctorEntity, DoctorDetails>().ReverseMap();

        }
    }
}

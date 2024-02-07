using AutoMapper;
using BloodDonation.BLL.IServices;
using BloodDonation.DAL.Entities.Doctor;
using BloodDonation.DAL.Entities.Donor;
using BloodDonation.DAL.Entities.States;
using BloodDonation.DAL.Implementation;
using BloodDonation.DAL.Interface;
using BloodDonation.DTO.Doner;
using BloodDonation.DTO.Donor;
using BloodDonation.WebApi.Controllers.Token_Generation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.BLL.Services
{
    public class DonorDetailsServices : IDonorDetailsServices
    {
        private readonly IDonor donor;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration config;

        public DonorDetailsServices(IDonor donor, IMapper mapper, UserManager<IdentityUser> userManager, IConfiguration config)
        {
            this.donor = donor;
            this.mapper = mapper;
            this.userManager = userManager;
            this.config = config;
        }


        public bool RegisterDonor(DonorRegistrationDetails entity)
        {
            try
            {
                if (entity != null)
                {
                    var data = new IdentityUser()
                    {
                        UserName = entity.DonorEmail,
                        Email = entity.DonorEmail
                    };

                    //cheking already existense
                    var check = userManager.FindByEmailAsync(data.Email).Result;
                    if (check != null)
                    {
                        throw new Exception("User Email Already Exist");
                    }

                    var res = userManager.CreateAsync(data, entity.DonorPassword).Result;

                    if (res.Succeeded)
                    {
                        entity.DonorCustomeId = data.Id;
                        var res1 = donor.CreateAsync(mapper.Map<DonorDetailsEntity>(entity)).Result;
                        var res2 = userManager.AddToRoleAsync(data, "Donor").Result;

                        if (res1)
                            return true;

                        throw new Exception("Sorry No Data Added");
                    }
                }
                throw new NullReferenceException();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteDonor(DonorDetails entity)
        {
            try
            {
                var data = new IdentityUser()
                {
                    UserName = entity.DonorEmail,
                    Email = entity.DonorEmail
                };

                var res = userManager.DeleteAsync(data).Result;

                if (res.Succeeded)
                {
                    var res1 = donor.DeleteAsync(mapper.Map<DonorDetailsEntity>(entity)).Result;

                    if (res1)
                        return true;

                    throw new Exception("Doner Cannot Deleted !");
                }

                throw new Exception("Doner Cannot Deleted !");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<DonorDetails> GetAllDonor()
        {
            try
            {
                var data = donor.GetAllAsync().Result;

                if (data != null)
                {
                    var convertedData = mapper.Map<IEnumerable<DonorDetails>>(data);
                    return convertedData;
                }
                throw new Exception("Cannot Get Data !");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DonorRegistrationDetails GetDonorById(int id)
        {
            try
            {
                if (id != 0)
                {
                    var res = donor.GetByIdAsync(id).Result;

                    if (res != null)
                    {
                        return mapper.Map<DonorRegistrationDetails>(res);
                    }

                    throw new Exception("Cannot Find Any Data");
                }

                throw new Exception("No Id Found");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateDonor(DonorRegistrationDetails entity)
        {
            try
            {
                var data = new IdentityUser()
                {
                    UserName = entity.DonorEmail,
                    Email = entity.DonorEmail
                };

                var identityUserResult = userManager.UpdateAsync(data).Result;

                if (identityUserResult.Succeeded)
                {
                    var res = donor.UpdateAsync(mapper.Map<DonorDetailsEntity>(entity)).Result;

                    if (res)
                    {
                        return true;
                    }
                    throw new Exception("Data Cannot Be Updated !");
                }
                throw new Exception("Data Cannot Be Updated !");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public async Task<(int, string)> DonorLogin(DonorLoginDetails entity)
        {
            var user = userManager.FindByNameAsync(entity.DonorEmail).Result;
            if (user == null)
                return (0, "Invalid Username");

            var res = userManager.CheckPasswordAsync(user, entity.DonorPassword).Result;
            if (!res)
                return (0, "Invalid Password");


            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, entity.DonorEmail),
                    new Claim(ClaimTypes.Role, "Donor"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

            var token = tokenGeneration.GenerateJSONWebToken(config, authClaims);

            return (1, token);
        }





        public DonorRegistrationDetails GetDonorByEmail(string email)
        {
            var res = donor.GetDataByEmail(email);

            return mapper.Map<DonorRegistrationDetails>(res);
        }



        //getting the states

        public List<State> GetAllStates()
        {
            var res = donor.GetStates().Result;
            return res;
        }




        //checking donor existence
        public bool checkDonorExistence(string email)
        {
            var res = userManager.FindByEmailAsync(email).Result;

            if (res != null)
                return true;

            return false;   
        }


        //GETTING THE DONOR DATA BY EMAIL
        public DonorDetails getDonorDataByEmail(string email)
        {
            var res = donor.GetDataByEmail(email).Result;

            if (res != null)
                return mapper.Map<DonorDetails>(res);

            return new DonorDetails();
        }
    }
}

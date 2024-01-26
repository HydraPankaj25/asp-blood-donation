using AutoMapper;
using BloodDonation.BLL.IServices;
using BloodDonation.DAL.Entities.Doctor;
using BloodDonation.DAL.Interface;
using BloodDonation.DTO.Doctor;
using BloodDonation.WebApi.Controllers.Token_Generation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.BLL.Services
{
    public class DoctorServices : IDoctorServices
    {
        private readonly IDoctor _doctor;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IConfiguration config;

        public DoctorServices(IDoctor doctor, IMapper mapper, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration config)
        {
            _doctor = doctor;
            _mapper = mapper;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.config = config;
        }



        public bool RegisterDoctor(DoctorRegistrationDetails entity)
        {
            try
            {
                if (entity != null)
                {
                    var data = new IdentityUser()
                    {
                        UserName = entity.DoctorEmail,
                        Email = entity.DoctorEmail
                    };

                    //cheking already existense
                    var check = userManager.FindByEmailAsync(data.Email).Result;
                    if (check != null)
                    {
                        throw new Exception("Doctor Email Already Exist");
                    }


                    var res = userManager.CreateAsync(data, entity.DoctorPassword).Result;

                    if (res.Succeeded)
                    {
                        entity.HId = data.Id;
                        var res1 = _doctor.CreateAsync(_mapper.Map<DoctorEntity>(entity)).Result;
                        if (res1)
                        {
                            var res2 = userManager.AddToRoleAsync(data, "Doctor").Result;

                            if (res2.Succeeded)
                                return true;
                        }

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


        public bool DeleteDoctor(DoctorRegistrationDetails entity)
        {
            try
            {
                var data = new IdentityUser()
                {
                    UserName = entity.DoctorEmail,
                    Email = entity.DoctorEmail
                };

                var res = userManager.DeleteAsync(data).Result;

                if (res.Succeeded)
                {
                    var res1 = _doctor.DeleteAsync(_mapper.Map<DoctorEntity>(entity)).Result;

                    if (res1)
                        return true;

                    throw new Exception("Doctor Cannot Deleted !");
                }

                throw new Exception("Doctor Cannot Deleted !");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<DoctorDetails> GetAllDoctor()
        {
            try
            {
                var data = _doctor.GetAllAsync().Result;

                if (data != null)
                {
                    var convertedData = _mapper.Map<IEnumerable<DoctorDetails>>(data);
                    return convertedData;
                }
                throw new Exception("Cannot Get Data !");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DoctorRegistrationDetails GetDoctorById(int id)
        {
            try
            {
                if (id != 0)
                {
                    var res = _doctor.GetByIdAsync(id).Result;

                    if (res != null)
                    {
                        return _mapper.Map<DoctorRegistrationDetails>(res);
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


        public bool UpdateDoctor(DoctorRegistrationDetails entity)
        {
            try
            {
                var data = new IdentityUser()
                {
                    UserName = entity.DoctorEmail,
                    Email = entity.DoctorEmail
                };

                var identityUserResult = userManager.UpdateAsync(data).Result;

                if (identityUserResult.Succeeded)
                {
                    var res = _doctor.UpdateAsync(_mapper.Map<DoctorEntity>(entity)).Result;

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



        public async Task<(int, string)> DoctorLogin(DoctorLoginDetails entity)
        {
            var user = userManager.FindByNameAsync(entity.DoctorEmail).Result;
            if (user == null)
                return (0, "Invalid Username");

            var res = userManager.CheckPasswordAsync(user, entity.DoctorPassword).Result;
            if (!res)
                return (0, "Invalid Password");


            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, entity.DoctorEmail),
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

            var token = tokenGeneration.GenerateJSONWebToken(config, authClaims);

            return (1, token);
        }



        public DoctorRegistrationDetails GetDoctorByEmail(string email)
        {
            var res = _doctor.GetDataByEmail(email);

            return _mapper.Map<DoctorRegistrationDetails>(res);
        }



        //checking the email existence
        public bool checkDoctorExistence(string email)
        {
            var res = userManager.FindByEmailAsync(email).Result;
            if (res != null)
                return true;

            return false;
        }
    }
}

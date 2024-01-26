using BloodDonation.BLL.IServices;
using BloodDonation.DAL.Implementation;
using BloodDonation.DTO.Doctor;
using BloodDonation.DTO.Doner;
using BloodDonation.DTO.Donor;
using BloodDonation.WebApi.Controllers.Token_Generation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonation.WebApi.Controllers
{
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorServices services;
        private readonly IConfiguration config;

        public DoctorController(IDoctorServices services, IConfiguration config)
        {
            this.services = services;
            this.config = config;
        }


        //ADDING NEW DONOR 

        [Route("Api/RegisterNewDoctor")]
        [HttpPost]
        [Authorize(Roles ="Admin")]
        public IActionResult CreateDoctor(DoctorRegistrationDetails obj)
        {
            try
            {
                var res = services.RegisterDoctor(obj);

                if (res)
                    return Ok("Data Added Successfully !");

                return BadRequest("Sorry No Data Added");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }





        //GETTING ALL DOCTOR DATA

        [Route("Api/GetAllDoctor")]
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IActionResult GetAllData()
        {
            try
            {
                var res = services.GetAllDoctor();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        //GETTING THE DOCTOR DATA BY THEIR ID

        [Route("Api/GetDoctorDataById")]
        [HttpGet]
        [Authorize(Roles = "Doctor")]
        public IActionResult GetDoctorById(int id)
        {
            try
            {
                var res = services.GetDoctorById(id);

                if (res != null)
                {
                    return Ok(res);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        //UPDATE DOCTOR DATA 

        [Route("Api/UpdateDoctorData")]
        [HttpPut]
        [Authorize(Roles = "Doctor")]
        public IActionResult UpdateDoctor(DoctorRegistrationDetails obj)
        {
            try
            {
                var res = services.UpdateDoctor(obj);
                if (res)
                    return Ok("Data Updated Successfully");

                return BadRequest("Sorry No Data Updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }



        //LOGIN API FOR DOCTOR
        [Route("Api/DoctorLogin")]
        [HttpPost]
        public IActionResult LogginDocter(DoctorLoginDetails obj)
        {
            try
            {
                var (status,message) = services.DoctorLogin(obj).Result;

                if(status == 1)
                {
                    var doctor = services.GetDoctorByEmail(obj.DoctorEmail);

                    return Ok(new {doctorDetail=doctor,token=message});
                }
                return BadRequest(new { doctorDetail = new DoctorDetails(), token = "Cannot Login" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { doctorDetail = new DoctorDetails(), token = ex.Message });
            }
        }






        //checking doctor existece
        [HttpGet]
        [Route("Api/DoctorExistence")]

        public bool existence(string email)
        {
            var res = services.checkDoctorExistence(email);
            return res;
        }
    }
}

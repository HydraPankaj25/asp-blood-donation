using BloodDonation.BLL.IServices;
using BloodDonation.DTO.Doctor;
using BloodDonation.DTO.Doner;
using BloodDonation.DTO.Donor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BloodDonation.WebApi.Controllers
{
    [ApiController]
    public class DonorController : ControllerBase
    {
        private readonly IDonorDetailsServices _services;
        private readonly IConfiguration config;

        public DonorController(IDonorDetailsServices services,IConfiguration config)
        {
            _services = services;
            this.config = config;
        }





        //ADDING NEW DONOR 

        [Route("Api/RegisterNewDonor")]
        [HttpPost]
        public IActionResult CreateDonor([FromBody] DonorRegistrationDetails obj)
        {
            try
            {
                var res = _services.RegisterDonor(obj);

                if (res)
                    return Ok("Data Added Successfully !");

                return BadRequest("Sorry No Data Added");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        //GETTING ALL DONOR DATA

        [Route("Api/GetAllDonor")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllData()
        {
            try
            {
                var res = _services.GetAllDonor();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        //GETTING THE DONOR DATA BY THEIR ID

        [Route("Api/GetDonorDataById")]
        [HttpGet]
        [Authorize(Roles ="User")]
        public IActionResult GetDonorById(int id)
        {
            try
            {
                var res = _services.GetDonorById(id);

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




        //UPDATE DONOR DATA 

        [Route("Api/UpdateDonorData")]
        [HttpPut]
        [Authorize(Roles ="User")]
        public IActionResult UpdateDonor(DonorRegistrationDetails obj)
        {
            try
            {
                var res = _services.UpdateDonor(obj);
                if(res)
                    return Ok("Data Updated Successfully");

                return BadRequest("Sorry No Data Updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }




        //LOGIN API FOR DONOR
        [Route("Api/DonorLogin")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult LogginDoner(DonorLoginDetails obj)
        {
            try { 
             
                var (status, message) = _services.DonorLogin(obj).Result;

                if (status == 1)
                {
                    var donor = _services.GetDonorByEmail(obj.DonorEmail);

                    return Ok(new { donorDetail = donor, token = message });
                }
                return BadRequest(new { donorDetail = new DonorDetails(), token = "Cannot Login" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { donorDetail = new DonorDetails(), token = ex.Message });
            }
        }




        //checking donor existece
        [HttpGet]
        [Route("Api/DonorExistence")]

        public bool existence(string email)
        {
            var res = _services.checkDonorExistence(email);
            return res;
        }





        //GETTING THE STATES    
        [HttpGet]
        [Route("Api/GetStates")]
        [AllowAnonymous]
        public IActionResult AllState()
        {
            try
            {
                return Ok(_services.GetAllStates().Select(a => a.state).ToList());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

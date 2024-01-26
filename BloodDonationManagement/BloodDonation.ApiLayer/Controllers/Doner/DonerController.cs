using BloodDonation.BLL.IServices;
using BloodDonation.DTO.Doner;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonation.ApiLayer.Controllers.Doner
{
    [ApiController]
    public class DonerController : ControllerBase
    {

        private readonly IDonerDetailsServices _services;

        public DonerController(IDonerDetailsServices services)
        {
            _services = services;
        }


        [Route("Api/RegisterNewDoner")]
        [HttpPost]
        public IActionResult CreateDoner(DonerRegistrationDetails obj)
        {
            var res = _services.RegisterDoner(obj);

            if (res)
                return Ok("Data Added Successfully !");

            return BadRequest();
        }
    }
}

using AspNetCoreHero.ToastNotification.Abstractions;
using BloodDonation.DTO;
using BloodDonation.DTO.Donor;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace BloodDonationManagement.Controllers
{
    public class HomeController : Controller
    {

        HttpClient client = new HttpClient();
        private readonly INotyfService notyf;

        public HomeController(INotyfService notyf)
        {
            this.notyf = notyf;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Organisation()
        {
            return View();
        }

        public IActionResult GroupMember()
        {
            return View();
        }



        //Rendering the registration page
        [HttpGet]
        public IActionResult Register()
        {
            var stateRes = client.GetAsync("http://localhost:5062/Api/GetStates").Result;
            if(stateRes.IsSuccessStatusCode)
            {
                var StateList = stateRes.Content.ReadAsStringAsync().Result;
                var stingList = JsonConvert.DeserializeObject<List<string>>(StateList);

                var stateConvertedList = new List<SelectListItem>();
                foreach(var item in stingList)
                {
                    stateConvertedList.Add(new SelectListItem() { Text= item ,Value = item});
                }
                TempData["states"] = stateConvertedList;
                return View();
            }
            return View();
        }


        //Getting the data 
        [HttpPost]
        public async Task<IActionResult> Register(DonorRegistrationDetails obj)
        {
            if (ModelState.IsValid)
            {
                var stringData = JsonConvert.SerializeObject(obj);
                StringContent sc = new StringContent(stringData, Encoding.UTF8, "application/json");

                var res = await client.PostAsync("http://localhost:5062/Api/RegisterNewDonor", sc);
                var message = res.Content.ReadAsStringAsync().Result;

                if (res.IsSuccessStatusCode)
                {
                    notyf.Success(message, 5);

                    return RedirectToAction("Login");
                }

                notyf.Error(message, 5);
                return RedirectToAction("Register",obj);
            }
            return RedirectToAction("Register",obj);
        }



        //checking user email existence

        [HttpGet]
        public IActionResult CheckEmail(string DonorEmail)
        {
            var res = client.GetAsync($"http://localhost:5062/Api/DonorExistence?email={DonorEmail}").Result;
            var resResult = res.Content.ReadAsStringAsync().Result;

            if(resResult == "false")
                return Json(true);

            return Json(false);
        }








        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

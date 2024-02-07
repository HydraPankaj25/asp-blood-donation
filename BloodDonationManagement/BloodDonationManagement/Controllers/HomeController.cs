using AspNetCoreHero.ToastNotification.Abstractions;
using BloodDonation.DTO;
using BloodDonation.DTO.Doner;
using BloodDonation.DTO.Donor;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
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

        public IActionResult Login()
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
        [ValidateAntiForgeryToken]
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







        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(DonorLoginDetails obj)
        {
            if (ModelState.IsValid)
            {
                SetCookie("UserName", obj.DonorEmail, null);
                var jsonData = JsonConvert.SerializeObject(obj);

                StringContent sc = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var res = client.PostAsync("http://localhost:5062/Api/DonorLogin", sc).Result;

                var message = res.Content.ReadAsStringAsync().Result;

                if (res.IsSuccessStatusCode)
                {
                    SetCookie("UserToken", message, null);
                    notyf.Success("Donor Logged In Successfully", 5);
                    return RedirectToAction("Dashboard");
                }

                notyf.Error(message, 5);
                return View();
            }

            return View(obj);
        }



        public IActionResult Dashboard()
        {
            if (Request.Cookies["UserToken"] != null)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["UserToken"]);

                var res = client.GetAsync($"http://localhost:5062/Api/GetDonorDataByEmail?email={Request.Cookies["UserName"]}").Result;

                var message = res.Content.ReadAsStringAsync().Result;

                if(res.IsSuccessStatusCode)
                {
                    var data = JsonConvert.DeserializeObject<DonorDetails>(message);
                    return View(data);
                }
                else if(res.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    RedirectToAction("Logout");
                    notyf.Error("Sorry You Are Unauthorized !", 5);
                    return RedirectToAction("Login");
                }
            }
            return RedirectToAction("Login");
        }





        //default error 
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        //logout the donor
        public IActionResult Logout()
        {
            Response.Cookies.Delete("UserToken");
            Response.Cookies.Delete("UserName");

            return RedirectToAction("Index");
        }



        //setting the cookie time function
        private void SetCookie(string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();

            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddDays(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMinutes(15);

            Response.Cookies.Append(key, value, option);
        }
    }
}

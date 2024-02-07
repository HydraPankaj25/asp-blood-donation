using AspNetCoreHero.ToastNotification.Abstractions;
using BloodDonation.DTO.Admin;
using BloodDonation.DTO.Doctor;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
namespace BloodDonationManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly INotyfService notyf;
        HttpClient client = new HttpClient();
        private const string SessionName = "AdminName";


        //adding the DI to use the toster 
        public AdminController(INotyfService notyf)
        {
            this.notyf = notyf;
        }


        //ViewBag.Name = HttpContext.Session.GetString(SessionName);
        [HttpGet]
        public IActionResult Index()
        {
            if (Request.Cookies["AdminToken"] == null)
                return View();

            return RedirectToAction("DoctorRegistration");
        }


        [HttpGet]
        public IActionResult DoctorRegistration()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DoctorRegistration(DoctorRegistrationDetails obj)
        {
            if (ModelState.IsValid)
            {
                var jsonData = JsonConvert.SerializeObject(obj);

                StringContent sc = new StringContent(jsonData, Encoding.UTF8, "application/json");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["AdminToken"]);

                var res = client.PatchAsync("http://localhost:5062/Api/RegisterNewDoctor", sc).Result;

                var message = res.Content.ReadAsStringAsync().Result;

                if (res.IsSuccessStatusCode)
                {
                    notyf.Success("Doctor Register Successfully", 5);
                    return RedirectToAction("Index");
                }
                notyf.Error(message, 5);
                return View();
            }
            else
            {
                return View(obj);
            }
        }





        //checking the email existence of the doctor
        [HttpGet]
        public IActionResult checkEmail(string DoctorEmail)
        {
            var res = client.GetAsync($"http://localhost:5062/Api/DoctorExistence?email={DoctorEmail}").Result;
            var resResult = res.Content.ReadAsStringAsync().Result;

            if (resResult == "false")
                return Json(true);

            return Json(false);
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AdminLogin(AdminLoginDetails obj)
        {
            if (ModelState.IsValid)
            {
                SetCookie("AdminName", obj.adminName, null);
                var jsonData = JsonConvert.SerializeObject(obj);

                StringContent sc = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var res = client.PostAsync("http://localhost:5062/Api/AdminLogin", sc).Result;

                var message = res.Content.ReadAsStringAsync().Result;

                if (res.IsSuccessStatusCode)
                {
                    SetCookie("AdminToken", message, null);
                    notyf.Success("Admin Logged In Successfully", 5);
                    return RedirectToAction("DoctorRegistration");
                }

                notyf.Error(message, 5);
                return RedirectToAction("Index");
            }

            return View(obj);
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



        //logout the admin
        public IActionResult Logout()
        {
            Response.Cookies.Delete("AdminToken");
            Response.Cookies.Delete("AdminName");

            return RedirectToAction("Index");
        }
    }
}

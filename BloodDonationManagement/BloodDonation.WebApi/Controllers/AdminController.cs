using BloodDonation.DTO.Admin;
using BloodDonation.WebApi.Controllers.Token_Generation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BloodDonation.WebApi.Controllers
{
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IConfiguration config;
        private readonly RoleManager<IdentityRole> roleManager;

        public AdminController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration config, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.config = config;   
            this.roleManager = roleManager;
        }




        //ADDING THE ADMIN PROFILE

        [Route("Api/AdminRegistration")]
        [HttpPost]
        public IActionResult addAdmin(AdminDetails obj)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser()
                {
                    UserName = obj.adminName,
                    Email = obj.adminEmail
                };

                var res = userManager.CreateAsync(user, obj.adminPassword).Result;
                var res1 = userManager.AddToRoleAsync(user, "Admin").Result;
                if (res1.Succeeded)
                    return Ok("Admin Added Successfully !");

                foreach (var item in res.Errors)
                {
                    ModelState.AddModelError("ValidationErr", item.Description);
                }

                return BadRequest(ModelState);
            }
            return BadRequest("Details Not Found!");
        }



        //LOGGING THE ADMIN

        [HttpPost]
        [Route("Api/AdminLogin")]
        public IActionResult AdminLogin(AdminLoginDetails obj)
        {
            var user = userManager.FindByNameAsync(obj.adminName).Result;
            if (user == null)
                return BadRequest("Invalid Username");

            var res = userManager.CheckPasswordAsync(user, obj.adminPassword).Result;
            if (!res)
                return BadRequest("Invalid password");


            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, obj.adminName),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = tokenGeneration.GenerateJSONWebToken(config, authClaims);

            return Ok(token);
        }





        //ADDING THE ROLES TO THE TABLE

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("Api/AddingRoles")]
        public IActionResult addingRole([FromBody]string roleName)
        {
            if (roleName != null)
            {
                if (!roleManager.RoleExistsAsync(roleName).Result)
                {
                    var res = roleManager.CreateAsync(new IdentityRole() { Name = roleName }).Result;

                    if (res.Succeeded)
                    {
                        return Ok("Role has been added !");
                    }
                }
                return BadRequest("Role Already Found");
            }
            return BadRequest("No Role Found");
        }
    }
}

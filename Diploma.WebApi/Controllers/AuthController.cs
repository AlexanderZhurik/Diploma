using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Diploma.WebApi.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Diploma.WebApi.Controllers
{
    
    [ApiController]
    public class AuthController : ControllerBase
    { 
        [HttpPost]
        [Route("Auth")]
        
        public async void Login(string login, string password)
        {
            
            var loginViewModel = new LoginViewModel() { UserName = login, Password = password };
            if (!ModelState.IsValid)
            {
                return;
            }
            var User = AuthenticateUser(loginViewModel);
            if (User == null)
            {
                return;
            }
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, loginViewModel.UserName),
                new(ClaimTypes.Role, "Administrator")
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            //Response.Redirect("localhost:7046/ViewAsAdmin.html");
        }
        private UserInfo AuthenticateUser(LoginViewModel user)
        {
            var users = new StaticUsers();
            if (user.UserName == users.Admin.UserName && user.Password == users.Admin.Password) return users.Admin;
            else return null;
        }
        [HttpGet]
        [Route("RoleCheck")]
        public bool IsInRole()
        {
            return User.IsInRole("Administrator");
        }
        [HttpDelete]
        [Route("Logout")]
        public async void Logout()
        {
            await HttpContext.SignOutAsync();
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using RecipePlus.ViewModels; // пространство имен моделей RegisterModel и LoginModel
using RecipePlus.Models; // пространство имен UserContext и класса User
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Linq;
using System;
using RecipePlus.ViewModels;

namespace RecipePlus.Controllers
{
    public class AccountController : Controller
    {
        private RecipeContext db;
        public AccountController(RecipeContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            int userId = 0;
            if (ModelState.IsValid)
            {
                User user = db.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
                
                if (user != null)
                {
                    await Authenticate(model.Email);
                    
                    return RedirectToAction("Person", "Home", new { user.UserId});
                }
                ModelState.AddModelError("", "Login or password isn't correct");
            }

            
            ViewBag.User = model.Email;
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = db.Users.FirstOrDefault(u => u.Email == model.Email);
               
                if (user == null)
                {
                    db.Users.Add(new User { Email = model.Email, Password = model.Password, FirstName = model.FirstName, LastName = model.LastName });
                    await db.SaveChangesAsync();

                    await Authenticate(model.Email);

                    User needUser = db.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

                    return RedirectToAction("Person", "Home", new { needUser.UserId });
                }
            }
            else
            {
                ModelState.AddModelError("", "Login or password isn't correct");
            }

            return View(model);
        }

        private async Task Authenticate(string email)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, email)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));

        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Start", "Home");
        }
    }
}
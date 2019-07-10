using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using loginRegC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace loginRegC.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
     
        // here we can "inject" our context service into the constructorcopy
        public HomeController(MyContext context)
        {
            dbContext = context;
        }


        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("create")]
        public IActionResult Create(User newUser)
        {
            Console.WriteLine(newUser.FName);
            Console.WriteLine(newUser.Email);
            if(ModelState.IsValid)
            {
                // If a User exists with provided email
                if(dbContext.Users.Any(user => user.Email == newUser.Email))
                {
                    ModelState.AddModelError("NewUser.Email", "Email already in use!");
                    return View("Index");
                }
                else
                {
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
                    dbContext.Add(newUser);
                    dbContext.SaveChanges();
                    return RedirectToAction("Show");
                }
            }
            return View("Index");
        }

        [HttpGet("show")]
        public IActionResult Show()
        {
            return View("Show");
        }

        [HttpPost("login")]
        public IActionResult Login(LogUser LoggedUser)
        {
            Console.WriteLine(LoggedUser);
            Console.WriteLine(LoggedUser.Email);
            Console.WriteLine(LoggedUser.Password);

            if(ModelState.IsValid)
            {
                var confirmUser = dbContext.Users.FirstOrDefault(user => user.Email == LoggedUser.Email);
                Console.WriteLine(confirmUser.Email);
                if(confirmUser == null)
                {
                    ModelState.AddModelError("Email", "Invalid Email/Password");
                    return View("Index");
                }
                
                var hasher = new PasswordHasher<LogUser>();
                
                var result = hasher.VerifyHashedPassword(LoggedUser, confirmUser.Password, LoggedUser.Password);
                
                if(result == 0)
                {
                    ModelState.AddModelError("Email", "Invalid Email/Password");
                    return View("Index");
                }
                else
                {
                    return View("Show");
                }
            }
            return View("Index");

        }

    }

}

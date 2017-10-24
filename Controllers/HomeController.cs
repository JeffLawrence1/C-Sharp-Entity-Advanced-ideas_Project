using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ideas_project.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace ideas_project.Controllers
{
    public class HomeController : Controller
    {
        private YourContext _context;
 
    

            public HomeController(YourContext context)
                
            {
                    
            _context = context;
                
            }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

               [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterViewModel model){
            if(ModelState.IsValid){
                User email = _context.Users.Where(x => x.Email == model.Email).SingleOrDefault();
                if(email == null){

                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                User user = new User();

                user.Password = Hasher.HashPassword(user, model.Password);
                user.Name = model.Name;
                user.Alias = model.Alias;
                user.Email = model.Email;
                user.createdat = DateTime.Now;
                user.updatedat = DateTime.Now;
                
                _context.Users.Add(user);
                _context.SaveChanges();
                int UserID = _context.Users.Last().UserID;
                TempData["Success"] = "Congratulations please login to continue!";
                return View("Index");
                }
            
            else{
                TempData["Error"] = "Email already taken hacker!!!";
                return View("Index");
            }
        }
        return View("Index");
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(string Email, string Password){

            User user = _context.Users.Where(x => x.Email == Email).SingleOrDefault();
            if(user != null && Password != null){
                var Hasher = new PasswordHasher<User>();
                if(0 != Hasher.VerifyHashedPassword(user, user.Password, Password)){
                    HttpContext.Session.SetInt32("UserID", user.UserID);
                    HttpContext.Session.SetString("Name", user.Name);
                    return RedirectToAction("Dash", "Idea");
                }
            }
           
            TempData["Error"] = "Wrong email or password!!!! Stop trying to hack other people's accounts!!!";
            return RedirectToAction("Index");
            
        }

        [HttpGet]
        [Route("Logout")]
        public IActionResult Logout(){
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}


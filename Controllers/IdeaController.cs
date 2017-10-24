using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ideas_project.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ideas_project.Controllers
{
    public class IdeaController : Controller
    {
        private YourContext _context;
 
    

            public IdeaController(YourContext context)
                
            {
                    
            _context = context;
                
            }
        // GET: /Home/
        [HttpGet]
        [Route("Dash")]
        public IActionResult Dash()
        {
            if(HttpContext.Session.GetInt32("UserID") == null){
                return RedirectToAction("Index", "Home");
            }
            List<Idea> idea = _context.Ideas.Include(x => x.Like).OrderByDescending(x => x.TotalLikes).ToList();
            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.ID = (int)HttpContext.Session.GetInt32("UserID");
            ViewBag.Idea = idea;
            return View("Dash");
        }
        [HttpGet]
        [Route("User1/{UserID}")]
        public IActionResult User1(int UserID){
            if(HttpContext.Session.GetInt32("UserID") == null){
                return RedirectToAction("Index", "Home");
            }
            User user = _context.Users.Where(x => x.UserID == UserID).SingleOrDefault();
            ViewBag.User = user;
            return View("User1");
        }
        [HttpGet]
        [Route("Status/{IdeaID}")]
        public IActionResult Status(int IdeaID){
            if(HttpContext.Session.GetInt32("UserID") == null){
                return RedirectToAction("Index", "Home");
            }
            Idea idea = _context.Ideas.Where(x => x.IdeaID == IdeaID).SingleOrDefault();
            List<Like> likers = _context.Likes.Where(x => x.IdeaID == IdeaID).Include(x => x.User).ToList();
            // List<Like> likers2 = likers.Distinct().ToList();
            // var likers2 = likers.Select(x => x.UserID).Distinct().ToList();

            ViewBag.Idea = idea;
            ViewBag.Likes = likers;
            return View("Status");
        }
        [HttpPost]
        [Route("AddIdea")]
        public IActionResult AddIdea(string Idea){
            if(ModelState.IsValid){
                int ID = (int)HttpContext.Session.GetInt32("UserID");
                User poster = _context.Users.Where(x => x.UserID == ID).SingleOrDefault();
                Idea newI = new Idea();
                newI.UserID = (int)HttpContext.Session.GetInt32("UserID");
                newI.Description = Idea;
                newI.TotalLikes = 0;
                newI.createdat = DateTime.Now;
                newI.updatedat = DateTime.Now;
                newI.CreatorName = HttpContext.Session.GetString("Name");
                poster.TotalPosts += 1;
                _context.Ideas.Add(newI);
                _context.SaveChanges();
                int IdeaId = _context.Ideas.Last().IdeaID;
                return RedirectToAction("Dash");
            }
            else{
                TempData["Error"] = "You must add an idea to post and idea geezzz!!!!";
                return RedirectToAction("Dash");
            }
        }

        [HttpGet]
        [Route("Like/{IdeaID}")]
        public IActionResult Like(int IdeaID){
            int ID = (int)HttpContext.Session.GetInt32("UserID");
            Idea IdeaLike = _context.Ideas.Where(x => x.IdeaID == IdeaID).SingleOrDefault();
            User Liker = _context.Users.Where(x => x.UserID == ID).SingleOrDefault();
            Like newL = new Like();
            newL.UserID = ID;
            newL.IdeaID = IdeaID;
            newL.createdat = DateTime.Now;
            newL.updatedat = DateTime.Now;
            IdeaLike.TotalLikes += 1;
            Liker.TotalLikes += 1;
            _context.Likes.Add(newL);
            _context.SaveChanges();
            return RedirectToAction("Dash");
        }
        [HttpPost]
        [Route("Delete/{IdeaID}")]
        public IActionResult Delete(int IdeaID){
            List<Like> badLikes = _context.Likes.Where(x => x.IdeaID == IdeaID).ToList();
            foreach(var x in badLikes){
                _context.Likes.Remove(x);
                _context.SaveChanges();
            }
            Idea badIdea = _context.Ideas.Where(x => x.IdeaID == IdeaID).SingleOrDefault();
            _context.Ideas.Remove(badIdea);
            _context.SaveChanges();
            return RedirectToAction("Dash");
        }
    }
}

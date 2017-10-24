using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ideas_project.Models
{
    public class User
    {
        public int UserID { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        public int TotalPosts { get; set; }
        public int TotalLikes { get; set; }

        public List<Like> Like { get; set; }
        public DateTime createdat { get; set; }
        public DateTime updatedat { get; set; }


        public User(){

            Like = new List<Like>();
            createdat = DateTime.Now;
            updatedat = DateTime.Now;
        }
    }
    
}
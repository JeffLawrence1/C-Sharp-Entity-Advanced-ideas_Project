using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ideas_project.Models
{
    public class Idea
    {
        public int UserID { get; set; }
        public int IdeaID { get; set; }

        [Required]
        public string Description { get; set; }
        public int TotalLikes { get; set; }

        public string CreatorName { get; set; }


        public List<Like> Like { get; set; }

        public DateTime createdat { get; set; }
        public DateTime updatedat { get; set; }


        public Idea(){

            Like = new List<Like>();
            createdat = DateTime.Now;
            updatedat = DateTime.Now;
        }
    }
}
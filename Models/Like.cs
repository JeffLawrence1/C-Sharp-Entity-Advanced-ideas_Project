using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ideas_project.Models
{
    public class Like
    {
        public int LikeID { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public int IdeaID { get; set; }
        public Idea Idea { get; set; }

        public DateTime createdat { get; set; }
        public DateTime updatedat { get; set; }

        public Like(){

            createdat = DateTime.Now;
            updatedat = DateTime.Now;
        }
    }
}
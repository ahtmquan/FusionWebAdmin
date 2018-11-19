using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fusion.Face.WebAdmin.Models
{
    public class UserInfo
    {
        public Guid ID { get; set; }

        [Required(ErrorMessage = "The User name could not be null")]
        public string Username { get; set; }

        [Required(ErrorMessage = "The Full name could not be null")]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "The Email could not be null")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Status could not be null")]
        public string Status { get; set; }

        [Required(ErrorMessage = "The Password could not be null")]
        public string Password { get; set; }

        [Required(ErrorMessage = "The Password could not be null")]
        public string ConfirmPassword { get; set; }
    }
}
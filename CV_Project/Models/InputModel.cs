using CV_Project.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CV_Project.Models
{
    public class InputModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "Maximum length is {1}")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Maximum length is {1}")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name = "Nationality")]
        public string Nationality { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required]
        public List<bool> Skills { get; set; } = new List<bool>();

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Confirm Email")]
        public string ConfirmEmail { get; set; }

        [Required]
        [Display(Name = "Photo")]
        public IFormFile Photo { get; set; }

        public CV ToCv()
        {
            var skills = "";

            if (Skills[0]) skills += "Python ";

            if (Skills[1]) skills += "C# ";

            if (Skills[2]) skills += "PHP ";

            if (Skills[3]) skills += "Java ";

            return new CV
            {
                FirstName = FirstName,
                LastName = LastName,
                DateOfBirth = DateOfBirth.ToString("yyyy-MM-dd"),
                Nationality = Nationality,
                Gender = Gender,
                Skills = skills,
                Email = Email,
                Photo = Photo.FileName,
                IsDeleted = false,
            };
        }
    }
}

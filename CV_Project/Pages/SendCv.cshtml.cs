using Cv_Project;
using CV_Project.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;

namespace CV_Project.Pages
{
    public class SendCvModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        [Obsolete]
        private readonly IHostingEnvironment _environment;

        static Random rnd = new Random();

        [BindProperty]
        public int X { get; set; } = rnd.Next(1, 20);
        [BindProperty]
        public int Y { get; set; } = rnd.Next(20, 50);

        [BindProperty, Required]
        public int SUM { get; set; }

        private readonly Service _service;

        [Obsolete]
        public SendCvModel(Service service, IHostingEnvironment environment)
        {
            _service = service;
            _environment = environment;
        }

        public void OnGet()
        {
            Input = new InputModel();
            Input.Skills.Add(false);
            Input.Skills.Add(false);
            Input.Skills.Add(false);
            Input.Skills.Add(false);
        }

        [Obsolete]
        public async Task<IActionResult> OnPost()
        {
            try
            {
                if (X + Y != SUM)
                {
                    ModelState.AddModelError(
                    string.Empty,
                    "Check the sum of the two numbers"
                    );
                }
                if (Input.Email != Input.ConfirmEmail)
                {
                    ModelState.AddModelError(
                    string.Empty,
                    "Email and confirmation email are not the same"
                    );
                }
                if (ModelState.IsValid)
                {
                    var file = Path.Combine(_environment.ContentRootPath, "wwwroot/Uploads", Input.Photo.FileName);
                    using (var fileStream = new FileStream(file, FileMode.Create))
                    {
                        await Input.Photo.CopyToAsync(fileStream);
                    }
                    var myId = await _service.AddCvAsync(Input.ToCv());
                    return RedirectToPage("Summary", new {id = myId });
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "An error occurred while trying to save the resume to the database"
                    );
            }
            return Page();
        }

    }
}

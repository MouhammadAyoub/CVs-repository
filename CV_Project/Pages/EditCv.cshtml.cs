using Cv_Project;
using CV_Project.Data;
using CV_Project.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CV_Project.Pages
{
    public class EditCvModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int id { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        [Obsolete]
        private readonly IHostingEnvironment _environment;

        private readonly Service _service;

        public CV myCv { get; set; }

        [Obsolete]
        public EditCvModel(Service service, IHostingEnvironment environment)
        {
            _service = service;
            _environment = environment;
        }

        [Obsolete]
        public async Task OnGet()
        {
            myCv = await _service.GetCv(id);

            Input = new InputModel();

            Input.FirstName = myCv.FirstName;

            Input.LastName = myCv.LastName;

            Input.DateOfBirth = DateTime.Parse(myCv.DateOfBirth);

            Input.Nationality = myCv.Nationality;

            Input.Gender = myCv.Gender;

            if (myCv.Skills.Contains("Python"))
            {
                Input.Skills.Add(true);
            }
            else
            {
                Input.Skills.Add(false);
            }

            if (myCv.Skills.Contains("C#"))
            {
                Input.Skills.Add(true);
            }
            else
            {
                Input.Skills.Add(false);
            }

            if (myCv.Skills.Contains("PHP"))
            {
                Input.Skills.Add(true);
            }
            else
            {
                Input.Skills.Add(false);
            }

            if (myCv.Skills.Contains("Java"))
            {
                Input.Skills.Add(true);
            }
            else
            {
                Input.Skills.Add(false);
            }

            Input.Email = myCv.Email;

            Input.ConfirmEmail = myCv.Email;

        }

        [Obsolete]
        public async Task<IActionResult> OnPost()
        {
            try
            {
                if (Input.Email != Input.ConfirmEmail)
                {
                    ModelState.AddModelError(
                    string.Empty,
                    "the Email and Conformation email are not the same"
                    );
                }
                if (ModelState.IsValid)
                {
                    var file = Path.Combine(_environment.ContentRootPath, "wwwroot/Uploads", Input.Photo.FileName);
                    using (var fileStream = new FileStream(file, FileMode.Create))
                    {
                        await Input.Photo.CopyToAsync(fileStream);
                    }
                    var cv = Input.ToCv();
                    cv.CvId = id;
                    await _service.EditCv(cv);
                    return RedirectToPage("/BrowseCv");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "An error occured saving the Edited CV try a different id"
                    );
            }
            return Page();
        }

    }
}

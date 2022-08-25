using Cv_Project;
using CV_Project.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace CV_Project.Pages
{
    public class SummaryModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int id { get; set; }

        [Obsolete]
        private readonly IHostingEnvironment _environment;

        private readonly Service _service;

        public string myPicturePath;

        public CV myCv { get; set; }

        public int myGrade { get; set; } = 0;

        [Obsolete]
        public SummaryModel(Service service, IHostingEnvironment environment)
        {
            _service = service;
            _environment = environment;
        }

        public async Task OnGetAsync()
        {
            myCv = await _service.GetCv(id);

            myPicturePath = "Uploads/" + myCv.Photo;

            if (myCv != null)
            {
                myGrade = _service.CalculateGrade(myCv.Gender, myCv.Skills);
            }
        }
    }
}

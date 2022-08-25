using Cv_Project;
using CV_Project.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CV_Project.Pages
{
    public class BrowseCvModel : PageModel
    {
        [BindProperty]
        public string SearchContent { get; set; }

        public readonly Service _service;

        public IEnumerable<CV> myCvs { get; set; }

        public BrowseCvModel(Service service)
        {
            _service = service;
        }

        public async Task OnGetAsync()
        {
            myCvs = await _service.GetCvs();
        }

        public async Task OnPost()
        {

            if (SearchContent!= null)
            {
                myCvs = await _service.Search(SearchContent);
            }
            else
            {
                myCvs = await _service.GetCvs();
            }

        }

        public async Task<RedirectToPageResult> OnPostDeleteAsync(int id)
        {
            await _service.DeleteCv(id);
            return RedirectToPage("/BrowseCv");
        }

    }
}

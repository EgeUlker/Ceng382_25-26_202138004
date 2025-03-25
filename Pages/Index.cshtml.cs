using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using lab5.Models;

namespace lab5.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public ClassInformationModel NewClass { get; set; } = new ClassInformationModel();

        // Static list to retain data across page instances
        public static List<ClassInformationModel> Classes { get; set; } = new List<ClassInformationModel>();

        public void OnGet()
        {
            // Page load logic (if needed)
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Classes.Add(NewClass); // Add the new class to the static list
            return RedirectToPage();
        }
    }
}
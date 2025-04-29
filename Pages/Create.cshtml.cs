using System.Threading.Tasks;
using lab9.Data;
using lab9.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lab9.Pages
{
    public class CreateModel : PageModel
    {
        private readonly SchoolDbContext _context;

        public CreateModel(SchoolDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ClassInformationModel ClassInformation { get; set; } = new ClassInformationModel();

        public IActionResult OnGet() => Page();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            _context.ClassInformationModels.Add(ClassInformation);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}
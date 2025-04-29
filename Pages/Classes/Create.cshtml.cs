using lab9.Data;
using lab9.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lab9.Pages.Classes
{
    public class CreateModel : PageModel
    {
        private readonly SchoolDbContext _context;

        public CreateModel(SchoolDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Class ClassInfo { get; set; } = new Class();

        public IActionResult OnGet() => Page();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            _context.Classes.Add(ClassInfo);
            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
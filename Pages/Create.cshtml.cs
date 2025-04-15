using System.Threading.Tasks;
using lab8.Data;
using lab8.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lab8.Pages
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // Formda kullanılacak ClassInformation property
        [BindProperty]
        public ClassInformationModel ClassInformation { get; set; } = new ClassInformationModel();

        // GET: sayfa yüklendiğinde boş form göstermek için.
        public IActionResult OnGet()
        {
            return Page();
        }

        // POST: Form gönderimiyle yeni sınıf kaydı eklenir.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ClassInformationModels.Add(ClassInformation);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}
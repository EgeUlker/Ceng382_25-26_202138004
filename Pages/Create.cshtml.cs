using System.Threading.Tasks;
using lab7.Data;
using lab7.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lab7.Pages
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        
        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [BindProperty]
        public ClassInformationModel ClassInformation { get; set; } = new ClassInformationModel();
        
        public IActionResult OnGet()
        {
            return Page();
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            _context.ClassInformationModels.Add(ClassInformation);
            await _context.SaveChangesAsync();
            
            return RedirectToPage("./Index");
        }
    }
}
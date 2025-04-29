using lab9.Data;
using lab9.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace lab9.Pages.Classes
{
    public class DeleteModel : PageModel
    {
        private readonly SchoolDbContext _context;

        public DeleteModel(SchoolDbContext context)
        {
            _context = context;
        }

        // Silinecek kaydı tutacak property.
        [BindProperty]
        public Class ClassInfo { get; set; } = new Class();

        // Silme sayfasını yüklemek için.
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            ClassInfo = await _context.Classes.FirstOrDefaultAsync(c => c.Id == id);
            if (ClassInfo == null)
                return NotFound();

            return Page();
        }

        // Silme işlemini gerçekleştiren POST metodu.
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
                return NotFound();

            // id ile kaydı buluyoruz.
            ClassInfo = await _context.Classes.FindAsync(id);

            if (ClassInfo != null)
            {
                _context.Classes.Remove(ClassInfo);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
    }
}
using lab9.Data;
using lab9.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace lab9.Pages.Classes
{
    public class EditModel : PageModel
    {
        private readonly SchoolDbContext _context;

        public EditModel(SchoolDbContext context)
        {
            _context = context;
        }

        // Düzenlenecek kaydı tutacak property.
        [BindProperty]
        public Class ClassInfo { get; set; } = new Class();

        // Kaydı düzenleme sayfasını yüklemek için.
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            ClassInfo = await _context.Classes.FirstOrDefaultAsync(c => c.Id == id);

            if (ClassInfo == null)
                return NotFound();

            return Page();
        }

        // Düzenleme işleminden sonra POST metodu.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            // Güncelleme işlemi için mevcut entity'yi attach ediyoruz.
            _context.Attach(ClassInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Düzenlediğimiz kaydın veritabanında yoksa NotFound döndürüyoruz.
                if (!ClassExists(ClassInfo.Id))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToPage("Index");
        }

        // Belirtilen Id'ye sahip Class kaydının olup olmadığını kontrol eder.
        private bool ClassExists(int id)
        {
            return _context.Classes.Any(c => c.Id == id);
        }
    }
}
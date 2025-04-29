using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using lab9.Data;
using lab9.Models;  // Class modeliniz bu namespace'de yer almalı

namespace lab9.Pages.Classes
{
    public class IndexModel : PageModel
    {
        private readonly SchoolDbContext _context;

        public IndexModel(SchoolDbContext context)
        {
            _context = context;
        }

        // Sayfada görüntülenecek sınıf listesini tutuyoruz.
        public IList<Class> ClassList { get; set; } = new List<Class>();

        // Sayfa yüklendiğinde veritabanından tüm kayıtları çekiyoruz.
        public async Task OnGetAsync()
        {
            ClassList = await _context.Classes.ToListAsync();
        }
    }
}
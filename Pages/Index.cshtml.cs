using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lab7.Data;
using lab7.Helpers;
using lab7.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lab7.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        
        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // URL üzerinden alınan filtre değeri (varsayılan boş string)
        [BindProperty(SupportsGet = true)]
        public string searchTerm { get; set; } = string.Empty;

        // Sayfalı veri listesi; OnGetAsync'de doldurulacak.
        public PaginatedList<ClassInformationModel> ClassInformationModelList { get; set; } = default!;

        public async Task OnGetAsync(int? pageIndex)
        {
            var query = _context.ClassInformationModels.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(c => c.ClassName.Contains(searchTerm));
            }

            int pageSize = 10; // Her sayfada 10 kayıt gösterilecek.
            ClassInformationModelList = await PaginatedList<ClassInformationModel>.CreateAsync(query, pageIndex ?? 1, pageSize);
        }

        // JSON dışa aktarma POST handler'ı: Tüm kayıtlar veya filtrelenmiş kayıtlar JSON olarak dışa aktarılır.
        public IActionResult OnPostExportJson(bool isFiltered)
        {
            IEnumerable<ClassInformationModel> data;
            if (isFiltered && !string.IsNullOrEmpty(searchTerm))
            {
                data = _context.ClassInformationModels.Where(c => c.ClassName.Contains(searchTerm)).ToList();
            }
            else
            {
                data = _context.ClassInformationModels.ToList();
            }

            string jsonResult = Utils.Instance.ExportToJson(data);
            return File(System.Text.Encoding.UTF8.GetBytes(jsonResult), "application/json", "export.json");
        }
    }
}
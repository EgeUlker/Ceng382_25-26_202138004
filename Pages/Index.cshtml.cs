using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lab9.Data;
using lab9.Helpers;
using lab9.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lab9.Pages
{
    public class IndexModel : PageModel
    {
        private readonly SchoolDbContext _context;
        
        public IndexModel(SchoolDbContext context)
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
            // Veritabanından ClassInformationModels tablosunu sorguluyoruz.
            var query = _context.ClassInformationModels.AsQueryable();

            // Arama terimi varsa, sorguya filtre ekliyoruz.
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

            // Eğer filtreli dışa aktarım seçildiyse ve arama terimi boş değilse filtre uyguluyoruz
            if (isFiltered && !string.IsNullOrEmpty(searchTerm))
            {
                data = _context.ClassInformationModels.Where(c => c.ClassName.Contains(searchTerm)).ToList();
            }
            else
            {
                data = _context.ClassInformationModels.ToList();
            }

            string jsonResult = Utils.Instance.ExportToJson(data);

            // JSON dosyası olarak kullanıcıya geri gönderiyoruz.
            return File(System.Text.Encoding.UTF8.GetBytes(jsonResult), "application/json", "export.json");
        }
    }
}
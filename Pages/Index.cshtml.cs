using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using lab5.Models;

namespace lab5.Pages
{
    public static class SessionHelper
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
    }

    public class IndexModel : PageModel
    {
        public List<ClassInformationTable> ClassData { get; set; } = new List<ClassInformationTable>();

        [BindProperty]
        public ClassInformationModel NewClass { get; set; } = new ClassInformationModel();

        [BindProperty(SupportsGet = true)]
        public string? InstructorFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        private const int PageSize = 10;
        public int TotalPages { get; private set; }

        public void OnGet()
        {
            var existingData = HttpContext.Session.GetString("ClassData");
            if (existingData != null)
            {
                ClassData = JsonConvert.DeserializeObject<List<ClassInformationTable>>(existingData);
            }
            else
            {
                ClassData = new List<ClassInformationTable>();
            }

            var rawData = GenerateSampleData();

            if (!string.IsNullOrWhiteSpace(InstructorFilter))
            {
                rawData = rawData.Where(c => c.Instructor == InstructorFilter).ToList();
            }

            TotalPages = (int)Math.Ceiling((double)rawData.Count / PageSize);

            ClassData = rawData.Skip((PageNumber - 1) * PageSize).Take(PageSize)
                               .Select(c => new ClassInformationTable
                               {
                                   ClassName = c.ClassName,
                                   Instructor = c.Instructor,
                                   StartDate = c.StartDate,
                                   StudentCount = c.StudentCount,
                                   Description = c.Description,
                                   ID = c.ID
                               }).ToList();
        }

        public IActionResult OnPostAdd()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var existingData = HttpContext.Session.GetString("ClassData");
            if (existingData != null)
            {
                ClassData = JsonConvert.DeserializeObject<List<ClassInformationTable>>(existingData);
            }
            else
            {
                ClassData = new List<ClassInformationTable>();
            }

            ClassData.Add(new ClassInformationTable
            {
                ClassName = NewClass.ClassName,
                Instructor = "Unknown Instructor",
                StartDate = DateTime.Now,
                StudentCount = NewClass.StudentCount,
                Description = NewClass.Description,
                ID = ClassData.Count + 1
            });

            HttpContext.Session.SetString("ClassData", JsonConvert.SerializeObject(ClassData));
            return RedirectToPage();
        }

        private List<ClassInformationModel> GenerateSampleData()
        {
            var sampleData = new List<ClassInformationModel>();
            for (int i = 1; i <= 100; i++)
            {
                sampleData.Add(new ClassInformationModel
                {
                    ID = i,
                    ClassName = $"Class {i}",
                    Instructor = $"Instructor {i % 5}",
                    StartDate = DateTime.Now.AddDays(-i),
                    StudentCount = new Random().Next(10, 50),
                    Description = $"Description for Class {i}"
                });
            }
            return sampleData;
        }
    }
}

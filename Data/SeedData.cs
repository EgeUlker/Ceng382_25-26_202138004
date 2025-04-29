using lab9.Models;
using System.Linq;

namespace lab9.Data
{
    public static class SeedData
    {
        public static void Initialize(SchoolDbContext context)
        {
            // Veritabanını oluşturmayı garanti et.
            context.Database.EnsureCreated();

            // Eğer veriler zaten varsa, seed işlemi yapma.
            if (context.Classes.Any())
            {
                return;
            }

            // Seed verilerini ekleyin. (SchoolDbContext'te bu DbSet'in tanımlı olduğundan emin olun)
            context.Classes.AddRange(
                new Class { Name = "Mathematics", PersonCount = 30, Description = "Math class", IsActive = true },
                new Class { Name = "Science", PersonCount = 25, Description = "Science class", IsActive = true }
            );

            context.SaveChanges();
        }
    }
}
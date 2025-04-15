using Microsoft.EntityFrameworkCore;
using lab8.Models;

namespace lab8.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Burada ClassInformationModel veritabanı varlığı olarak kullanılacak.
        public DbSet<ClassInformationModel> ClassInformationModels { get; set; }
    }
}
using Microsoft.EntityFrameworkCore;
using lab9.Models;

namespace lab9.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Mevcut DbSet
        public DbSet<ClassInformationModel> ClassInformationModels { get; set; }

        // Eğer Class entity'si de kullanılacaksa ekleyin:
        public DbSet<Class> Classes { get; set; }
    }
}
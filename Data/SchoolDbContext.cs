using Microsoft.EntityFrameworkCore;
using lab9.Models;

namespace lab9.Data
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
            : base(options)
        {
        }

        // Bu özellik, ClassInformationModel türündeki verileri yönetecek.
        public DbSet<ClassInformationModel> ClassInformationModels { get; set; }

        // Eğer başka DbSet'ler de kullanıyorsanız, örneğin:
        public DbSet<Class> Classes { get; set; }
    }
}
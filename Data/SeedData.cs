using System;
using System.Collections.Generic;
using System.Linq;
using lab8.Models;

namespace lab8.Data
{
    public static class SeedData
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Eğer veritabanında zaten kayıt varsa, seed işlemini atla.
            if (context.ClassInformationModels.Any())
            {
                return;
            }
            
            var classes = new List<ClassInformationModel>();
            
            for (int i = 1; i <= 100; i++)
            {
                classes.Add(new ClassInformationModel
                {
                    ClassName = $"Class {i}",
                    Instructor = $"Instructor {i}",
                    StartDate = DateTime.Now.AddDays(i), // Bugünden i gün sonrası
                    StudentCount = 10 + i,               // Örneğin, 11'den başlayabilir
                    Description = $"Description for Class {i}"
                });
            }

            context.ClassInformationModels.AddRange(classes);
            context.SaveChanges();
        }
    }
}
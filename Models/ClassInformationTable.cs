using System;

namespace lab8.Models
{
    public class ClassInformationTable
    {
        public int ID { get; set; }
        public string ClassName { get; set; }
        public string Instructor { get; set; }
        public DateTime? StartDate { get; set; }  // Nullable DateTime
        public int? StudentCount { get; set; }    // Nullable int
        public string Description { get; set; }

        // Parametresiz yapıcı (Entity Framework ve model binding için gerekli)
        public ClassInformationTable() { }

        // Zorunlu alanların atanması için kullanılan yapıcı
        public ClassInformationTable(string className, string instructor, DateTime? startDate, int? studentCount, string description)
        {
            ClassName = className;
            Instructor = instructor;
            StartDate = startDate;
            StudentCount = studentCount;
            Description = description;
        }
    }
}
using System;

namespace lab9.Models
{
    public class ClassInformationModel
    {
        public int ID { get; set; }
        public string ClassName { get; set; }
        public string Instructor { get; set; }
        public DateTime StartDate { get; set; }
        public int StudentCount { get; set; }
        public string Description { get; set; }

        // Parametresiz constructor (Entity Framework için de gereklidir)
        public ClassInformationModel()
        {
            ClassName = "Default Class Name";
            Instructor = "Default Instructor";
            Description = "Default Description";
            StartDate = DateTime.Now;
            StudentCount = 0;
        }

        // Tüm özellikleri alan constructor (kullanım tercihinize göre)
        public ClassInformationModel(string className, string instructor, DateTime startDate, int studentCount, string description)
        {
            ClassName = className;
            Instructor = instructor;
            StartDate = startDate;
            StudentCount = studentCount;
            Description = description;
        }
    }
}
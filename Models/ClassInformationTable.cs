namespace lab5.Models
{
    public class ClassInformationTable
    {
        public string ClassName { get; set; }
        public string Instructor { get; set; }
        public DateTime StartDate { get; set; }
        public int StudentCount { get; set; }

        // **Description alanı**
        public string Description { get; set; }

        // ID sadece arkaplanda kullanılacak
        public int ID { get; set; }
    }
}

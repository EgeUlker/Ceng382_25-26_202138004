namespace lab5.Models
{
    public class ClassInformationModel
    {
        public int ID { get; set; }
        public string ClassName { get; set; }
        public string Instructor { get; set; }
        public DateTime StartDate { get; set; }
        public int StudentCount { get; set; }
        public string Description { get; set; } // Description alanı eklendi
    }
}

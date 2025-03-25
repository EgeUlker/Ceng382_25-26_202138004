namespace lab5.Models
{
    public class ClassInformationModel
    {
        public int Id { get; set; } // Benzersiz kimlik numarası (auto-incremented)

        public string ClassName { get; set; } // Sınıfın adı

        public int StudentCount { get; set; } // Sınıftaki öğrenci sayısı

        public string Description { get; set; } // Sınıf açıklaması

        // Constructor ile varsayılan değerlerin tanımlanması (isteğe bağlı)
        public ClassInformationModel()
        {
            ClassName = string.Empty;
            Description = string.Empty;
        }
    }
}